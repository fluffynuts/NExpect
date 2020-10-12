using System;
using System.Collections.Generic;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;

namespace NExpect
{
    /// <summary>
    /// Provides matchers for asserting the order of collections
    /// </summary>
    public static class CollectionOrderMatchers
    {
        /// <summary>
        /// Asserts that the given collection is ordered ascending with the
        /// default comparer for T. A collection with less than 2 items will
        /// always fail.
        /// </summary>
        /// <param name="collectionOrdered">Continuation</param>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Ascending<T>(
            this ICollectionOrdered<T> collectionOrdered
        )
        {
            return collectionOrdered.Ascending(
                Comparer<T>.Default
            );
        }

        /// <summary>
        /// Asserts that the given collection is ordered ascending with the
        /// provided comparer for T. A collection with less than 2 items will
        /// always fail.
        /// </summary>
        /// <param name="collectionOrdered">Continuation</param>
        /// <param name="comparer">Comparer for two items of type T,
        /// implementing IComparer&lt;T&gt;</param>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Ascending<T>(
            this ICollectionOrdered<T> collectionOrdered,
            IComparer<T> comparer
        )
        {
            return collectionOrdered.Ascending(comparer, NULL_STRING);
        }

        /// <summary>
        /// Asserts that the given collection is ordered ascending with the
        /// default comparer for T. A collection with less than 2 items will
        /// always fail.
        /// </summary>
        /// <param name="collectionOrdered">Continuation</param>
        /// <param name="customMessage">Custom message to include when assertion fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Ascending<T>(
            this ICollectionOrdered<T> collectionOrdered,
            string customMessage)
        {
            return collectionOrdered.Ascending(
                Comparer<T>.Default,
                customMessage
            );
        }

        /// <summary>
        /// Asserts that the given collection is ordered ascending with the
        /// provided comparer for T. A collection with less than 2 items will
        /// always fail.
        /// </summary>
        /// <param name="collectionOrdered">Continuation</param>
        /// <param name="comparer">Comparer for two items of type T,
        /// implementing IComparer&lt;T&gt;</param>
        /// <param name="customMessage">Custom message to include when assertion fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Ascending<T>(
            this ICollectionOrdered<T> collectionOrdered,
            IComparer<T> comparer,
            string customMessage
        )
        {
            return collectionOrdered.Ascending(
                comparer,
                () => customMessage
            );
        }

        /// <summary>
        /// Asserts that the given collection is ordered ascending with the
        /// default comparer for T. A collection with less than 2 items will
        /// always fail.
        /// </summary>
        /// <param name="collectionOrdered">Continuation</param>
        /// <param name="customMessageGenerator">
        /// Generates a custom message to include when assertion fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Ascending<T>(
            this ICollectionOrdered<T> collectionOrdered,
            Func<string> customMessageGenerator
        )
        {
            return collectionOrdered.Ascending(
                Comparer<T>.Default,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Asserts that the given collection is ordered ascending with the
        /// provided comparer for T. A collection with less than 2 items will
        /// always fail.
        /// </summary>
        /// <param name="collectionOrdered">Continuation</param>
        /// <param name="comparer">Comparer for two items of type T,
        /// implementing IComparer&lt;T&gt;</param>
        /// <param name="customMessageGenerator">
        /// Generates a custom message to include when assertion fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Ascending<T>(
            this ICollectionOrdered<T> collectionOrdered,
            IComparer<T> comparer,
            Func<string> customMessageGenerator
        )
        {
            return collectionOrdered.AddMatcher(
                actual =>
                {
                    return TestOrderingOf(
                        actual,
                        comparer,
                        i => i > 0,
                        customMessageGenerator
                    );
                });
        }
        
        
        // -> descending start
        /// <summary>
        /// Asserts that the given collection is ordered descending with the
        /// default comparer for T. A collection with less than 2 items will
        /// always fail.
        /// </summary>
        /// <param name="collectionOrdered">Continuation</param>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Descending<T>(
            this ICollectionOrdered<T> collectionOrdered
        )
        {
            return collectionOrdered.Descending(
                Comparer<T>.Default
            );
        }

        /// <summary>
        /// Asserts that the given collection is ordered descending with the
        /// provided comparer for T. A collection with less than 2 items will
        /// always fail.
        /// </summary>
        /// <param name="collectionOrdered">Continuation</param>
        /// <param name="comparer">Comparer for two items of type T,
        /// implementing IComparer&lt;T&gt;</param>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Descending<T>(
            this ICollectionOrdered<T> collectionOrdered,
            IComparer<T> comparer
        )
        {
            return collectionOrdered.Descending(comparer, NULL_STRING);
        }

        /// <summary>
        /// Asserts that the given collection is ordered descending with the
        /// default comparer for T. A collection with less than 2 items will
        /// always fail.
        /// </summary>
        /// <param name="collectionOrdered">Continuation</param>
        /// <param name="customMessage">Custom message to include when assertion fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Descending<T>(
            this ICollectionOrdered<T> collectionOrdered,
            string customMessage)
        {
            return collectionOrdered.Descending(
                Comparer<T>.Default,
                customMessage
            );
        }

        /// <summary>
        /// Asserts that the given collection is ordered descending with the
        /// provided comparer for T. A collection with less than 2 items will
        /// always fail.
        /// </summary>
        /// <param name="collectionOrdered">Continuation</param>
        /// <param name="comparer">Comparer for two items of type T,
        /// implementing IComparer&lt;T&gt;</param>
        /// <param name="customMessage">Custom message to include when assertion fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Descending<T>(
            this ICollectionOrdered<T> collectionOrdered,
            IComparer<T> comparer,
            string customMessage
        )
        {
            return collectionOrdered.Descending(
                comparer,
                () => customMessage
            );
        }

        /// <summary>
        /// Asserts that the given collection is ordered descending with the
        /// default comparer for T. A collection with less than 2 items will
        /// always fail.
        /// </summary>
        /// <param name="collectionOrdered">Continuation</param>
        /// <param name="customMessageGenerator">
        /// Generates a custom message to include when assertion fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Descending<T>(
            this ICollectionOrdered<T> collectionOrdered,
            Func<string> customMessageGenerator
        )
        {
            return collectionOrdered.Descending(
                Comparer<T>.Default,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Asserts that the given collection is ordered descending with the
        /// provided comparer for T. A collection with less than 2 items will
        /// always fail.
        /// </summary>
        /// <param name="collectionOrdered">Continuation</param>
        /// <param name="comparer">Comparer for two items of type T,
        /// implementing IComparer&lt;T&gt;</param>
        /// <param name="customMessageGenerator">
        /// Generates a custom message to include when assertion fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Descending<T>(
            this ICollectionOrdered<T> collectionOrdered,
            IComparer<T> comparer,
            Func<string> customMessageGenerator
        )
        {
            return collectionOrdered.AddMatcher(
                actual =>
                {
                    return TestOrderingOf(
                        actual,
                        comparer,
                        i => i < 0,
                        customMessageGenerator
                    );
                });
        }

        private static MatcherResult TestOrderingOf<T>(
            IEnumerable<T> actual,
            IComparer<T> comparer,
            Func<int, bool> failsWhen,
            Func<string> customMessageGenerator
        )
        {
            var itemCount = 0;
            var last = default(T);
            foreach (var item in actual)
            {
                if (itemCount == 0)
                {
                    last = item;
                    itemCount++;
                    continue;
                }

                var thisResult = comparer.Compare(last, item);
                if (failsWhen(thisResult))
                {
                    return new MatcherResult(
                        false,
                        FinalMessageFor(
                            () => $"Expected collection {false.AsNot()}to be ordered ascending",
                            customMessageGenerator
                        )
                    );
                }

                itemCount++;
            }

            if (itemCount < 2)
            {
                var context = actual.GetMetadata<IExpectationContext>(Expectations.METADATA_KEY);
                return new MatcherResult(
                    context.IsNegated(),
                    "Ordering expectations require collections containing at least two items"
                );
            }

            return new MatcherResult(
                true,
                FinalMessageFor(
                    () => $"Expected collection {true.AsNot()}to be ordered ascending",
                    customMessageGenerator
                )
            );
        }
    }
}