using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.Helpers.DeepTestHelpers;
using static PeanutButter.Utils.PyLike;

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
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Equal<T>(
            this ICollectionIntersection<T> continuation,
            IEnumerable<T> expected
        )
        {
            continuation.Equal(expected, NULL_STRING);
        }

        /// <summary>
        /// Does intersection-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customMessage">Custom message to add when failing</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Equal<T>(
            this ICollectionIntersection<T> continuation,
            IEnumerable<T> expected,
            string customMessage
        )
        {
            continuation.Equal(expected, () => customMessage);
        }

        /// <summary>
        /// Does intersection-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customMessageGenerator">Generates a custom message to add when failing</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Equal<T>(
            this ICollectionIntersection<T> continuation,
            IEnumerable<T> expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                MakeCollectionIntersectionEqualMatcherFor(expected, customMessageGenerator)
            );
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
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Performs intersection-equality testing on two collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected collection values</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of collection item</typeparam>
        public static void To<T>(
            this ICollectionIntersectionEqual<T> continuation,
            IEnumerable<T> expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Performs intersection-equality testing on two collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected collection values</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T">Type of collection item</typeparam>
        public static void To<T>(
            this ICollectionIntersectionEqual<T> continuation,
            IEnumerable<T> expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                MakeCollectionIntersectionEqualMatcherFor(expected, customMessageGenerator)
            );
        }

        
        private static Func<IEnumerable<T>, IMatcherResult> MakeCollectionIntersectionEqualMatcherFor<T>(
            IEnumerable<T> expected,
            Func<string> customMessage
        )
        {
            return collection =>
            {
                var passed = CollectionsAreIntersectionEqual(collection, expected);
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
            IEnumerable<T> expected
        )
        {
            return CollectionCompare(
                collection,
                expected,
                (master, compare) => Zip(master, compare)
                    .Aggregate(
                        true,
                        (acc, cur) => acc && AreIntersectionEqual(cur.Item1, cur.Item2)
                    )
            );
        }




    }
}