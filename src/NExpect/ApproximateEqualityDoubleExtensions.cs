using System;
using System.Collections.Generic;
using NExpect.EqualityComparers;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect
{
    /// <summary>
    /// Adds approxmiate equality testing for double values
    /// </summary>
    public static class ApproximateEqualityDoubleExtensions
    {
        /// <summary>
        /// Tests if the actual value is approximately equal, to two double
        /// places
        /// </summary>
        /// <param name="approx"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static IMore<double> Equal(this IApproximately<double> approx,
            double expected)
        {
            return approx.Equal(expected, MessageHelpers.NULL_STRING);
        }

        /// <summary>
        /// Tests if the actual value is approximately equal, to two double
        /// places
        /// </summary>
        /// <param name="approx"></param>
        /// <param name="expected"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<double> Equal(this IApproximately<double> approx,
            double expected,
            string customMessage)
        {
            return approx.Equal(expected, () => customMessage);
        }

        /// <summary>
        /// Tests if the actual value is approximately equal, to two double
        /// places
        /// </summary>
        /// <param name="approx"></param>
        /// <param name="expected"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<double> Equal(this IApproximately<double> approx,
            double expected,
            Func<string> customMessageGenerator)
        {
            return approx.Equal(expected,
                new DoublesEqualToDecimalPlacesRounded(2),
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
        public static IMore<double> Equal(this IApproximately<double> approx,
            double expected,
            IEqualityComparer<double> comparer)
        {
            return approx.Equal(expected, comparer, MessageHelpers.NULL_STRING);
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
        public static IMore<double> Equal(this IApproximately<double> approx,
            double expected,
            IEqualityComparer<double> comparer,
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
        public static IMore<double> Equal(this IApproximately<double> approx,
            double expected,
            IEqualityComparer<double> comparer,
            Func<string> customMessageGenerator)
        {
            approx.AddMatcher(actual =>
            {
                var passed = comparer.Equals(actual, expected);
                return new MatcherResult(passed,
                    () => MessageHelpers.FinalMessageFor(
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