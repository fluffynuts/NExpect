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
    /// Provides matchers to assert variance in a collection
    /// </summary>
    public static class CollectionVarianceExtensions
    {
        /// <summary>
        /// Expects the collection to have more than one item
        /// and to have any variance (ie at least two items are
        /// not .Equal)
        /// </summary>
        /// <param name="to"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Vary<T>(
            this ICollectionTo<T> to
        )
        {
            return to.Vary(NULL_STRING);
        }

        /// <summary>
        /// Expects the collection to have more than one item
        /// and to have any variance (ie at least two items are
        /// not .Equal)
        /// </summary>
        /// <param name="to"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Vary<T>(
            this ICollectionTo<T> to,
            string customMessage
        )
        {
            return to.Vary(() => NULL_STRING);
        }

        /// <summary>
        /// Expects the collection to have more than one item
        /// and to have any variance (ie at least two items are
        /// not .Equal)
        /// </summary>
        /// <param name="to"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Vary<T>(
            this ICollectionTo<T> to,
            Func<string> customMessageGenerator
        )
        {
            return to.AssertVariance(customMessageGenerator);
        }

        /// <summary>
        /// Expects the collection _not_ to vary
        /// </summary>
        /// <param name="to"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Vary<T>(
            this ICollectionToAfterNot<T> to
        )
        {
            return to.Vary(NULL_STRING);
        }

        /// <summary>
        /// Expects the collection _not_ to vary
        /// </summary>
        /// <param name="to"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Vary<T>(
            this ICollectionToAfterNot<T> to,
            string customMessage
        )
        {
            return to.Vary(() => NULL_STRING);
        }


        /// <summary>
        /// Expects the collection _not_ to vary
        /// </summary>
        /// <param name="to"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Vary<T>(
            this ICollectionToAfterNot<T> to,
            Func<string> customMessageGenerator
        )
        {
            return to.AssertVariance(() => NULL_STRING);
        }

        /// <summary>
        /// Expects the collection _not_ to vary
        /// </summary>
        /// <param name="to"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Vary<T>(
            this ICollectionNotAfterTo<T> to
        )
        {
            return to.Vary(NULL_STRING);
        }

        /// <summary>
        /// Expects the collection _not_ to vary
        /// </summary>
        /// <param name="to"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Vary<T>(
            this ICollectionNotAfterTo<T> to,
            string customMessage
        )
        {
            return to.Vary(() => NULL_STRING);
        }

        /// <summary>
        /// Expects the collection _not_ to vary
        /// </summary>
        /// <param name="to"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Vary<T>(
            this ICollectionNotAfterTo<T> to,
            Func<string> customMessageGenerator
        )
        {
            return to.AssertVariance(() => NULL_STRING);
        }

        private static IMore<IEnumerable<T>> AssertVariance<T>(
            this ICanAddMatcher<IEnumerable<T>> continuation,
            Func<string> customMessageGenerator
        )
        {
            return continuation.AddMatcher(actual =>
            {
                if (actual is null)
                {
                    return EnforcedFail("Collection is null");
                }
                
                var asArray = actual as T[] ?? actual.ToArray();
                if (asArray.Length == 0)
                {
                    return EnforcedFail("Collection is empty");
                }

                if (asArray.Length == 1)
                {
                    return EnforcedFail("Collection contains only one item");
                }

                var distinctCount = asArray.Distinct().Count();
                var passed = asArray.Length == distinctCount;

                return new MatcherResult(
                    passed,
                    () => $"Expected collection {passed.AsNot()}to vary:\n{asArray.Stringify()}"
                );
            });
        }


        private static MatcherResult EnforcedFail(
            string message
        )
        {
            return new EnforcedMatcherResult(
                false,
                () => message
            );
        }
    }
}