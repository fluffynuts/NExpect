using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static PeanutButter.Utils.PyLike;
using static NExpect.DeepTestHelpers;

// ReSharper disable PossibleMultipleEnumeration

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect
{
    /// <summary>
    /// Provides extensions for testing equality
    /// </summary>
    public static class EqualityProviderExtensions
    {
        /// <summary>
        /// Performs reference equality checking between your actual and the provided expected value
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(
            this ITo<T> be,
            object expected)
        {
            be.Be(expected, null);
        }

        /// <summary>
        /// Performs reference equality checking between your actual and the provided expected value
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(
            this ITo<T> be,
            object expected,
            string customMessage)
        {
            be.AddMatcher(CreateRefEqualMatcherFor<T>(expected, () => customMessage));
        }

        /// <summary>
        /// Performs reference equality checking between your actual and the provided expected value
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(this IToAfterNot<T> be, object expected)
        {
            be.Be(expected, null);
        }

        /// <summary>
        /// Performs reference equality checking between your actual and the provided expected value
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(
            this IToAfterNot<T> be,
            object expected,
            string customMessage)
        {
            be.AddMatcher(CreateRefEqualMatcherFor<T>(expected, () => customMessage));
        }

        /// <summary>
        /// Performs reference equality checking between your actual and the provided expected value
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(
            this ICollectionTo<T> be,
            object expected)
        {
            be.Be(expected, NULL_STRING);
        }

        /// <summary>
        /// Performs reference equality checking between your actual and the provided expected value
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(
            this ICollectionTo<T> be,
            object expected,
            string customMessage)
        {
            be.Be(expected, () => customMessage);
        }

        /// <summary>
        /// Performs reference equality checking between your actual and the provided expected value
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(
            this ICollectionTo<T> be,
            object expected,
            Func<string> customMessageGenerator)
        {
            be.AddMatcher(CreateCollectionRefEqualMatcherFor<T>(expected, customMessageGenerator));
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ITo<T> continuation,
            T expected
        )
        {
            continuation.Equal(expected, null);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ITo<T> continuation,
            T? expected
        ) where T : struct
        {
            continuation.Equal(expected, null);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to add into failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ITo<T> continuation,
            T? expected,
            string customMessage
        ) where T : struct
        {
            continuation.AddMatcher(
                GenerateNullableEqualityMatcherFor(expected, customMessage)
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this IToAfterNot<T> continuation,
            T expected
        )
        {
            continuation.Equal(expected, null);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this IToAfterNot<T> continuation,
            T expected,
            string customMessage
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(expected, customMessage)
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this IToAfterNot<T> continuation,
            T? expected
        ) where T : struct
        {
            continuation.Equal(expected, null);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this IToAfterNot<T> continuation,
            T? expected,
            string customMessage
        ) where T : struct
        {
            continuation.AddMatcher(
                GenerateNullableEqualityMatcherFor(expected, customMessage)
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this INotAfterTo<T> continuation,
            T expected
        )
        {
            continuation.Equal(expected, null);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this INotAfterTo<T> continuation,
            T expected,
            string customMessage
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(expected, customMessage)
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this INotAfterTo<T> continuation,
            T? expected
        ) where T : struct
        {
            continuation.Equal(expected, null);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this INotAfterTo<T> continuation,
            T? expected,
            string customMessage
        ) where T : struct
        {
            continuation.AddMatcher(
                GenerateNullableEqualityMatcherFor(expected, customMessage)
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
            this ITo<T> continuation,
            T expected,
            string customMessage
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(expected, customMessage)
            );
        }

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
            continuation.Equal(expected, null);
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
            continuation.AddMatcher(
                MakeCollectionIntersectionEqualMatcherFor(expected, customMessage)
            );
        }

        /// <summary>
        /// Does deep-equality testing on two collections, ignoring complex item referencing
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionDeepEqual<T> continuation,
            IEnumerable<T> expected
        )
        {
            continuation.To(expected, null);
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
            continuation.AddMatcher(
                MakeCollectionIntersectionEqualMatcherFor(expected, customMessage)
            );
        }

        /// <summary>
        /// Does deep-equivalence testing on two collections, ignoring complex item referencing.
        /// Two collections are deep-equivalent when their object data matches, but not necessarily
        /// in order.
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionDeepEquivalent<T> continuation,
            IEnumerable<T> expected
        )
        {
            continuation.To(expected, null);
        }

        /// <summary>
        /// Does deep-equivalence testing on two collections, ignoring complex item referencing.
        /// Two collections are deep-equivalent when their object data matches, but not necessarily
        /// in order.
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Collection to match</param>
        /// <param name="customMessage">Custom message to add when failing</param>
        /// <typeparam name="T">Collection item type</typeparam>
        public static void To<T>(
            this ICollectionDeepEquivalent<T> continuation,
            IEnumerable<T> expected,
            string customMessage
        )
        {
            continuation.AddMatcher(
                collection =>
                {
                    var passed = CollectionsAreDeepEquivalent(collection, expected);
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
                            customMessage
                        ));
                });
        }

        /// <summary>
        /// Provides deep intersection-equality testing for two collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected values</param>
        /// <typeparam name="T">Original type of collection</typeparam>
        public static void To<T>(
            this ICollectionIntersectionEquivalent<T> continuation,
            IEnumerable<T> expected
        )
        {
            continuation.To(expected, null);
        }

        /// <summary>
        /// Provides deep intersection-equality testing for two collections
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected values</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Original type of collection</typeparam>
        public static void To<T>(
            this ICollectionIntersectionEquivalent<T> continuation,
            IEnumerable<T> expected,
            string customMessage
        )
        {
            continuation.AddMatcher(
                collection =>
                {
                    var passed = CollectionsAreIntersectionEquivalent(collection, expected);
                    return new MatcherResult(
                        passed,
                        () => FinalMessageFor(
                            new[]
                            {
                                "Expected",
                                collection.LimitedPrint(),
                                $"{passed.AsNot()} to be intersection equivalent to",
                                expected.LimitedPrint()
                            },
                            customMessage
                        )
                    );
                });
        }

        private static bool CollectionsAreIntersectionEquivalent<T>(
            IEnumerable<T> collection,
            IEnumerable<T> expected
        )
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
                            compare.FirstOrDefault(c => AreIntersectionEqual(currentMaster, c));
                        if (compareMatch == null)
                            return false;
                        master.Remove(currentMaster);
                        compare.Remove(compareMatch);
                    }

                    return true;
                });
        }

        private static Func<T, IMatcherResult> GenerateNullableEqualityMatcherFor<T>(
            T? expected,
            string customMessage
        ) where T : struct
        {
            return actual =>
            {
                var nullableActual = actual as T?;
                return CompareForEquality(nullableActual, expected, customMessage);
            };
        }

        private static Func<IEnumerable<T>, IMatcherResult> MakeCollectionIntersectionEqualMatcherFor<T>(
            IEnumerable<T> expected,
            string customMessage
        )
        {
            return collection =>
            {
                var passed = CollectionsAreIntersectionEqual(collection, expected);
                return new MatcherResult(
                    passed,
                    () => FinalMessageFor(
                        new[]
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

        private static bool CollectionsAreDeepEquivalent<T>(
            IEnumerable<T> collection,
            IEnumerable<T> expected)
        {
            return CollectionCompare(
                collection,
                expected,
                (master, compare) =>
                {
                    while (master.Any())
                    {
                        var currentMaster = master.First();
                        var compareMatch = compare.FirstOrDefault(c => AreDeepEqual(currentMaster, c));
                        if (compareMatch == null)
                            return false;
                        master.Remove(currentMaster);
                        compare.Remove(compareMatch);
                    }

                    return true;
                });
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


        internal static Func<T, IMatcherResult> GenerateEqualityMatcherFor<T>(
            T expected,
            string customMessage
        )
        {
            return actual => CompareForEquality(actual, expected, customMessage);
        }


        private static IMatcherResult CompareForEquality<T>(T actual, T expected, string customMessage)
        {
            if (ValuesAreEqual(expected, actual) ||
                BothAreNull(expected, actual))
            {
                return new MatcherResult(
                    true,
                    () => FinalMessageFor(
                        new[]
                        {
                            "Did not expect",
                            Quote(expected),
                            "but got exactly that"
                        },
                        customMessage)
                );
            }

            return new MatcherResult(
                false,
                () => FinalMessageFor(
                    new[]
                    {
                        "Expected",
                        Quote(expected),
                        "but got",
                        Quote(actual)
                    },
                    customMessage
                ));
        }

        private static bool ValuesAreEqual<T>(T expected, T actual)
        {
            var result = actual != null &&
                         actual.Equals(expected);
            if (!result)
                return false;
            if (expected is DateTime expectedDateTime &&
                actual is DateTime actualDateTime)
            {
                return expectedDateTime.Kind == actualDateTime.Kind;
            }

            return true;
        }


        private static bool BothAreNull<T>(T expected, T actual)
        {
            return actual == null && expected == null;
        }

        /// <summary>
        /// Tests if a value is null
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to include when failing</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static IMore<T> Null<T>(
            this IBe<T> continuation,
            string customMessage
        )
        {
            continuation.AddMatcher(
                actual =>
                {
                    var passed = actual == null;
                    return new MatcherResult(
                        passed,
                        () => FinalMessageFor(
                            passed
                                ? new[] {"Expected not to get null"}
                                : new[] {"Expected null but got", Quote(actual)},
                            customMessage)
                    );
                });
            return continuation.More();
        }

        /// <summary>
        /// Tests if a value is null
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static IMore<T> Null<T>(this IBe<T> continuation)
        {
            return continuation.Null(null);
        }

        /// <summary>
        /// Last part of the .To.Be.Equal.To() chain
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Object type being tested</typeparam>
        public static void To<T>(
            this IEqualityContinuation<T> continuation,
            T expected
        )
        {
            continuation.To(expected, null);
        }

        /// <summary>
        /// Last part of the .To.Be.Equal.To() chain
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to include when failing</param>
        /// <typeparam name="T">Object type being tested</typeparam>
        public static void To<T>(
            this IEqualityContinuation<T> continuation,
            T expected,
            string customMessage
        )
        {
            continuation.AddMatcher(
                actual =>
                {
                    var passed = (actual == null && expected == null) ||
                                 (actual?.Equals(expected) ?? false);
                    return new MatcherResult(
                        passed,
                        () => FinalMessageFor(
                            new[]
                            {
                                "Expected",
                                Quote(actual),
                                $"{passed.AsNot()}to equal",
                                Quote(expected)
                            },
                            customMessage
                        ));
                });
        }

        /// <summary>
        /// Tests if a string is empty, with a provided custom error message
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to include when failing</param>
        public static void Empty(this IBe<string> continuation, string customMessage)
        {
            continuation.AddMatcher(
                actual =>
                {
                    var passed = actual == "";
                    var message = passed
                        ? new[] {"Expected not to be empty"}
                        : new[] {"Expected empty string but got", Quote(actual)};
                    return new MatcherResult(
                        passed,
                        () => FinalMessageFor(
                            message,
                            customMessage)
                    );
                });
        }

        /// <summary>
        /// Tests if a string is empty
        /// </summary>
        /// <param name="continuation"></param>
        public static void Empty(this IBe<string> continuation)
        {
            continuation.Empty(null);
        }

        /// <summary>
        /// Tests if a string is null or empty
        /// </summary>
        /// <param name="nullOr"></param>
        public static void Empty(
            this INullOr<string> nullOr
        )
        {
            nullOr.Empty(null);
        }

        /// <summary>
        /// Tests if a string is null or empty
        /// </summary>
        /// <param name="nullOr"></param>
        /// <param name="customMessage">Custom message to add to the final failure message</param>
        public static void Empty(
            this INullOr<string> nullOr,
            string customMessage
        )
        {
            nullOr.AddMatcher(
                actual =>
                {
                    var passed = string.IsNullOrEmpty(actual);
                    return new MatcherResult(
                        passed,
                        () => FinalMessageFor(
                            $"Expected {actual} {passed.AsNot()}to be null or empty",
                            customMessage
                        )
                    );
                });
        }

        /// <summary>
        /// Test if string is null or whitespace
        /// </summary>
        /// <param name="nullOr"></param>
        public static void Whitespace(
            this INullOr<string> nullOr
        )
        {
            nullOr.Whitespace(null);
        }

        /// <summary>
        /// Test if string is null or whitespace
        /// </summary>
        /// <param name="nullOr"></param>
        /// <param name="customMessage">Custom message to add to the final failure message</param>
        public static void Whitespace(
            this INullOr<string> nullOr,
            string customMessage
        )
        {
            nullOr.AddMatcher(
                actual =>
                {
                    var passed = string.IsNullOrWhiteSpace(actual);
                    return new MatcherResult(
                        passed,
                        () => FinalMessageFor(
                            $"Expected {actual} {passed.AsNot()}to be null or whitespace",
                            customMessage
                        )
                    );
                });
        }

        private static Func<T, IMatcherResult> CreateRefEqualMatcherFor<T>(
            object other,
            Func<string> customMessageGenerator)
        {
            return actual => RefCompare(actual, other, customMessageGenerator);
        }

        private static Func<IEnumerable<T>, IMatcherResult> CreateCollectionRefEqualMatcherFor<T>(
            object other,
            Func<string> customMessageGenerator)
        {
            return actual => RefCompare(actual, other, customMessageGenerator);
        }

        private static IMatcherResult RefCompare(object actual, object other, Func<string> customMessageGenerator)
        {
            var passed = ReferenceEquals(actual, other);
            return new MatcherResult(
                passed,
                () => FinalMessageFor(
                    $"Expected {actual} {passed.AsNot()}to be the same reference as {other}",
                    customMessageGenerator()
                )
            );
        }
    }
}