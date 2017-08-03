using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NExpect.Exceptions;

namespace NExpect
{
    internal static class Assertion
    {
        private static Type _assertionExceptionWithMessageOnlyType =
            FindType("NUnit.Framework.AssertionException", new[] {typeof(string)})
            ?? typeof(UnmetExpectation);

        private static Type FindType(string fullName, Type[] requiredConstructorParameters)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Select(TryGetTypes)
                .SelectMany(a => a)
                .Aggregate(null as Type, (acc, cur) => acc ?? TypeMatch(cur, fullName, requiredConstructorParameters));
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

        internal static Exception CreateExceptionFor(string message)
        {
            try
            {
                return (Exception)Activator.CreateInstance(_assertionExceptionWithMessageOnlyType, message ?? "(failed)");
            }
            catch
            {
                // cover the case where construction didnt' happen properly -- though I need
                //  to track down *why* it's not happening properly sometimes
                //  -> the AssertionException constructor is throwing an ArgumentNullException?!
                _assertionExceptionWithMessageOnlyType = typeof(UnmetExpectation);
                return new UnmetExpectation(message);
            }
        }
    }
}