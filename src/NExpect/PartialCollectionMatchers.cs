using System;
using System.Collections.Generic;
using System.Linq;
using Imported.PeanutButter.Utils;
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
        /// Provides quick access to .Of with a params collection
        /// at the cost of no custom messaging
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="item"></param>
        /// <param name="more"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Of<T>(
            this ICountMatchContinuation<IEnumerable<T>> continuation,
            T item,
            params T[] more
        )
        {
            return continuation.Of(more.And(item));
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
            return continuation.AddMatcher(
                actual =>
                {
                    var otherArray = other as IList<T> ?? other.ToArray();
                    var actualArray = actual as IList<T> ?? actual.ToArray();
                    var intersection = actualArray.Intersect(otherArray).ToArray();
                    var missing = otherArray.Except(actualArray).ToArray();
                    var passed = continuation.Method switch
                    {
                        CountMatchMethods.All => !missing.Any(),
                        _ => CountPassStrategies[continuation.Method](
                            intersection.Count(),
                            continuation.ExpectedCount,
                            otherArray.Count()
                        )
                    };
                    
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () =>
                                $@"Expected {passed.AsNot()}to find {
                                    CountMessageFor(continuation.Method, continuation.ExpectedCount)
                                } of
{
    otherArray.Stringify()
}
in
{
    actualArray.Stringify()
}
but found matching:{
    (intersection.Any()
        ? $"\n{intersection.Stringify()}"
        : " none")
}
and missing:{
    (missing.Any()
        ? $"\n{missing.Stringify()}"
        : " none")
}",
                            customMessageGenerator
                        )
                    );
                }
            );
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