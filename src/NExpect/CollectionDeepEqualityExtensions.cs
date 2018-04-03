using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.Helpers.DeepTestHelpers;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect
{
    /// <summary>
    /// Provides extensions for performing deep equality testing on collections
    /// </summary>
    public static class CollectionDeepEqualityExtensions
    {
        /// <summary>
        /// Does deep-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Equal<T>(
            this ICollectionDeep<T> continuation,
            IEnumerable<T> expected
        )
        {
            continuation.Equal(expected, NULL_STRING);
        }

        /// <summary>
        /// Does deep-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customMessage">Custom message to add when failing</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Equal<T>(
            this ICollectionDeep<T> continuation,
            IEnumerable<T> expected,
            string customMessage
        )
        {
            continuation.Equal(expected, () => customMessage);
        }

        /// <summary>
        /// Does deep-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customMessageGenerator">Generates a custom message to add when failing</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Equal<T>(
            this ICollectionDeep<T> continuation,
            IEnumerable<T> expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                MakeCollectionDeepEqualMatcherFor(expected, customMessageGenerator)
            );
        }

        /// <summary>
        /// Does deep-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionDeepEqual<T> continuation,
            IEnumerable<T> expected,
            params object[] customEqualityComparers
        )
        {
            continuation.To(expected, NULL_STRING, customEqualityComparers);
        }

        /// <summary>
        /// Does deep-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customMessage">Custom message to add when failing</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionDeepEqual<T> continuation,
            IEnumerable<T> expected,
            string customMessage,
            params object[] customEqualityComparers
        )
        {
            continuation.To(expected, () => customMessage, customEqualityComparers);
        }

        /// <summary>
        /// Does deep-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customMessage">Generates a custom message to add when failing</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionDeepEqual<T> continuation,
            IEnumerable<T> expected,
            Func<string> customMessage,
            params object[] customEqualityComparers
        )
        {
            continuation.AddMatcher(
                MakeCollectionDeepEqualMatcherFor(
                    expected,
                    customMessage,
                    customEqualityComparers));
        }

        private static Func<IEnumerable<T>, IMatcherResult> MakeCollectionDeepEqualMatcherFor<T>(
            IEnumerable<T> expected,
            Func<string> customMessage,
            params object[] customEqualityComparers)
        {
            return collection =>
            {
                var passed = CollectionsAreDeepEqual(collection, expected, customEqualityComparers);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => new[]
                        {
                            "Expected",
                            collection.LimitedPrint(),
                            $"{passed.AsNot()}to deep equal",
                            expected.LimitedPrint()
                        },
                        customMessage
                    )
                );
            };
        }

        private static bool CollectionsAreDeepEqual<T>(
            IEnumerable<T> collection,
            IEnumerable<T> expected,
            object[] customEqualityComparers
        )
        {
            return CollectionCompare(
                collection,
                expected,
                (master, compare) => master.Zip(compare, Tuple.Create)
                    .Aggregate(
                        true,
                        (acc, cur) => acc && AreDeepEqual(
                                          cur.Item1,
                                          cur.Item2,
                                          customEqualityComparers)
                    )
            );
        }
    }
}