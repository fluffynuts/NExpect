using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.DeepTestHelpers;
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
            continuation.Equal(expected, null);
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
            continuation.AddMatcher(
                MakeCollectionDeepEqualMatcherFor(expected, customMessage)
            );
        }

        /// <summary>
        /// Does deep-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customMessage">Custom message to add when failing</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionDeepEqual<T> continuation,
            IEnumerable<T> expected,
            string customMessage
        )
        {
            continuation.AddMatcher(MakeCollectionDeepEqualMatcherFor(expected, customMessage));
        }

        /// <summary>
        /// Performs intersection-equality testing on two collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected collection values</param>
        /// <typeparam name="T">Type of collection item</typeparam>
        public static void To<T>(
            this ICollectionIntersectionEqual<T> continuation,
            IEnumerable<T> expected
        )
        {
            continuation.To(expected, null);
        }

        private static Func<IEnumerable<T>, IMatcherResult> MakeCollectionDeepEqualMatcherFor<T>(
            IEnumerable<T> expected,
            string customMessage
        )
        {
            return collection =>
            {
                var passed = CollectionsAreDeepEqual(collection, expected);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        $"Expected\n{collection.PrettyPrint()}\n{passed.AsNot()}to deep equal\n{expected.PrettyPrint()}",
                        customMessage
                    )
                );
            };
        }

        private static bool CollectionsAreDeepEqual<T>(
            IEnumerable<T> collection,
            IEnumerable<T> expected
        )
        {
            return CollectionCompare(
                collection,
                expected,
                (master, compare) => master.Zip(compare, (o1, o2) => Tuple.Create(o1, o2))
                    .Aggregate(
                        true,
                        (acc, cur) => acc && AreDeepEqual(cur.Item1, cur.Item2)
                    )
            );
        }

    }
}
