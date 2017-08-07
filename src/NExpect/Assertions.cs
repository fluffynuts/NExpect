using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NExpect.Exceptions;

namespace NExpect
{
    public static class Assertions
    {
        private static Type _assertionExceptionWithMessageOnlyTypeField;
        private static Type AssertionExceptionWithMessageOnlyType
            { get => 
                _assertionExceptionWithMessageOnlyTypeField ?? 
                (_assertionExceptionWithMessageOnlyTypeField = FindType("NUnit.Framework.AssertionException", new[] {typeof(string)})
                ?? typeof(UnmetExpectation)); }

        private static Type FindType(string fullName, Type[] requiredConstructorParameters)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Select(TryGetTypes)
                .SelectMany(a => a)
                .Aggregate(null as Type, (acc, cur) =>
                {
                    try
                    {
                        return acc ?? TypeMatch(cur, fullName, requiredConstructorParameters);
                    }
                    catch
                    {
                        return acc;
                    }
                });
        }

        private static Type TypeMatch(Type t, string fullName, Type[] constructorParamTypes)
        {
            if (t.FullName != fullName)
                return null;
            return t.GetConstructors()
                .Any(c => c.GetParameters()
                    .Select(p => p.ParameterType)
                    .ToArray()
                    .Matches(constructorParamTypes))
                ? t
                : null;
        }

        private static bool Matches<T>(this T[] src, T[] other)
        {
            return src.Length == other.Length &&
                   !src.Except(other).Any();
        }

        private static Type[] TryGetTypes(Assembly a)
        {
            try
            {
                return a.GetExportedTypes();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get types from assembly {a.FullName}: {ex.Message}");
                return _empty;
            }
        }

        private static readonly Type[] _empty = new Type[0];

        internal static void Throw(string message)
        {
            throw CreateExceptionFor(message);
        }

        static Func<string, Exception> _assertionsGenerator;

        /// <summary>
        /// Register your own factory for generating assertion exceptions
        /// </summary>
        /// <param name="generator">Func to invoke when NExpect needs an assertion exception</param>
        /// <typeparam name="T">Type of exception</typeparam>
        public static void RegisterAssertionsFactory<T>(Func<string, T> generator) where T : Exception
        {
            _assertionsGenerator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        internal static Exception CreateExceptionFor(string message)
        {
            return _assertionsGenerator?.Invoke(message) 
                        ?? TryCreateExceptionFor(message)
                        ?? new UnmetExpectation(message);
        }

        internal static Exception TryCreateExceptionFor(string message)
        {
            try
            {
                return (Exception) Activator.CreateInstance(
                    AssertionExceptionWithMessageOnlyType,
                    message ?? "(failed)"
                );
            } catch { return null; }
        }
    }
}