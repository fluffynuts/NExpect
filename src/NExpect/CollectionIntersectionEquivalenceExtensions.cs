using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.Helpers.DeepTestHelpers;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect
{
    /// <summary>
    /// Provides extensions to perform collection intersection equivalence assertions
    /// </summary>
    public static class CollectionIntersectionEquivalenceExtensions
    {
        /// <summary>
        /// Provides deep intersection-equality testing for two collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected values</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Original type of collection</typeparam>
        public static void To<T>(
            this ICollectionIntersectionEquivalent<T> continuation,
            IEnumerable<T> expected,
            params object[] customEqualityComparers
        )
        {
            continuation.To(expected, NULL_STRING, customEqualityComparers);
        }

        /// <summary>
        /// Provides deep intersection-equality testing for two collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected values</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Original type of collection</typeparam>
        public static void To<T>(
            this ICollectionIntersectionEquivalent<T> continuation,
            IEnumerable<T> expected,
            string customMessage,
            params object[] customEqualityComparers
        )
        {
            continuation.To(expected, () => customMessage, customEqualityComparers);
        }

        /// <summary>
        /// Provides deep intersection-equality testing for two collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected values</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
        /// to use when comparing properties of type TProperty</param>
        /// <typeparam name="T">Original type of collection</typeparam>
        public static void To<T>(
            this ICollectionIntersectionEquivalent<T> continuation,
            IEnumerable<T> expected,
            Func<string> customMessageGenerator,
            params object[] customEqualityComparers
        )
        {
            continuation.AddMatcher(
                collection =>
                {
                    var actualArray = collection as T[] ?? collection.ToArray();
                    var expectedArray = expected as T[] ?? expected.ToArray();
                    var passed = CollectionsAreIntersectionEquivalent(
                        actualArray,
                        expectedArray,
                        customEqualityComparers);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => new[]
                            {
                                "Expected",
                                actualArray.LimitedPrint(),
                                $"{passed.AsNot()} to be intersection equivalent to",
                                expectedArray.LimitedPrint()
                            },
                            customMessageGenerator
                        )
                    );
                });
        }

        private static bool CollectionsAreIntersectionEquivalent<T>(
            IEnumerable<T> collection,
            IEnumerable<T> expected,
            object[] customEqualityMatchers)
        {
            return CollectionCompare(
                collection,
                expected,
                (master, compare) =>
                {
                    while (master.Any())
                    {
                        var currentMaster = master.First();
                        var compareMatch =
                            compare.FirstOrDefault(
                                c => AreIntersectionEqual(
                                    currentMaster,
                                    c,
                                    customEqualityMatchers));
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