using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using PeanutButter.Utils;
using static NExpect.Implementations.MessageHelpers;
using static PeanutButter.Utils.PyLike;

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
        /// Performs deep equality testing on two objects
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of object</typeparam>
        public static void Equal<T>(
            this IDeep<T> continuation,
            object expected
        )
        {
            continuation.Equal(expected, null);
        }

        /// <summary>
        /// Performs deep equality testing on two objects
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object</typeparam>
        public static void Equal<T>(
            this IDeep<T> continuation,
            object expected,
            string customMessage
        )
        {
            continuation.AddMatcher(
                actual =>
                {
                    var passed = DeepTestHelpers.AreDeepEqual(actual, expected);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            $"Expected {Stringifier.Stringify(actual, MessageHelpers.Null)}\n{passed.AsNot()}to deep equal\n{Stringifier.Stringify(expected, MessageHelpers.Null)}",
                            customMessage
                        )
                    );
                }
            );
        }

        /// <summary>
        /// Performs intersection-equality testing on two objects
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Object to test against</param>
        /// <typeparam name="T">Original type</typeparam>
        public static void Equal<T>(
            this IIntersection<T> continuation,
            object expected
        )
        {
            continuation.Equal(expected, null);
        }

        /// <summary>
        /// Performs intersection-equality testing on two objects
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Object to test against</param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T">Original type</typeparam>
        public static void Equal<T>(
            this IIntersection<T> continuation,
            object expected,
            string customMessage
        )
        {
            continuation.AddMatcher(
                actual =>
                {
                    var passed = DeepTestHelpers.AreIntersectionEqual(actual, expected);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            $"Expected {actual.Stringify()}\n{passed.AsNot()}to intersection equal\n{expected.Stringify()}",
                            customMessage
                        )
                    );
                }
            );
        }

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
                            compare.FirstOrDefault(c => DeepTestHelpers.AreIntersectionEqual(currentMaster, c));
                        if (compareMatch == null)
                            return false;
                        master.Remove(currentMaster);
                        compare.Remove(compareMatch);
                    }
                    return true;
                });
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
                        var compareMatch = compare.FirstOrDefault(c => DeepTestHelpers.AreDeepEqual(currentMaster, c));
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
                        (acc, cur) => acc && DeepTestHelpers.AreIntersectionEqual(cur.Item1, cur.Item2)
                    )
            );
        }

        private static bool CollectionsAreDeepEqual<T>(
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
                        (acc, cur) => acc && DeepTestHelpers.AreDeepEqual(cur.Item1, cur.Item2)
                    )
            );
        }

        private static bool CollectionCompare<T>(
            IEnumerable<T> collection,
            IEnumerable<T> expected,
            Func<List<T>, List<T>, bool> finalComparison
        )
        {
            if (collection == null &&
                expected == null)
                return true;
            if (collection == null ||
                expected == null)
                return false;
            var master = collection.ToList();
            var compare = expected.ToList();
            if (master.Count != compare.Count)
                return false;
            return finalComparison(master, compare);
        }

        private static Func<T, IMatcherResult> GenerateEqualityMatcherFor<T>(
            T expected, string customMessage
        )
        {
            return actual =>
            {
                if (ValuesAreEqual(expected, actual) ||
                    BothAreNull(expected, actual))
                    return new MatcherResult(true, $"Did not expect {Quote(expected)}, but got exactly that");
                return new MatcherResult(false,
                    FinalMessageFor(
                        $"Expected {Quote(expected)} but got {Quote(actual)}",
                        customMessage
                    ));
            };
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

    /// <summary>
    /// Provides some convenience extensions for deep equality testing
    /// </summary>
    public static class DeepEqualityTestingHelperExtensions
    {
        /// <summary>
        /// "Dumbs down" a collection to be of IEnumerable&lt;object&gt; 
        ///   to make deep equality testing convenient on different types
        /// </summary>
        /// <param name="collection">Collection to convert</param>
        /// <typeparam name="T">Original item type</typeparam>
        /// <returns></returns>
        public static IEnumerable<object> AsObjects<T>(this IEnumerable<T> collection)
        {
            return collection.Select(o => o as object);
        }
    }
}