using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NExpect.Exceptions;
using NExpect.Helpers;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect
{
    /// <summary>
    /// Provides extensions for collection expectations
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Short contain, equivalent to .Contain.At.Least.One.Equal.To(x)
        /// -> less expressive, but shorter to type (:
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="search"></param>
        /// <typeparam name="T"></typeparam>
        public static void Contain<T>(
            this ICollectionTo<T> continuation,
            T search
        )
        {
            continuation.Contain(search, NULL_STRING);
        }

        /// <summary>
        /// Short contain, equivalent to .Contain.At.Least.One.Equal.To(x)
        /// -> less expressive, but shorter to type (:
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="search"></param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T"></typeparam>
        public static void Contain<T>(
            this ICollectionTo<T> continuation,
            T search,
            string customMessage
        )
        {
            continuation.Contain(search, () => customMessage);
        }

        /// <summary>
        /// Short contain, equivalent to .Contain.At.Least.One.Equal.To(x)
        /// -> less expressive, but shorter to type (:
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="search"></param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T"></typeparam>
        public static void Contain<T>(
            this ICollectionTo<T> continuation,
            T search,
            Func<string> customMessageGenerator
        )
        {
//            continuation.AddMatcher(
//                collection =>
//                {
//                    var passed = collection.Contains(search);
//                    return new MatcherResult(
//                        passed,
//                        () => $"Expected {collection.Stringify()} {passed.AsNot()}to contain {search.Stringify()}"
//                    );
//                });
            continuation.AddMatcher(
                CreateShortContainMatcherFor(search, customMessageGenerator)
            );
        }

        /// <summary>
        /// Short contain, equivalent to .Contain.At.Least.One.Equal.To(x)
        /// -> less expressive, but shorter to type (:
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="search"></param>
        /// <typeparam name="T"></typeparam>
        public static void Contain<T>(
            this ICollectionToAfterNot<T> continuation,
            T search
        )
        {
            continuation.Contain(search, NULL_STRING);
        }

        /// <summary>
        /// Short contain, equivalent to .Contain.At.Least.One.Equal.To(x)
        /// -> less expressive, but shorter to type (:
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="search"></param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T"></typeparam>
        public static void Contain<T>(
            this ICollectionToAfterNot<T> continuation,
            T search,
            string customMessage
        )
        {
            continuation.Contain(search, () => customMessage);
        }

        /// <summary>
        /// Short contain, equivalent to .Contain.At.Least.One.Equal.To(x)
        /// -> less expressive, but shorter to type (:
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="search"></param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T"></typeparam>
        public static void Contain<T>(
            this ICollectionToAfterNot<T> continuation,
            T search,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                CreateShortContainMatcherFor(search, customMessageGenerator)
            );
        }

        /// <summary>
        /// Short contain, equivalent to .Contain.At.Least.One.Equal.To(x)
        /// -> less expressive, but shorter to type (:
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="search"></param>
        /// <typeparam name="T"></typeparam>
        public static void Contain<T>(
            this ICollectionNotAfterTo<T> continuation,
            T search
        )
        {
            continuation.Contain(search, NULL_STRING);
        }

        /// <summary>
        /// Short contain, equivalent to .Contain.At.Least.One.Equal.To(x)
        /// -> less expressive, but shorter to type (:
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="search"></param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T"></typeparam>
        public static void Contain<T>(
            this ICollectionNotAfterTo<T> continuation,
            T search,
            string customMessage
        )
        {
            continuation.Contain(search, () => customMessage);
        }

        /// <summary>
        /// Short contain, equivalent to .Contain.At.Least.One.Equal.To(x)
        /// -> less expressive, but shorter to type (:
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="search"></param>
        /// <param name="customMessageGenerator">Generats a custom message to add to failure messages</param>
        /// <typeparam name="T"></typeparam>
        public static void Contain<T>(
            this ICollectionNotAfterTo<T> continuation,
            T search,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                CreateShortContainMatcherFor(search, customMessageGenerator)
            );
        }

        private static Func<IEnumerable<T>, IMatcherResult> CreateShortContainMatcherFor<T>(
            T search,
            Func<string> customMessageGenerator
        )
        {
            return collection =>
            {
                var passed = collection.Contains(search);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => new[]
                        {
                            "Expected",
                            collection.LimitedPrint(),
                            $"{passed.AsNot()}to contain",
                            search.Stringify()
                        },
                        customMessageGenerator
                    )
                );
            };
        }

        /// <summary>
        /// Match exactly N elements with following matchers
        /// </summary>
        /// <param name="contain">contain continuation</param>
        /// <param name="howMany">how many items to match</param>
        /// <typeparam name="T">Type of item to match</typeparam>
        /// <returns>continuation be used: .Equal.To()</returns>
        public static ICountMatchContinuation<IEnumerable<T>> Exactly<T>(
            this IContain<IEnumerable<T>> contain,
            int howMany
        )
        {
            CheckContain(contain);
            return new CountMatchContinuation<IEnumerable<T>>(
                contain,
                CountMatchMethods.Exactly,
                howMany
            );
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="contain"></param>
        /// <param name="howMany"></param>
        /// <returns></returns>
        public static ICountMatchContinuationOfStringCollection Exactly(
            this IContain<IEnumerable<string>> contain,
            int howMany)
        {
            CheckContain(contain);
            return new CountMatchContinuationOfStringCollection(
                contain,
                CountMatchMethods.Exactly,
                howMany
            );
        }

        /// <summary>
        /// Checks that collection only contains N number of items.
        /// Continues with ICountMatchContinuation if it does
        /// </summary>
        /// <param name="contain">contain continuation</param>
        /// <param name="howMany">how many items to match</param>
        /// <typeparam name="T">Type of item to match</typeparam>
        /// <returns>continuation be used: .Equal.To()</returns>
        public static ICountMatchContinuation<IEnumerable<T>> Only<T>(
            this IContain<IEnumerable<T>> contain,
            int howMany
        )
        {
            CheckContain(contain);
            CheckOnly(contain, howMany);
            return new CountMatchContinuation<IEnumerable<T>>(
                contain,
                CountMatchMethods.Only,
                howMany
            );
        }

        /// <summary>
        /// Match at least N elements with following matchers
        /// </summary>
        /// <param name="contain">contain continuation</param>
        /// <param name="howMany">how many items to match at least</param>
        /// <typeparam name="T">Type of item to match</typeparam>
        /// <returns>continuation be used: .Equal.To()</returns>
        public static ICountMatchContinuation<IEnumerable<T>> Least<T>(
            this IContainAt<IEnumerable<T>> contain,
            int howMany
        )
        {
            CheckContain(contain);
            return new CountMatchContinuation<IEnumerable<T>>(
                contain,
                CountMatchMethods.Minimum,
                howMany
            );
        }

        /// <summary>
        /// Starts continuation to match any items in the source collection
        /// </summary>
        /// <param name="contain">Continuation to continue from</param>
        /// <typeparam name="T">Type of items in collection</typeparam>
        /// <returns></returns>
        public static ICountMatchContinuation<IEnumerable<T>> Any<T>(
            this IContain<IEnumerable<T>> contain
        )
        {
            return new CountMatchContinuation<IEnumerable<T>>(
                contain,
                CountMatchMethods.Any,
                0
            );
        }

        /// <summary>
        /// Starts continuation to match no items in the source collection
        /// </summary>
        /// <param name="contain">Continuation to continue from</param>
        /// <typeparam name="T">Type of items in collection</typeparam>
        /// <returns></returns>
        public static ICountMatchContinuation<IEnumerable<T>> No<T>(
            this IContain<IEnumerable<T>> contain
        )
        {
            return new CountMatchContinuation<IEnumerable<T>>(
                contain,
                CountMatchMethods.Exactly,
                0
            );
        }

        /// <summary>
        /// Starts continuation to match all items in the source collection
        /// </summary>
        /// <param name="contain">Continuation to continue from</param>
        /// <typeparam name="T">Type of items in collection</typeparam>
        /// <returns></returns>
        public static ICountMatchContinuation<IEnumerable<T>> All<T>(
            this IContain<IEnumerable<T>> contain
        )
        {
            return new CountMatchContinuation<IEnumerable<T>>(
                contain,
                CountMatchMethods.All,
                0
            );
        }


        /// <summary>
        /// Match at most N elements with following matchers
        /// </summary>
        /// <param name="contain">contain continuation</param>
        /// <param name="howMany">how many items to match at most</param>
        /// <typeparam name="T">Type of item to match</typeparam>
        /// <returns>continuation be used: .Equal.To()</returns>
        public static ICountMatchContinuation<IEnumerable<T>> Most<T>(
            this IContainAt<IEnumerable<T>> contain,
            int howMany
        )
        {
            CheckContain(contain);
            return new CountMatchContinuation<IEnumerable<T>>(
                contain,
                CountMatchMethods.Maximum,
                howMany
            );
        }

        /// <summary>
        /// Performs the equality match with the provided limit (exactly, min, max)
        /// on the collection, using {T}.Equals()
        /// </summary>
        /// <param name="countMatch">Count matcher continuation</param>
        /// <param name="search">Thing to search for</param>
        /// <typeparam name="T">Type of underlying continuation</typeparam>
        /// <exception cref="ArgumentNullException">Thrown if the countMatch is null -- mainly to keep the library sane and testable</exception>
        public static void To<T>(
            this ICountMatchEqual<IEnumerable<T>> countMatch,
            T search
        )
        {
            countMatch.To(search, null);
        }

        /// <summary>
        /// Performs the equality match with the provided limit (exactly, min, max)
        /// on the collection, using {T}.Equals()
        /// </summary>
        /// <param name="countMatch">Count matcher continuation</param>
        /// <param name="search">Thing to search for</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <typeparam name="T">Type of underlying continuation</typeparam>
        /// <exception cref="ArgumentNullException">Thrown if the countMatch is null -- mainly to keep the library sane and testable</exception>
        public static void To<T>(
            this ICountMatchEqual<IEnumerable<T>> countMatch,
            T search,
            string customMessage
        )
        {
            if (countMatch == null)
                throw new ArgumentNullException(
                    nameof(countMatch),
                    $"EqualTo<T> cannot extend null ICanAddMatcher<IEnumerable<{typeof(T)}>>");
            countMatch.Continuation.AddMatcher(
                collection =>
                {
                    var asArray = collection.ToArray();
                    var have = countMatch.Method == CountMatchMethods.Any
                        ? (asArray.Any(o => o.Equals(search))
                            ? 1
                            : 0)
                        : asArray.Count(o => o.Equals(search));
                    var passed = _collectionCountMatchStrategies[countMatch.Method](
                        have,
                        countMatch.Method == CountMatchMethods.All
                            ? asArray.Length
                            : countMatch.ExpectedCount
                    );

                    return new MatcherResult(
                        passed,
                        () => FinalMessageFor(
                            _collectionCountMessageStrategies[countMatch.Method](
                                passed,
                                search,
                                have,
                                countMatch.ExpectedCount),
                            customMessage)
                    );
                });
        }


        /// <summary>
        /// Continuation for Matched, allowing testing the artifact with a simple func
        /// </summary>
        /// <param name="countMatch">Matched continuation</param>
        /// <param name="test">Func to test Actual value with; return true for match, false for non-match</param>
        /// <typeparam name="T">Type of artifact being tested</typeparam>
        public static void By<T>(
            this ICountMatchMatched<IEnumerable<T>> countMatch,
            Func<T, bool> test
        )
        {
            countMatch.By(test, NULL_STRING);
        }

        /// <summary>
        /// Continuation for Matched, allowing testing the artifact with a simple func
        /// </summary>
        /// <param name="countMatch">Matched continuation</param>
        /// <param name="test">Func to test Actual value with; return true for match, false for non-match</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <typeparam name="T">Type of artifact being tested</typeparam>
        public static void By<T>(
            this ICountMatchMatched<IEnumerable<T>> countMatch,
            Func<T, bool> test,
            string customMessage
        )
        {
            countMatch.By(test, () => customMessage);
        }

        /// <summary>
        /// Continuation for Matched, allowing testing the artifact with a simple func
        /// </summary>
        /// <param name="countMatch">Matched continuation</param>
        /// <param name="test">Func to test Actual value with; return true for match, false for non-match</param>
        /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
        /// <typeparam name="T">Type of artifact being tested</typeparam>
        public static void By<T>(
            this ICountMatchMatched<IEnumerable<T>> countMatch,
            Func<T, bool> test,
            Func<string> customMessageGenerator
        )
        {
            countMatch.Continuation.AddMatcher(
                collection =>
                {
                    var have = collection.Where(test).Count();
                    var compare = countMatch.Method == CountMatchMethods.All
                        ? collection.Count()
                        : countMatch.Compare;
                    var passed = _collectionCountMatchStrategies[countMatch.Method](have, compare);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => _collectionCountMatchMessageStrategies[countMatch.Method](
                                passed,
                                have,
                                countMatch.Compare),
                            customMessageGenerator));
                });
        }

        /// <summary>
        /// Continuation for Matched, allowing testing the artifact with a simple func
        /// </summary>
        /// <param name="countMatch">Matched continuation</param>
        /// <param name="test">Func to test Actual value with; return true for match, false for non-match</param>
        /// <typeparam name="T">Type of artifact being tested</typeparam>
        public static void By<T>(
            this ICountMatchMatched<IEnumerable<T>> countMatch,
            Func<int, T, bool> test
        )
        {
            countMatch.By(test, NULL_STRING);
        }

        /// <summary>
        /// Continuation for Matched, allowing testing the artifact with a simple func
        /// </summary>
        /// <param name="countMatch">Matched continuation</param>
        /// <param name="test">Func to test Actual value with; return true for match, false for non-match</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <typeparam name="T">Type of artifact being tested</typeparam>
        public static void By<T>(
            this ICountMatchMatched<IEnumerable<T>> countMatch,
            Func<int, T, bool> test,
            string customMessage
        )
        {
            countMatch.By(test, () => customMessage);
        }

        /// <summary>
        /// Continuation for Matched, allowing testing the artifact with a simple func
        /// </summary>
        /// <param name="countMatch">Matched continuation</param>
        /// <param name="test">Func to test Actual value with; return true for match, false for non-match</param>
        /// <param name="customMessageGenerator">Custom message to include in failure messages</param>
        /// <typeparam name="T">Type of artifact being tested</typeparam>
        public static void By<T>(
            this ICountMatchMatched<IEnumerable<T>> countMatch,
            Func<int, T, bool> test,
            Func<string> customMessageGenerator
        )
        {
            countMatch.Continuation.AddMatcher(
                collection =>
                {
                    var idx = 0;
                    var have = collection.Select(
                            o => new
                            {
                                o,
                                idx = idx++
                            })
                        .Count(o => test(o.idx, o.o));
                    var compare = countMatch.Method == CountMatchMethods.All
                        ? collection.Count()
                        : countMatch.Compare;
                    var passed = _collectionCountMatchStrategies[countMatch.Method](have, compare);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => _collectionCountMatchMessageStrategies[countMatch.Method](
                                passed,
                                have,
                                countMatch.Compare),
                            customMessageGenerator));
                });
        }

        /// <summary>
        /// Tests if a collection is empty from the continuation
        /// </summary>
        /// <param name="be">ICollectionBe&lt;T&gt; continuation</param>
        /// <typeparam name="T">Item type of the collection being tested</typeparam>
        public static void Empty<T>(
            this ICollectionBe<T> be
        )
        {
            be.Empty(NULL_STRING);
        }

        /// <summary>
        /// Tests if a collection is empty from the continuation
        /// </summary>
        /// <param name="be">ICollectionBe&lt;T&gt; continuation</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <typeparam name="T">Item type of the collection being tested</typeparam>
        public static void Empty<T>(
            this ICollectionBe<T> be,
            string customMessage
        )
        {
            be.Empty(() => customMessage);
        }

        /// <summary>
        /// Tests if a collection is empty from the continuation
        /// </summary>
        /// <param name="be">ICollectionBe&lt;T&gt; continuation</param>
        /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
        /// <typeparam name="T">Item type of the collection being tested</typeparam>
        public static void Empty<T>(
            this ICollectionBe<T> be,
            Func<string> customMessageGenerator
        )
        {
            be.AddMatcher(
                collection =>
                {
                    var passed = collection != null && !collection.Any();
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => new[]
                            {
                                "Expected",
                                collection.LimitedPrint(),
                                $"{passed.AsNot()}to be an empty collection"
                            },
                            customMessageGenerator
                        )
                    );
                });
        }


        /// <summary>
        /// Tests equivalence with another collection
        /// </summary>
        /// <param name="equivalent">continuation for test</param>
        /// <param name="other">collection to test against</param>
        /// <typeparam name="T"></typeparam>
        public static void To<T>(
            this ICollectionEquivalent<T> equivalent,
            IEnumerable<T> other
        )
        {
            equivalent.To(other, NULL_STRING);
        }

        /// <summary>
        /// Tests equivalence with another collection
        /// </summary>
        /// <param name="equivalent">continuation for test</param>
        /// <param name="other">collection to test against</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <typeparam name="T"></typeparam>
        public static void To<T>(
            this ICollectionEquivalent<T> equivalent,
            IEnumerable<T> other,
            string customMessage
        )
        {
            equivalent.To(other, () => customMessage);
        }

        /// <summary>
        /// Tests equivalence with another collection
        /// </summary>
        /// <param name="equivalent">continuation for test</param>
        /// <param name="other">collection to test against</param>
        /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
        /// <typeparam name="T"></typeparam>
        public static void To<T>(
            this ICollectionEquivalent<T> equivalent,
            IEnumerable<T> other,
            Func<string> customMessageGenerator
        )
        {
            equivalent.To(other, null as IEqualityComparer<T>, customMessageGenerator);
        }

        /// <summary>
        /// Tests equivalence with another collection
        /// </summary>
        /// <param name="equivalent">continuation for test</param>
        /// <param name="other">collection to test against</param>
        /// <param name="comparer">Custom equality comparer for each item</param>
        /// <typeparam name="T"></typeparam>
        public static void To<T>(
            this ICollectionEquivalent<T> equivalent,
            IEnumerable<T> other,
            IEqualityComparer<T> comparer
        )
        {
            equivalent.To(other, comparer, NULL_STRING);
        }

        /// <summary>
        /// Tests equivalence with another collection
        /// </summary>
        /// <param name="equivalent">continuation for test</param>
        /// <param name="other">collection to test against</param>
        /// <param name="comparer">Custom equality comparer for each item</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <typeparam name="T"></typeparam>
        public static void To<T>(
            this ICollectionEquivalent<T> equivalent,
            IEnumerable<T> other,
            IEqualityComparer<T> comparer,
            string customMessage
        )
        {
            equivalent.To(other, comparer, () => customMessage);
        }

        /// <summary>
        /// Tests equivalence with another collection
        /// </summary>
        /// <param name="equivalent">continuation for test</param>
        /// <param name="other">collection to test against</param>
        /// <param name="comparer">Custom equality comparer for each item</param>
        /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
        /// <typeparam name="T"></typeparam>
        public static void To<T>(
            this ICollectionEquivalent<T> equivalent,
            IEnumerable<T> other,
            IEqualityComparer<T> comparer,
            Func<string> customMessageGenerator
        )
        {
            equivalent.AddMatcher(
                collection =>
                {
                    var passed = TestEquivalenceOf(collection, other, comparer ?? new DefaultEqualityComparer<T>());
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => new[]
                            {
                                "Expected",
                                collection.LimitedPrint(),
                                $"{passed.AsNot()}to be equivalent to",
                                other.LimitedPrint()
                            },
                            customMessageGenerator
                        )
                    );
                });
        }

        /// <summary>
        /// Tests equivalence with another collection
        /// </summary>
        /// <param name="equivalent">continuation for test</param>
        /// <param name="other">collection to test against</param>
        /// <param name="comparer">Custom equality comparer function for each item</param>
        /// <typeparam name="T"></typeparam>
        public static void To<T>(
            this ICollectionEquivalent<T> equivalent,
            IEnumerable<T> other,
            Func<T, T, bool> comparer
        )
        {
            equivalent.To(
                other,
                comparer,
                NULL_STRING);
        }

        /// <summary>
        /// Tests equivalence with another collection
        /// </summary>
        /// <param name="equivalent">continuation for test</param>
        /// <param name="other">collection to test against</param>
        /// <param name="comparer">Custom equality comparer function for each item</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <typeparam name="T"></typeparam>
        public static void To<T>(
            this ICollectionEquivalent<T> equivalent,
            IEnumerable<T> other,
            Func<T, T, bool> comparer,
            string customMessage
        )
        {
            equivalent.To(
                other,
                comparer,
                () => customMessage);
        }

        /// <summary>
        /// Tests equivalence with another collection
        /// </summary>
        /// <param name="equivalent">continuation for test</param>
        /// <param name="other">collection to test against</param>
        /// <param name="comparer">Custom equality comparer function for each item</param>
        /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
        /// <typeparam name="T"></typeparam>
        public static void To<T>(
            this ICollectionEquivalent<T> equivalent,
            IEnumerable<T> other,
            Func<T, T, bool> comparer,
            Func<string> customMessageGenerator
        )
        {
            equivalent.To(
                other,
                new FuncComparer<T>(comparer),
                customMessageGenerator);
        }

        /// <summary>
        /// Tests whether or not a collection is null
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Null<T>(
            this ICollectionBe<T> be
        )
        {
            be.Null(NULL_STRING);
        }

        /// <summary>
        /// Tests whether or not a collection is null
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="customMessage">Provide a custom message to include when the matcher fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Null<T>(
            this ICollectionBe<T> be,
            string customMessage
        )
        {
            be.Null(() => customMessage);
        }

        /// <summary>
        /// Tests whether or not a collection is null
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="customMessageGenerator">Provide a custom message to include when the matcher fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Null<T>(
            this ICollectionBe<T> be,
            Func<string> customMessageGenerator
        )
        {
            be.AddMatcher(
                collection =>
                {
                    var passed = collection == null;
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => new[]
                            {
                                "Expected",
                                collection.LimitedPrint(),
                                $"{passed.AsNot()}to be null"
                            },
                            customMessageGenerator)
                    );
                });
        }

        /// <summary>
        /// Tests whether or not a collection contains unique items
        /// </summary>
        /// <param name="unique">Continuation to operate on</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Items<T>(
            this ICollectionUnique<T> unique
        )
        {
            unique.Items(NULL_STRING);
        }


        /// <summary>
        /// Tests whether or not a collection contains unique items
        /// </summary>
        /// <param name="unique">Continuation to operate on</param>
        /// <param name="customMessage">Provide a custom message to include when the matcher fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Items<T>(
            this ICollectionUnique<T> unique,
            string customMessage
        )
        {
            unique.Items(() => customMessage);
        }

        /// <summary>
        /// Tests whether or not a collection contains unique items
        /// </summary>
        /// <param name="unique">Continuation to operate on</param>
        /// <param name="customMessageGenerator">Generates a custom message to include when the matcher fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Items<T>(
            this ICollectionUnique<T> unique,
            Func<string> customMessageGenerator
        )
        {
            CheckDistinct(unique, customMessageGenerator);
        }

        /// <summary>
        /// Tests whether or not a collection contains unique items
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Distinct<T>(
            this ICollectionBe<T> be
        )
        {
            be.Distinct(NULL_STRING);
        }

        /// <summary>
        /// Tests whether or not a collection contains unique items
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="customMessage">Provide a custom message to include when the matcher fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Distinct<T>(
            this ICollectionBe<T> be,
            string customMessage
        )
        {
            be.Distinct(() => customMessage);
        }

        /// <summary>
        /// Tests whether or not a collection contains unique items
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="customMessageGenerator">Generates a custom message to include when the matcher fails</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void Distinct<T>(
            this ICollectionBe<T> be,
            Func<string> customMessageGenerator
        )
        {
            CheckDistinct(be, customMessageGenerator);
        }

        /// <summary>
        /// Tests for the presence of any items, using the count matcher that preceded
        /// </summary>
        /// <param name="contain">Collection to test</param>
        /// <typeparam name="T">Item type of the collection</typeparam>
        public static void Items<T>(
            this ICountMatchContinuation<IEnumerable<T>> contain
        )
        {
            contain.Items(NULL_STRING);
        }

        /// <summary>
        /// Tests for the presence of any items, using the count matcher that preceded
        /// </summary>
        /// <param name="contain">Collection to test</param>
        /// <typeparam name="T">Item type of the collection</typeparam>
        public static void Item<T>(
            this ICountMatchContinuation<IEnumerable<T>> contain
        )
        {
            contain.Item(NULL_STRING);
        }

        /// <summary>
        /// Tests for the presence of any items, using the count matcher that preceded
        /// </summary>
        /// <param name="contain">Collection to test</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <typeparam name="T">Item type of the collection</typeparam>
        public static void Item<T>(
            this ICountMatchContinuation<IEnumerable<T>> contain,
            string customMessage
        )
        {
            contain.Item(() => customMessage);
        }

        /// <summary>
        /// Tests for the presence of any items, using the count matcher that preceded
        /// </summary>
        /// <param name="contain">Collection to test</param>
        /// <param name="customMessageGenerator">Custom message to include in failure messages</param>
        /// <typeparam name="T">Item type of the collection</typeparam>
        public static void Item<T>(
            this ICountMatchContinuation<IEnumerable<T>> contain,
            Func<string> customMessageGenerator
        )
        {
            contain.Items(customMessageGenerator);
        }

        /// <summary>
        /// Tests for the presence of any items, using the count matcher that preceded
        /// </summary>
        /// <param name="contain">Collection to test</param>
        /// <param name="customMessage">Generates a custom message to include in failure messages</param>
        /// <typeparam name="T">Item type of the collection</typeparam>
        public static void Items<T>(
            this ICountMatchContinuation<IEnumerable<T>> contain,
            string customMessage
        )
        {
            contain.Items(() => customMessage);
        }

        /// <summary>
        /// Tests for the presence of any items, using the count matcher that preceded
        /// </summary>
        /// <param name="contain">Collection to test</param>
        /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
        /// <typeparam name="T">Item type of the collection</typeparam>
        public static void Items<T>(
            this ICountMatchContinuation<IEnumerable<T>> contain,
            Func<string> customMessageGenerator
        )
        {
            contain.AddMatcher(
                collection =>
                {
                    var expected = contain.ExpectedCount;
                    var actual = collection?.Count() ?? 0;
                    var passed = actual == expected;
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => $"Expected {passed.AsNot()}to find {expected} items but actually found {actual}",
                            customMessageGenerator
                        )
                    );
                });
        }

        /// <summary>
        /// Performs count-based deep-equality testing on collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Object to match</param>
        /// <typeparam name="T">Type of collection item</typeparam>
        public static void To<T>(
            this ICountMatchDeepEqual<IEnumerable<T>> continuation,
            object expected
        )
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Performs count-based deep-equality testing on collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Object to match</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <typeparam name="T">Type of collection item</typeparam>
        public static void To<T>(
            this ICountMatchDeepEqual<IEnumerable<T>> continuation,
            object expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Performs count-based deep-equality testing on collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Object to match</param>
        /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
        /// <typeparam name="T">Type of collection item</typeparam>
        public static void To<T>(
            this ICountMatchDeepEqual<IEnumerable<T>> continuation,
            object expected,
            Func<string> customMessageGenerator
        )
        {
            AddEqualityMatcher(
                continuation,
                expected,
                customMessageGenerator,
                DeepTestHelpers.AreDeepEqual);
        }

        /// <summary>
        /// Performs count-based intersection-equality testing on collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Object to match</param>
        /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
        /// <typeparam name="T">Type of collection item</typeparam>
        public static void To<T>(
            this ICountMatchIntersectionEqual<IEnumerable<T>> continuation,
            object expected,
            Func<string> customMessageGenerator
        )
        {
            AddEqualityMatcher(
                continuation,
                expected,
                customMessageGenerator,
                DeepTestHelpers.AreIntersectionEqual);
        }

        /// <summary>
        /// Performs count-based intersection-equality testing on collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Object to match</param>
        /// <typeparam name="T">Type of collection item</typeparam>
        public static void To<T>(
            this ICountMatchIntersectionEqual<IEnumerable<T>> continuation,
            object expected
        )
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Performs count-based intersection-equality testing on collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Object to match</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <typeparam name="T">Type of collection item</typeparam>
        public static void To<T>(
            this ICountMatchIntersectionEqual<IEnumerable<T>> continuation,
            object expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        private static void AddEqualityMatcher<T>(
            ICountMatch<IEnumerable<T>> continuation,
            object expected,
            Func<string> customMessageGenerator,
            Func<object, object, bool> mooCakes)
        {
            continuation.AddMatcher(
                collection =>
                {
                    var actualCount = collection?.Count(
                                          o => mooCakes(o, expected)
                                      ) ?? 0;
                    var passed = CountPassStrategies[continuation.Method](
                        actualCount,
                        continuation.ExpectedCount,
                        collection?.Count() ?? 0
                    );
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => new[]
                            {
                                $"Expected {passed.AsNot()}to find {continuation.ExpectedCount} items matching",
                                expected.Stringify(),
                                $"but actually found {actualCount}"
                            },
                            customMessageGenerator
                        )
                    );
                });
        }

        private static readonly Dictionary<CountMatchMethods, Func<int, int, int, bool>> CountPassStrategies =
            new Dictionary<CountMatchMethods, Func<int, int, int, bool>>
            {
                [CountMatchMethods.All] = (actual, expected, total) => actual == total,
                [CountMatchMethods.Any] = (actual, expected, total) => actual > 0,
                [CountMatchMethods.Exactly] = (actual, expected, total) => actual == expected,
                [CountMatchMethods.Maximum] = (actual, expected, total) => actual <= expected,
                [CountMatchMethods.Minimum] = (actual, expected, total) => actual >= expected,
                [CountMatchMethods.Only] = (actual, expected, total) => actual == expected && actual == total
            };


        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionTo<T> continuation,
            IEnumerable<T> expected
        )
        {
            continuation.Equal(
                expected,
                null as IEqualityComparer<T>,
                NULL_STRING);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionToAfterNot<T> continuation,
            IEnumerable<T> expected
        )
        {
            continuation.Equal(
                expected,
                null as IEqualityComparer<T>,
                NULL_STRING);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionToAfterNot<T> continuation,
            IEnumerable<T> expected,
            string customMessage
        )
        {
            continuation.Equal(expected, () => customMessage);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionToAfterNot<T> continuation,
            IEnumerable<T> expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(expected, null, customMessageGenerator)
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom item equality comparer</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionToAfterNot<T> continuation,
            IEnumerable<T> expected,
            IEqualityComparer<T> comparer
        )
        {
            continuation.Equal(expected, comparer, NULL_STRING);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom item equality comparer</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionToAfterNot<T> continuation,
            IEnumerable<T> expected,
            IEqualityComparer<T> comparer,
            string customMessage
        )
        {
            continuation.Equal(expected, comparer, () => customMessage);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom item equality comparer</param>
        /// <param name="customMessageGenerator">Generate a custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionToAfterNot<T> continuation,
            IEnumerable<T> expected,
            IEqualityComparer<T> comparer,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(expected, comparer, customMessageGenerator)
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom item equality comparer function</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionToAfterNot<T> continuation,
            IEnumerable<T> expected,
            Func<T, T, bool> comparer
        )
        {
            continuation.Equal(
                expected,
                comparer,
                NULL_STRING);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom item equality comparer function</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionToAfterNot<T> continuation,
            IEnumerable<T> expected,
            Func<T, T, bool> comparer,
            string customMessage
        )
        {
            continuation.Equal(
                expected,
                comparer,
                () => customMessage
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom item equality comparer function</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionToAfterNot<T> continuation,
            IEnumerable<T> expected,
            Func<T, T, bool> comparer,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(expected, new FuncComparer<T>(comparer), customMessageGenerator)
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionNotAfterTo<T> continuation,
            IEnumerable<T> expected
        )
        {
            continuation.Equal(
                expected,
                null as IEqualityComparer<T>,
                NULL_STRING);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionNotAfterTo<T> continuation,
            IEnumerable<T> expected,
            string customMessage
        )
        {
            continuation.Equal(expected, () => customMessage);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionNotAfterTo<T> continuation,
            IEnumerable<T> expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(
                    expected,
                    null,
                    customMessageGenerator)
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom comparer to use on items</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionNotAfterTo<T> continuation,
            IEnumerable<T> expected,
            IEqualityComparer<T> comparer
        )
        {
            continuation.Equal(
                expected,
                comparer,
                NULL_STRING);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom comparer to use on items</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionNotAfterTo<T> continuation,
            IEnumerable<T> expected,
            IEqualityComparer<T> comparer,
            string customMessage
        )
        {
            continuation.Equal(expected, comparer, () => customMessage);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom comparer to use on items</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionNotAfterTo<T> continuation,
            IEnumerable<T> expected,
            IEqualityComparer<T> comparer,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(expected, comparer, customMessageGenerator)
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom comparer function to use on items</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionNotAfterTo<T> continuation,
            IEnumerable<T> expected,
            Func<T, T, bool> comparer
        )
        {
            continuation.Equal(expected, comparer, NULL_STRING);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom comparer function to use on items</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionNotAfterTo<T> continuation,
            IEnumerable<T> expected,
            Func<T, T, bool> comparer,
            string customMessage
        )
        {
            continuation.Equal(expected, comparer, () => customMessage);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom comparer function to use on items</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionNotAfterTo<T> continuation,
            IEnumerable<T> expected,
            Func<T, T, bool> comparer,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(
                    expected,
                    new FuncComparer<T>(comparer),
                    customMessageGenerator)
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to include when failing</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionTo<T> continuation,
            IEnumerable<T> expected,
            string customMessage
        )
        {
            continuation.Equal(expected, () => customMessage);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessageGenerator">Generates a custom message to include when failing</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionTo<T> continuation,
            IEnumerable<T> expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(
                    expected,
                    null,
                    customMessageGenerator)
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom item comparer</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionTo<T> continuation,
            IEnumerable<T> expected,
            IEqualityComparer<T> comparer
        )
        {
            continuation.Equal(expected, comparer, NULL_STRING);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom item comparer</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionTo<T> continuation,
            IEnumerable<T> expected,
            IEqualityComparer<T> comparer,
            string customMessage
        )
        {
            continuation.Equal(expected, comparer, () => customMessage);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom item comparer</param>
        /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionTo<T> continuation,
            IEnumerable<T> expected,
            IEqualityComparer<T> comparer,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(GenerateEqualityMatcherFor(expected, comparer, customMessageGenerator));
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom item comparer Func</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionTo<T> continuation,
            IEnumerable<T> expected,
            Func<T, T, bool> comparer
        )
        {
            continuation.Equal(expected, comparer, NULL_STRING);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom item comparer Func</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionTo<T> continuation,
            IEnumerable<T> expected,
            Func<T, T, bool> comparer,
            string customMessage
        )
        {
            continuation.Equal(expected, comparer, () => customMessage);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="comparer">Custom item comparer Func</param>
        /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICollectionTo<T> continuation,
            IEnumerable<T> expected,
            Func<T, T, bool> comparer,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(
                    expected,
                    new FuncComparer<T>(comparer),
                    customMessageGenerator
                )
            );
        }

        /// <summary>
        /// Performs in-order equality testing on items in two collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected collection to match against</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionEqual<T> continuation,
            IEnumerable<T> expected
        )
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Performs in-order equality testing on items in two collections, using
        /// the Ordinal StringComparison
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected collection to match against</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionEqual<T> continuation,
            IEnumerable<T> expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Performs in-order equality testing on items in two collections, using
        /// the Ordinal StringComparison
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected collection to match against</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionEqual<T> continuation,
            IEnumerable<T> expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(
                    expected,
                    null,
                    customMessageGenerator
                ));
        }

        /// <summary>
        /// Tests a collection of strings for the required number of
        /// string items containing the provided substring
        /// </summary>
        /// <param name="continuation">Continuation to act upon</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void Containing(
            this ICountMatchContinuationOfStringCollection continuation,
            string search,
            Func<string> customMessageGenerator
        )
        {
            continuation.Containing(
                search,
                StringComparison.Ordinal,
                customMessageGenerator);
        }

        /// <summary>
        /// Tests a collection of strings for the required number of
        /// string items containing the provided substring, using the
        /// Ordinal StringComparison
        /// </summary>
        /// <param name="continuation">Continuation to act upon</param>
        /// <param name="search">Substring to search for</param>
        public static void Containing(
            this ICountMatchContinuationOfStringCollection continuation,
            string search
        )
        {
            continuation.Containing(
                search,
                StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests a collection of strings for the required number of
        /// string items containing the provided substring, using the
        /// provided StringComparison
        /// </summary>
        /// <param name="continuation">Continuation to act upon</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="comparison">StringComparer to use for locating matches</param>
        public static void Containing(
            this ICountMatchContinuationOfStringCollection continuation,
            string search,
            StringComparison comparison
        )
        {
            continuation.Containing(
                search,
                comparison,
                NULL_STRING);
        }

        /// <summary>
        /// Tests a collection of strings for the required number of
        /// string items containing the provided substring, using the
        /// Ordinal StringComparison
        /// </summary>
        /// <param name="continuation">Continuation to act upon</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="customMessage">Custom message to add when failing</param>
        public static void Containing(
            this ICountMatchContinuationOfStringCollection continuation,
            string search,
            string customMessage
        )
        {
            continuation.Containing(
                search,
                StringComparison.Ordinal,
                customMessage);
        }

        /// <summary>
        /// Tests a collection of strings for the required number of
        /// string items containing the provided substring, using the
        /// provided StringComparison
        /// </summary>
        /// <param name="continuation">Continuation to act upon</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="comparison">StringComparer to use for locating matches</param>
        /// <param name="customMessage">Custom message to add when failing</param>
        public static void Containing(
            this ICountMatchContinuationOfStringCollection continuation,
            string search,
            StringComparison comparison,
            string customMessage
        )
        {
            continuation.Containing(
                search,
                comparison,
                () => customMessage);
        }

        /// <summary>
        /// Tests a collection of strings for the required number of
        /// string items containing the provided substring, using the
        /// provided StringComparison
        /// </summary>
        /// <param name="continuation">Continuation to act upon</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="comparison">StringComparer to use for locating matches</param>
        /// <param name="customMessageGenerator">Custom message to add when failing</param>
        public static void Containing(
            this ICountMatchContinuationOfStringCollection continuation,
            string search,
            StringComparison comparison,
            Func<string> customMessageGenerator
        )
        {
            continuation.Matched.By(
                s => s.IndexOf(search, comparison) > -1,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Searches for strings ending with the provided
        /// search, using the provided comparison, and including
        /// the provided customMessage for failure
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">Substring to search for</param>
        public static void With(
            this ICountMatchContinuationOfStringCollectionEnding continuation,
            string search
        )
        {
            continuation.With(
                search,
                StringComparison.Ordinal);
        }

        /// <summary>
        /// Searches for strings ending with the provided
        /// search, using the provided comparison
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="comparison">Method of string comparison</param>
        public static void With(
            this ICountMatchContinuationOfStringCollectionEnding continuation,
            string search,
            StringComparison comparison
        )
        {
            continuation.With(
                search,
                comparison,
                NULL_STRING
            );
        }

        /// <summary>
        /// Searches for strings ending with the provided
        /// search, using the Ordinal StringComparison, and including
        /// the provided customMessage for failure
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void With(
            this ICountMatchContinuationOfStringCollectionEnding continuation,
            string search,
            string customMessage
        )
        {
            continuation.With(search, () => customMessage);
        }

        /// <summary>
        /// Searches for strings ending with the provided
        /// search, using the Ordinal StringComparison, and including
        /// the provided customMessage for failure
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
        public static void With(
            this ICountMatchContinuationOfStringCollectionEnding continuation,
            string search,
            Func<string> customMessageGenerator
        )
        {
            continuation.With(
                search,
                StringComparison.Ordinal,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Searches for strings ending with the provided
        /// search, using the provided comparison, and including
        /// the provided customMessage for failure
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="comparison">Method of string comparison</param>
        /// <param name="customMessage">Custom message</param>
        public static void With(
            this ICountMatchContinuationOfStringCollectionEnding continuation,
            string search,
            StringComparison comparison,
            string customMessage
        )
        {
            continuation.With(
                search,
                comparison,
                () => customMessage);
        }

        /// <summary>
        /// Searches for strings ending with the provided
        /// search, using the provided comparison, and including
        /// the provided customMessage for failure
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="comparison">Method of string comparison</param>
        /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
        public static void With(
            this ICountMatchContinuationOfStringCollectionEnding continuation,
            string search,
            StringComparison comparison,
            Func<string> customMessageGenerator
        )
        {
            if (!(continuation is CountMatchContinuationOfStringCollectionVerb concrete))
                throw new InvalidOperationException(
                    $".With() for collections of strings only supported where the concrete continuation is of type {typeof(CountMatchContinuationOfStringCollection)}");
            concrete.Wrapped.Matched.By(
                s => s.EndsWith(search, comparison),
                customMessageGenerator
            );
        }


        /// <summary>
        /// Searches for strings starting with the provided
        /// search, using the provided comparison, and including
        /// the provided customMessage for failure
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">Substring to search for</param>
        public static void With(
            this ICountMatchContinuationOfStringCollectionStarting continuation,
            string search
        )
        {
            continuation.With(
                search,
                StringComparison.Ordinal);
        }

        /// <summary>
        /// Searches for strings starting with the provided
        /// search, using the provided comparison
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="comparison">Method of string comparison</param>
        public static void With(
            this ICountMatchContinuationOfStringCollectionStarting continuation,
            string search,
            StringComparison comparison
        )
        {
            continuation.With(
                search,
                comparison,
                NULL_STRING
            );
        }

        /// <summary>
        /// Searches for strings starting with the provided
        /// search, using the Ordinal StringComparison, and including
        /// the provided customMessage for failure
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void With(
            this ICountMatchContinuationOfStringCollectionStarting continuation,
            string search,
            string customMessage
        )
        {
            continuation.With(search, () => customMessage);
        }

        /// <summary>
        /// Searches for strings starting with the provided
        /// search, using the Ordinal StringComparison, and including
        /// the provided customMessage for failure
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void With(
            this ICountMatchContinuationOfStringCollectionStarting continuation,
            string search,
            Func<string> customMessageGenerator
        )
        {
            continuation.With(
                search,
                StringComparison.Ordinal,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Searches for strings starting with the provided
        /// search, using the provided comparison, and including
        /// the provided customMessage for failure
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="comparison">Method of string comparison</param>
        /// <param name="customMessage">Custom message</param>
        public static void With(
            this ICountMatchContinuationOfStringCollectionStarting continuation,
            string search,
            StringComparison comparison,
            string customMessage
        )
        {
            continuation.With(search, comparison, () => customMessage);
        }

        /// <summary>
        /// Searches for strings starting with the provided
        /// search, using the provided comparison, and including
        /// the provided customMessage for failure
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">Substring to search for</param>
        /// <param name="comparison">Method of string comparison</param>
        /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
        public static void With(
            this ICountMatchContinuationOfStringCollectionStarting continuation,
            string search,
            StringComparison comparison,
            Func<string> customMessageGenerator
        )
        {
            if (!(continuation is CountMatchContinuationOfStringCollectionVerb concrete))
                throw new InvalidOperationException(
                    $".With() for collections of strings only supported where the concrete continuation is of type {typeof(CountMatchContinuationOfStringCollection)}");
            concrete.Wrapped.Matched.By(
                s => s.StartsWith(search, comparison),
                customMessageGenerator
            );
        }

        private static Func<IEnumerable<T>, IMatcherResult> GenerateEqualityMatcherFor<T>(
            IEnumerable<T> expected,
            IEqualityComparer<T> comparer,
            Func<string> customMessageGenerator
        )
        {
            return actual =>
            {
                var passed = AllItemsMatchInOrder(actual, expected, comparer ?? new DefaultEqualityComparer<T>());
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => new[]
                        {
                            "Expected collection",
                            actual.LimitedPrint(),
                            $"{passed.AsNot()}to match:",
                            expected.LimitedPrint()
                        },
                        customMessageGenerator)
                );
            };
        }

        private static bool AllItemsMatchInOrder<T>(
            IEnumerable<T> actual,
            IEnumerable<T> expected,
            IEqualityComparer<T> comparer
        )
        {
            if (actual == null &&
                expected == null)
                return true;
            if (actual == null ||
                expected == null)
                return false;
            var actualArray = actual.ToArray();
            var expectedArray = expected.ToArray();
            if (actualArray.Length != expectedArray.Length)
                return false;
            return actualArray.Zip(expectedArray, Tuple.Create)
                .All(o => comparer.Equals(o.Item1, o.Item2));
        }


        private static bool TestEquivalenceOf<T>(
            IEnumerable<T> collectionA,
            IEnumerable<T> collectionB,
            IEqualityComparer<T> comparer
        )
        {
            if (collectionA == null &&
                collectionB == null)
                return true;
            if (collectionA == null ||
                collectionB == null)
                return false;
            var distinctA = collectionA.Distinct().ToArray();
            var distinctB = collectionB.Distinct().ToArray();
            if (distinctA.Length != distinctB.Length)
                return false;
            var countsA = GetCounts(distinctA, collectionA.ToArray());
            var countsB = GetCounts(distinctB, collectionB.ToArray());
            return countsA.Aggregate(
                true,
                (acc, cur) =>
                {
                    if (!acc)
                        return false;
                    var match = countsB.FirstOrDefault(o => comparer.Equals(o.Item1, cur.Item1));
                    return match?.Item2 == cur.Item2;
                });
        }

        private static Tuple<T, int>[] GetCounts<T>(T[] distinctA, T[] collectionA)
        {
            return distinctA
                .Select(o => Tuple.Create(o, collectionA.Count(o2 => AreEqual(o2, o))))
                .ToArray();
        }

        private static bool AreEqual<T>(T left, T right)
        {
            if (left == null &&
                right == null)
                return true;
            if (left == null ||
                right == null)
                return false;
            return left.Equals(right);
        }

        private static void CheckContain<T>(ICanAddMatcher<IEnumerable<T>> contain)
        {
            if (contain == null)
                throw new ArgumentNullException(
                    nameof(contain),
                    $"Exactly<T>() cannot extend null IContain<IEnumerable<{typeof(T).Name}>>");
        }

        private static void CheckOnly<T>(ICanAddMatcher<IEnumerable<T>> contain, int howMany)
        {
            var itemInCollection = contain.GetActual().Count();
            if (itemInCollection == howMany)
                return;
            var s = howMany == 1
                ? ""
                : "s";
            throw new UnmetExpectationException(
                $"Expected to find only {howMany} item{s} in collection, but found {itemInCollection}");
        }

        private static void CheckDistinct<T>(
            ICanAddMatcher<IEnumerable<T>>
                distinct,
            Func<string> customMessageGenerator
        )
        {
            distinct.AddMatcher(
                collection =>
                {
                    var passed = collection.IsDistinct();
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => CreateCheckDistinctMessageFor(
                                collection.LimitedPrint(),
                                passed,
                                collection.IsEmpty()
                            ),
                            customMessageGenerator)
                    );
                });
        }

        private static readonly Dictionary<CountMatchMethods,
            Func<bool, object, int, int, string>> _collectionCountMessageStrategies =
            new Dictionary<CountMatchMethods, Func<bool, object, int, int, string>>()
            {
                [CountMatchMethods.Exactly] = CreateMessageFor("exactly"),
                [CountMatchMethods.Only] = CreateMessageFor("only"),
                [CountMatchMethods.Minimum] = CreateMessageFor("at least"),
                [CountMatchMethods.Maximum] = CreateMessageFor("at most"),
                [CountMatchMethods.Any] = CreateAnyMessage,
                [CountMatchMethods.All] = CreateAllMessage
            };

        private static string CreateAllMessage(bool passed, object search, int have, int want)
        {
            return passed
                ? $"Expected not to find all matching {search}"
                : $"Expected to find all matching {search}";
        }

        private static string CreateAnyMessage(bool passed, object search, int have, int want)
        {
            return passed
                ? $"Expected not to find any matches for {search}"
                : $"Expected to find any match for {search}";
        }

        private static readonly Dictionary<CountMatchMethods,
            Func<bool, int, int, string>> _collectionCountMatchMessageStrategies =
            new Dictionary<CountMatchMethods, Func<bool, int, int, string>>()
            {
                [CountMatchMethods.Exactly] = CreateMatchMessageFor("exactly"),
                [CountMatchMethods.Only] = CreateMatchMessageFor("only"),
                [CountMatchMethods.Minimum] = CreateMatchMessageFor("at least"),
                [CountMatchMethods.Maximum] = CreateMatchMessageFor("at most"),
                [CountMatchMethods.Any] = CreateMatchAnyAllMessageFor("any"),
                [CountMatchMethods.All] = CreateMatchAnyAllMessageFor("all")
            };

        private static Func<bool, int, int, string> CreateMatchAnyAllMessageFor(string comparison)
        {
            return (passed, have, want) =>
            {
                var haveWord = have > 0
                    ? have.ToString()
                    : "none";
                return $"Expected {passed.AsNot()}to find {comparison} matching but found {haveWord}";
            };
        }

        private static readonly Dictionary<CountMatchMethods,
            Func<int, int, bool>> _collectionCountMatchStrategies =
            new Dictionary<CountMatchMethods, Func<int, int, bool>>()
            {
                [CountMatchMethods.Exactly] = (have, want) => have == want,
                [CountMatchMethods.Only] = (have, want) => have == want,
                [CountMatchMethods.Minimum] = (have, want) => have >= want,
                [CountMatchMethods.Maximum] = (have, want) => have <= want,
                [CountMatchMethods.Any] = (have, want) => have > 0,
                [CountMatchMethods.All] = (have, collectionTotal) => have == collectionTotal
            };

        private static Func<bool, object, int, int, string> CreateMessageFor(
            string context
        )
        {
            return (passed, search, have, want) => passed
                ? CreatePassMessageFor(context, search, have, want)
                : CreateFailedMessageFor(context, search, have, want);
        }

        private static Func<bool, int, int, string> CreateMatchMessageFor(
            string context
        )
        {
            return (passed, have, want) => passed
                ? CreatePassMatchMessageFor(context, have, want)
                : CreateFailedMatchMessageFor(context, have, want);
        }

        private static string CreateFailedMessageFor(
            string comparison,
            object search,
            int have,
            int want
        )
        {
            var s = want == 1
                ? ""
                : "s";
            return $"Expected to find {comparison} {want} occurrence{s} of {search.Stringify()} but found {have}";
        }

        private static string CreatePassMessageFor(
            string comparison,
            object search,
            int have,
            int want
        )
        {
            var s = want == 1
                ? ""
                : "s";
            return $"Expected not to find {comparison} {want} occurrence{s} of {search.Stringify()} but found {have}";
        }

        private static string CreateFailedMatchMessageFor(
            string comparison,
            int have,
            int want
        )
        {
            var s = want == 1
                ? ""
                : "es";
            return $"Expected to find {comparison} {want} match{s} but found {have}";
        }

        private static string CreatePassMatchMessageFor(
            string comparison,
            int have,
            int want
        )
        {
            var s = want == 1
                ? ""
                : "es";
            return $"Expected not to find {comparison} {want} match{s} but found {have}";
        }

        private static string CreateCheckDistinctMessageFor(
            string printedCollection,
            bool isDistinct,
            bool isEmpty
        )
        {
            return isDistinct
                ? CreateDistinctMessageFor(printedCollection, isEmpty)
                : CreateNonDistinctMessageFor(printedCollection);
        }

        private static string CreateDistinctMessageFor(
            string printedCollection,
            bool isEmpty
        )
        {
            var empty = isEmpty
                ? ", but found empty collection"
                : "";
            return $"Expected {printedCollection} to contain duplicate items{empty}";
        }

        private static string CreateNonDistinctMessageFor(
            string printedCollection
        )
        {
            return $"Expected {printedCollection} to only contain unique items";
        }
    }
}