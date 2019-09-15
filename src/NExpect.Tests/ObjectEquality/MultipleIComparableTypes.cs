using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NExpect.Exceptions;
using NExpect.Interfaces;
using NUnit.Framework;
using PeanutButter.Utils;

namespace NExpect.Tests.ObjectEquality
{
    // TODO: something similar for .To.Equal()
    [TestFixture]
    public class MultipleIComparableTypes
    {
        // basically runs all permutations of numeric Greater.Than / Less.Than invocations
        public static IEnumerable<(string compare, object a, object e, bool shouldPass)> ActualLessThanExpectedGenerator()
        {
            var types = new[]
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

            foreach (var actualType in types)
            {
                foreach (var expectedType in types)
                {
                    yield return ("Less", Convert.ChangeType(1, actualType), Convert.ChangeType(2, expectedType), true);
                    yield return ("Greater", Convert.ChangeType(2, expectedType), Convert.ChangeType(1, actualType), true);
                    // TODO: these flips should have "just worked"
//                    yield return ("Less", Convert.ChangeType(2, expectedType), Convert.ChangeType(1, actualType), false);
//                    yield return ("Greater", Convert.ChangeType(1, actualType), Convert.ChangeType(2, expectedType), false);
                }
            }
        }

        [TestCaseSource(nameof(ActualLessThanExpectedGenerator))]
        [Test]
        public void WhenActualNotEqualExpected_ShouldNotThrowForComparison_(
            (string compare, object actual, object expected, bool shouldPass) testCase)
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

            var to = expectation.GetPropertyValue("To");
            var be = to.GetPropertyValue("Be");
            var last = be.GetPropertyValue(testCase.compare);

            var lessType = typeof(ILessContinuation<>).MakeGenericType(actualType);
            var extMethods = typeof(GreaterAndLessContinuationExtensions).GetMethods()
                .Where(mi => mi.Name == "Than" && mi.GetParameters().Length == 2)
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
                    catch (UnmetExpectationException)
                    {
                        // this is a proper, failing expectation
                        return mi;
                    }
                    catch (Exception ex)
                    {
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
                        catch (UnmetExpectationException)
                        {
                            return genericExt;
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }
                    }).FirstOrDefault(o => o != null);
            }

            if (ext == null)
            {
                Assert.Fail($"Can't find .Than to operate on {lessType} with expected type {expectedType}");
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
                    Throws.InstanceOf<UnmetExpectationException>());
            }
        }
    }
}