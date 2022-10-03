using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static Imported.PeanutButter.Utils.PyLike;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect
{
    /// <summary>
    /// Provides testing for reference-checking items in two collections
    /// </summary>
    public static class CollectionItemReferenceMatchers
    {
        /// <summary>
        /// Performs reference-equality testing between two collections
        /// </summary>
        /// <param name="collectionItemsTo"></param>
        /// <param name="other"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Be<T>(
            this ICollectionItemsCanAddMatcher<T> collectionItemsTo,
            IEnumerable<T> other
        )
        {
            return collectionItemsTo.Be(
                other,
                NULL_STRING
            );
        }


        /// <summary>
        /// Performs reference-equality testing between two collections
        /// </summary>
        /// <param name="collectionItemsTo"></param>
        /// <param name="other"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Be<T>(
            this ICollectionItemsCanAddMatcher<T> collectionItemsTo,
            IEnumerable<T> other,
            string customMessage
        )
        {
            return collectionItemsTo.Be(
                other,
                () => customMessage
            );
        }

        /// <summary>
        /// Performs reference-equality testing between two collections
        /// </summary>
        /// <param name="collectionItemsTo"></param>
        /// <param name="other"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<IEnumerable<T>> Be<T>(
            this ICollectionItemsCanAddMatcher<T> collectionItemsTo,
            IEnumerable<T> other,
            Func<string> customMessageGenerator
        )
        {
            return collectionItemsTo.AddMatcher(actual =>
            {
                if (actual is null || other is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        FinalMessageFor(
                            () => "Cannot perform item-wise reference checking when one or more collections is null",
                            customMessageGenerator
                        )
                    );
                }

                var actualArray = actual as T[] ?? actual.ToArray();
                var otherArray = other as T[] ?? other.ToArray();
                if (actualArray.Length != otherArray.Length)
                {
                    return new MatcherResult(
                        false,
                        FinalMessageFor(
                            () => $"Collection sizes do not match: {actualArray.Length} vs {otherArray.Length}",
                            customMessageGenerator
                        )
                    );
                }

                var passed = CollectionsAreItemWiseReferenceEqual(actualArray, otherArray);

                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () =>
                            $"Expected\n{actualArray.Stringify()}\n{passed.AsNot()}to be item-wise reference-equal to\n{otherArray.Stringify()}",
                        customMessageGenerator
                    )
                );
            });
        }

        private static bool CollectionsAreItemWiseReferenceEqual<T>(
            T[] left,
            T[] right
        )
        {
            var result = true;
            foreach (var pair in Zip(left, right))
            {
                if (!ReferenceEquals(pair.Item1, pair.Item2))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}