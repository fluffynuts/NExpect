using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NExpect.Exceptions;
using NExpect.Interfaces;
using NExpect.Tests.Collections;
using NUnit.Framework;
using PeanutButter.Utils;

namespace NExpect.Tests.ObjectEquality
{
    // TODO: something similar for .To.Equal()
    [TestFixture]
    public class MultipleIComparableTypes
    {
        private static Type NullableType = typeof(Nullable<>);
        
        private static readonly Type[] NonNullableNumerics = new[]
        {
            typeof(char),
            typeof(byte),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };
        
        private static readonly Type[] NullableNumerics 
            = NonNullableNumerics.Select(n => NullableType.MakeGenericType(n))
                .ToArray();
        private static readonly Type[] Numerics =
            NonNullableNumerics.And(NullableNumerics);

        // basically runs all permutations of numeric Greater.Than / Less.Than invocations
        public static IEnumerable<(string dotted, object a, object e, bool shouldPass)> NumericsGenerator()
        {
            return GenerateTestCasesFor(Numerics, 1, 2);
        }

        public static IEnumerable<(string dotted, object a, object e, bool shouldPass)> TimeSpansGenerator()
        {
            return GenerateTestCasesFor(new[] { typeof(TimeSpan) }, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
        }

        private static object Coerce(object value, Type type)
        {
            if (type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();
                if (genericType != NullableType)
                {
                    throw new InvalidOperationException($"Dunno what to do with {value} => {type}");
                }

                var underlyingType = type.GetGenericArguments()[0];
                var newValue = Convert.ChangeType(value, underlyingType);
                var generic = typeof(MultipleIComparableTypes).GetMethod(nameof(MakeNullable), BindingFlags.NonPublic | BindingFlags.Static);
                var specific = generic.MakeGenericMethod(underlyingType);
                return specific.Invoke(null, new object[] { newValue });
            }

            return Convert.ChangeType(value, type);
        }

        // ReSharper disable once ConvertNullableToShortForm
        private static Nullable<T> MakeNullable<T>(T value) where T: struct
        {
            // ReSharper disable once ConvertNullableToShortForm
            return new Nullable<T>(value);
        }

        private static IEnumerable<(string dotted, object a, object e, bool shouldPass)> GenerateTestCasesFor<T>(
            Type[] types,
            T minValue,
            T maxValue)
        {
            foreach (var actualType in types)
            {
                foreach (var expectedType in types)
                {
                    yield return (
                        "To.Be.Less.Than",
                        Coerce(minValue, actualType),
                        Coerce(maxValue, expectedType),
                        true
                    );
                    
                    yield return (
                        "To.Be.Greater.Than",
                        Coerce(maxValue, expectedType),
                        Coerce(minValue, actualType),
                        true
                    );
                    yield return (
                        "To.Be.Less.Than",
                        Coerce(maxValue, actualType),
                        Coerce(minValue, expectedType),
                        false
                    );
                    yield return (
                        "To.Be.Greater.Than",
                        Coerce(minValue, actualType),
                        Coerce(maxValue, expectedType),
                        false
                    );
                    
                    yield return (
                            "To.Be.Greater.Than.Or.Equal.To",
                            Coerce(maxValue, actualType),
                            Coerce(minValue, actualType),
                            true
                        );
                    
                    yield return (
                            "To.Be.Greater.Than.Or.Equal.To",
                            Coerce(maxValue, actualType),
                            Coerce(maxValue, actualType),
                            true
                        );
                    
                    yield return (
                            "To.Be.Less.Than.Or.Equal.To",
                            Coerce(minValue, actualType),
                            Coerce(maxValue, actualType),
                            true
                        );
                    
                    yield return (
                            "To.Be.Less.Than.Or.Equal.To",
                            Coerce(maxValue, actualType),
                            Coerce(maxValue, actualType),
                            true
                        );
                }
            }
        }

        [TestCaseSource(nameof(NumericsGenerator))]
        [TestCaseSource(nameof(TimeSpansGenerator))]
        [Test]
        public void WhenActualNotEqualExpected_ShouldNotThrowForComparison_(
            (string dotted, object actual, object expected, bool shouldPass) testCase)
        {
            // Arrange
            // Act
            var actualType = testCase.actual.GetType();
            var expectedType = testCase.expected.GetType();
            var expectMethods = typeof(Expectations).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var expectation = expectMethods
                .Select(mi =>
                {
                    if (mi.Name != "Expect")
                    {
                        return null;
                    }

                    try
                    {
                        return mi.Invoke(null, new[] { testCase.actual });
                    }
                    catch (Exception ex)
                    {
                        if (mi.GetParameters()[0].ParameterType == typeof(long))
                        {
                            Console.WriteLine(ex.Message);
                        }

                        return null;
                    }
                })
                .FirstOrDefault(o => o != null);
            if (expectation == null)
            {
                // try invoke the generic one
                expectation = expectMethods.Select(
                    mi =>
                    {
                        if (mi.Name != "Expect" || !mi.IsGenericMethod)
                        {
                            return null;
                        }

                        try
                        {
                            var genericExpect = mi.MakeGenericMethod(actualType);
                            return genericExpect.Invoke(null, new[] { testCase.actual });
                        }
                        catch
                        {
                            return null;
                        }
                    }).FirstOrDefault(o => o != null);
            }

            if (expectation == null)
            {
                Assert.Fail($"Can't start Expectation for {testCase.actual} ({testCase.actual.GetType()})");
            }

            var dottedParts = testCase.dotted.Split('.');
            var final = dottedParts.Last();
            var last = expectation;
            var queue = new Queue<string>(dottedParts.Take(dottedParts.Length - 1));
            while (queue.Count > 0)
            {
                last = last.GetPropertyValue(queue.Dequeue());
            }

            var extMethods = typeof(GreaterAndLessContinuationExtensions).GetMethods()
                .Where(mi => mi.Name == final && mi.GetParameters().Length == 2)
                .ToArray();

            var ext = extMethods
                .Where(mi => !mi.IsGenericMethod)
                .Select(mi =>
                {
                    try
                    {
                        mi.Invoke(null, new object[] { last, testCase.expected });
                        return mi;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null && ex.InnerException is UnmetExpectationException)
                        {
                            return mi;
                        }

                        var foo = ex;
                        // invocation fails -- no implicit upcast, perhaps
                        return null;
                    }
                }).FirstOrDefault(o => o != null);

            if (ext == null)
            {
                // try find the generic with T1, T2
                ext = extMethods
                    .Where(mi => mi.IsGenericMethod)
                    .Select(mi =>
                    {
                        MethodInfo genericExt;
                        try
                        {
                            genericExt = mi.MakeGenericMethod(actualType, expectedType);
                        }
                        catch
                        {
                            return null;
                        }

                        try
                        {
                            genericExt.Invoke(null, new object[] { last, testCase.expected });
                            return genericExt;
                        }
                        catch (Exception ex)
                        {
                            if (ex.InnerException != null && ex.InnerException is UnmetExpectationException)
                            {
                                return genericExt;
                            }

                            return null;
                        }
                    }).FirstOrDefault(o => o != null);
            }

            if (ext == null)
            {
                Assert.Fail($"Can't find .Than to operate on {last.GetType()} with expected type {expectedType}");
            }

            // Assert
            if (testCase.shouldPass)
            {
                Assert.That(
                    () => ext.Invoke(last, new object[] { last, testCase.expected }),
                    Throws.Nothing);
            }
            else
            {
                Assert.That(
                    () => ext.Invoke(last, new object[] { last, testCase.expected }),
                    Throws.InstanceOf<TargetInvocationException>()
                        .With.InnerException.InstanceOf<UnmetExpectationException>());
            }
        }
    }
}