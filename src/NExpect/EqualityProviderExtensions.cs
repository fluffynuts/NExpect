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
        public static void Be<T>(this ITo<T> be, object expected)
        {
            be.AddMatcher(CreateRefEqualMatcherFor<T>(expected));
        }

        /// <summary>
        /// Performs reference equality checking between your actual and the provided expected value
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(this IToAfterNot<T> be, object expected)
        {
            be.AddMatcher(CreateRefEqualMatcherFor<T>(expected));
        }

        /// <summary>
        /// Performs reference equality checking between your actual and the provided expected value
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(this ICollectionTo<T> be, object expected)
        {
            be.AddMatcher(CreateCollectionRefEqualMatcherFor<T>(expected));
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
        ) where T: struct
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
        ) where T: struct
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
        ) where T: struct
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
        ) where T: struct
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
        ) where T: struct
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
            continuation.AddMatcher(collection =>
            {
                var passed = CollectionsAreDeepEquivalent(collection, expected);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        $"Expected \n{collection.PrettyPrint()}\n{passed.AsNot()} to be deep equivalent to\n{expected.PrettyPrint()}",
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
            continuation.AddMatcher(collection =>
            {
                var passed = CollectionsAreIntersectionEquivalent(collection, expected);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        $"Expected\n{collection.PrettyPrint()}\n{passed.AsNot()} to be intersection equivalent to\n{expected.PrettyPrint()}",
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
            return CollectionCompare(collection,
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
        ) where T: struct
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
                    FinalMessageFor(
                        $"Expected\n{collection.PrettyPrint()}\n{passed.AsNot()} to intersect equal\n{expected.PrettyPrint()}",
                        customMessage
                    )
                );
            };
        }

        private static bool CollectionsAreDeepEquivalent<T>(
            IEnumerable<T> collection,
            IEnumerable<T> expected)
        {
            return CollectionCompare(collection,
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


        private static Func<T, IMatcherResult> GenerateEqualityMatcherFor<T>(
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
                return new MatcherResult(true, $"Did not expect {Quote(expected)}, but got exactly that");
            return new MatcherResult(false,
                FinalMessageFor(
                    $"Expected {Quote(expected)} but got {Quote(actual)}",
                    customMessage
                ));
        }

        private static bool ValuesAreEqual<T>(T expected, T actual)
        {
            return actual != null && (actual.Equals(expected));
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
        public static void Null<T>(this IBe<T> continuation, string customMessage)
        {
            continuation.AddMatcher(actual =>
            {
                var passed = actual == null;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        passed
                            ? "Expected not to get null"
                            : $"Expected null but got {Quote(actual)}",
                        customMessage)
                );
            });
        }

        /// <summary>
        /// Tests if a value is null
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Null<T>(this IBe<T> continuation)
        {
            continuation.Null(null);
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
            continuation.AddMatcher(actual =>
            {
                var passed = actual.Equals(expected);
                var message = FinalMessageFor(
                    $"Expected {Quote(actual)} {passed.AsNot()}to equal {Quote(expected)}",
                    customMessage
                );
                return new MatcherResult(passed, message);
            });
        }

        /// <summary>
        /// Tests if a string is empty, with a provided custom error message
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to include when failing</param>
        public static void Empty(this IBe<string> continuation, string customMessage)
        {
            continuation.AddMatcher(actual =>
            {
                var passed = actual == "";
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        passed
                            ? "Expected not to be empty"
                            : $"Expected empty string but got {Quote(actual)}",
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
            nullOr.AddMatcher(actual =>
            {
                var passed = string.IsNullOrEmpty(actual);
                return new MatcherResult(
                    passed,
                    $"Expected {actual} {passed.AsNot()}to be null or empty"
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
            nullOr.AddMatcher(actual =>
            {
                var passed = string.IsNullOrWhiteSpace(actual);
                return new MatcherResult(
                    passed,
                    $"Expected {actual} {passed.AsNot()}to be null or whitespace"
                );
            });
        }

        private static Func<T, IMatcherResult> CreateRefEqualMatcherFor<T>(object other)
        {
            return actual => RefCompare(actual, other);
        }

        private static Func<IEnumerable<T>, IMatcherResult> CreateCollectionRefEqualMatcherFor<T>(object other)
        {
            return actual => RefCompare(actual, other);
        }

        private static IMatcherResult RefCompare(object actual, object other)
        {
            var passed = ReferenceEquals(actual, other);
            return new MatcherResult(
                passed,
                $"Expected {actual} {passed.AsNot()}to be the same reference as {other}"
            );
        }
    }
}