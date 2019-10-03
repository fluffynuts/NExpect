using System;
using System.Collections.Generic;
using NExpect.EqualityComparers;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect
{
    /// <summary>
    /// Adds approximate equality matchers for decimal values
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public static class ApproximateEqualityDecimalMatchers
    {
        /// <summary>
        /// Tests if the actual value is approximately equal, to two decimal
        /// places
        /// </summary>
        /// <param name="approx"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static IMore<decimal> Equal(this IApproximately<decimal> approx,
            decimal expected)
        {
            return approx.Equal(expected, MessageHelpers.NULL_STRING);
        }

        /// <summary>
        /// Tests if the actual value is approximately equal, to two decimal
        /// places
        /// </summary>
        /// <param name="approx"></param>
        /// <param name="expected"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<decimal> Equal(this IApproximately<decimal> approx,
            decimal expected,
            string customMessage)
        {
            return approx.Equal(expected, () => customMessage);
        }

        /// <summary>
        /// Tests if the actual value is approximately equal, to two decimal
        /// places
        /// </summary>
        /// <param name="approx"></param>
        /// <param name="expected"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<decimal> Equal(this IApproximately<decimal> approx,
            decimal expected,
            Func<string> customMessageGenerator)
        {
            return approx.Equal(expected,
                new DecimalsEqualToDecimalPlacesRounded(2),
                customMessageGenerator);
        }

        /// <summary>
        /// Tests if the actual value is approximately equal, using the
        /// provided comparer
        /// </summary>
        /// <param name="approx"></param>
        /// <param name="expected"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IMore<decimal> Equal(this IApproximately<decimal> approx,
            decimal expected,
            IEqualityComparer<decimal> comparer)
        {
            return approx.Equal(expected, comparer, NULL_STRING);
        }

        /// <summary>
        /// Tests if the actual value is approximately equal, using the
        /// provided comparer
        /// </summary>
        /// <param name="approx"></param>
        /// <param name="expected"></param>
        /// <param name="comparer"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<decimal> Equal(this IApproximately<decimal> approx,
            decimal expected,
            IEqualityComparer<decimal> comparer,
            string customMessage)
        {
            return approx.Equal(expected, comparer, () => customMessage);
        }
        

        /// <summary>
        /// Tests if the actual value is approximately equal, using the
        /// provided comparer
        /// </summary>
        /// <param name="approx"></param>
        /// <param name="expected"></param>
        /// <param name="comparer"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<decimal> Equal(this IApproximately<decimal> approx,
            decimal expected,
            IEqualityComparer<decimal> comparer,
            Func<string> customMessageGenerator)
        {
            approx.AddMatcher(actual =>
            {
                var passed = comparer.Equals(actual, expected);
                return new MatcherResult(passed,
                    () => FinalMessageFor(
                        () => $@"Expected {
                                actual.Stringify()
                            } to approxmiately equal {
                                expected.Stringify()
                            }",
                        customMessageGenerator));
            });
            return approx.More();
        }
    }
}