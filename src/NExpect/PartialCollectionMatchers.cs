using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect
{
    /// <summary>
    /// Provides matchers like
    /// Expect(collection).Not.To.Contain.Any.Of(otherCollection)
    /// </summary>
    public static class PartialCollectionMatchers
    {
        /// <summary>
        /// Tests for intersection between two collections
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="other"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Of<T>(
            this ICountMatchContinuation<IEnumerable<T>> continuation,
            IEnumerable<T> other
        )
        {
            return continuation.Of(
                other,
                NULL_STRING
            );
        }

        /// <summary>
        /// Tests for intersection between two collections
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="other"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Of<T>(
            this ICountMatchContinuation<IEnumerable<T>> continuation,
            IEnumerable<T> other,
            string customMessage
        )
        {
            return continuation.Of(
                other,
                () => customMessage
            );
        }

        /// <summary>
        /// Tests for intersection between two collections
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="other"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Of<T>(
            this ICountMatchContinuation<IEnumerable<T>> continuation,
            IEnumerable<T> other,
            Func<string> customMessageGenerator
        )
        {
            return continuation.AddMatcher(actual =>
            {
                var intersection = actual.Intersect(other);
                var passed = CountPassStrategies[continuation.Method](
                    intersection.Count(),
                    continuation.ExpectedCount,
                    other.Count()
                );
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () =>
                            $@"Expected {passed.AsNot()}to find {
                                CountMessageFor(continuation.Method, continuation.ExpectedCount)
                            } of
{
    other.Stringify()
}
in
{
    actual.Stringify()
}
but found{
    (intersection.Any() 
        ? $"\n{intersection.Stringify()}" 
        : " none")
}",
                        customMessageGenerator
                    )
                );
            });
        }

        private static string CountMessageFor(
            CountMatchMethods method,
            int count
        )
        {
            switch (method)
            {
                case CountMatchMethods.All:
                case CountMatchMethods.Any:
                    return method.ToString().ToLower();
                default:
                    return $"{method.ToString().ToLower()} ({count})";
            }
        }

        private static readonly Dictionary<CountMatchMethods, Func<int, int, int, bool>> CountPassStrategies =
            new()
            {
                [CountMatchMethods.All] = (actual, expected, total) => actual == total,
                [CountMatchMethods.Any] = (actual, expected, total) => actual > 0,
                [CountMatchMethods.Exactly] = (actual, expected, total) => actual == expected,
                [CountMatchMethods.Maximum] = (actual, expected, total) => actual <= expected,
                [CountMatchMethods.Minimum] = (actual, expected, total) => actual >= expected,
                [CountMatchMethods.Only] = (actual, expected, total) => actual == expected && actual == total
            };
    }
}