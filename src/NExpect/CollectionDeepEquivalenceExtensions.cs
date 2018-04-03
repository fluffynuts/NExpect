using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.Helpers.DeepTestHelpers;

namespace NExpect
{
    /// <summary>
    /// Provides extensions for testing collection deep equivalence
    /// </summary>
    public static class CollectionDeepEquivalenceExtensions
    {
        /// <summary>
        /// Does deep-equivalence testing on two collections, ignoring complex item referencing.
        /// Two collections are deep-equivalent when their object data matches, but not necessarily
        /// in order.
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionDeepEquivalent<T> continuation,
            IEnumerable<T> expected,
            params object[] customEqualityComparers
        )
        {
            continuation.To(expected, NULL_STRING, customEqualityComparers);
        }

        /// <summary>
        /// Does deep-equivalence testing on two collections, ignoring complex item referencing.
        /// Two collections are deep-equivalent when their object data matches, but not necessarily
        /// in order.
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customMessage">Custom message to add when failing</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionDeepEquivalent<T> continuation,
            IEnumerable<T> expected,
            string customMessage,
            params object[] customEqualityComparers
        )
        {
            continuation.To(expected, () => customMessage, customEqualityComparers);
        }

        /// <summary>
        /// Does deep-equivalence testing on two collections, ignoring complex item referencing.
        /// Two collections are deep-equivalent when their object data matches, but not necessarily
        /// in order.
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customMessageGenerator">Generates a custom message to add when failing</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionDeepEquivalent<T> continuation,
            IEnumerable<T> expected,
            Func<string> customMessageGenerator,
            params object[] customEqualityComparers
        )
        {
            continuation.AddMatcher(
                collection =>
                {
                    var passed = CollectionsAreDeepEquivalent(
                        collection, expected, customEqualityComparers);
                    return new MatcherResult(
                        passed,
                        () => FinalMessageFor(
                            new[]
                            {
                                "Expected",
                                collection.LimitedPrint(),
                                $"{passed.AsNot()} to be deep equivalent to",
                                expected.LimitedPrint()
                            },
                            customMessageGenerator
                        ));
                });
        }

        private static bool CollectionsAreDeepEquivalent<T>(
            IEnumerable<T> collection,
            IEnumerable<T> expected,
            object[] customEqualityComparers)
        {
            return CollectionCompare(
                collection,
                expected,
                (master, compare) =>
                {
                    while (master.Any())
                    {
                        var currentMaster = master.First();
                        var compareMatch = compare.FirstOrDefault(
                            c => AreDeepEqual(currentMaster, c, customEqualityComparers));
                        if (compareMatch == null)
                            return false;
                        master.Remove(currentMaster);
                        compare.Remove(compareMatch);
                    }

                    return true;
                });
        }
    }
}