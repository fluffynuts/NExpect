using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.Helpers.DeepTestHelpers;
using static PeanutButter.Utils.PyLike;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect
{
    /// <summary>
    /// Provides extensions for intersection equality testing on collections
    /// </summary>
    public static class CollectionIntersectionEqualityExtensions
    {
        /// <summary>
        /// Does intersection-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Equal<T>(
            this ICollectionIntersection<T> continuation,
            IEnumerable<T> expected,
            params object[] customEqualityComparers
        )
        {
            continuation.Equal(expected, NULL_STRING, customEqualityComparers);
        }

        /// <summary>
        /// Does intersection-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customMessage">Custom message to add when failing</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Equal<T>(
            this ICollectionIntersection<T> continuation,
            IEnumerable<T> expected,
            string customMessage,
            params object[] customEqualityComparers
        )
        {
            continuation.Equal(expected, () => customMessage, customEqualityComparers);
        }

        /// <summary>
        /// Does intersection-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customMessageGenerator">Generates a custom message to add when failing</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Equal<T>(
            this ICollectionIntersection<T> continuation,
            IEnumerable<T> expected,
            Func<string> customMessageGenerator,
            params object[] customEqualityComparers
        )
        {
            continuation.AddMatcher(
                MakeCollectionIntersectionEqualMatcherFor(
                    expected,
                    customMessageGenerator,
                    customEqualityComparers)
            );
        }

        /// <summary>
        /// Performs intersection-equality testing on two collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected collection values</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Type of collection item</typeparam>
        public static void To<T>(
            this ICollectionIntersectionEqual<T> continuation,
            IEnumerable<T> expected,
            params object[] customEqualityComparers
        )
        {
            continuation.To(expected, NULL_STRING, customEqualityComparers);
        }

        /// <summary>
        /// Performs intersection-equality testing on two collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected collection values</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Type of collection item</typeparam>
        public static void To<T>(
            this ICollectionIntersectionEqual<T> continuation,
            IEnumerable<T> expected,
            string customMessage,
            params object[] customEqualityComparers
        )
        {
            continuation.To(expected, () => customMessage, customEqualityComparers);
        }

        /// <summary>
        /// Performs intersection-equality testing on two collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected collection values</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Type of collection item</typeparam>
        public static void To<T>(
            this ICollectionIntersectionEqual<T> continuation,
            IEnumerable<T> expected,
            Func<string> customMessageGenerator,
            params object[] customEqualityComparers
        )
        {
            continuation.AddMatcher(
                MakeCollectionIntersectionEqualMatcherFor(
                    expected,
                    customMessageGenerator,
                    customEqualityComparers)
            );
        }


        private static Func<IEnumerable<T>, IMatcherResult> MakeCollectionIntersectionEqualMatcherFor<T>(
            IEnumerable<T> expected,
            Func<string> customMessage,
            params object[] customEqualityComparers
        )
        {
            return collection =>
            {
                var passed = CollectionsAreIntersectionEqual(
                    collection,
                    expected,
                    customEqualityComparers);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => new[]
                        {
                            "Expected",
                            collection.LimitedPrint(),
                            $"{passed.AsNot()} to intersect equal",
                            expected.LimitedPrint()
                        },
                        customMessage
                    )
                );
            };
        }

        private static bool CollectionsAreIntersectionEqual<T>(
            IEnumerable<T> collection,
            IEnumerable<T> expected,
            params object[] customEqualityComparers)
        {
            return CollectionCompare(
                collection,
                expected,
                (master, compare) => Zip(master, compare)
                    .Aggregate(
                        true,
                        (acc, cur) => acc && AreIntersectionEqual(
                                          cur.Item1,
                                          cur.Item2,
                                          customEqualityComparers)
                    )
            );
        }
    }
}