using System;
using System.Collections.Generic;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
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

        // TODO: lock down .Equal to act on ITo<T> and IToAfterNot<T>, as above

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICanAddMatcher<T> continuation, 
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
        /// <param name="customMessage">Custom message to include when failing</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ICanAddMatcher<T> continuation, 
            T expected, 
            string customMessage
        )
        {
            continuation.AddMatcher(actual =>
            {
                if (actual.Equals(expected))
                    return new MatcherResult(true, $"Did not expect {expected}, but got exactly that");
                return new MatcherResult(false,
                    FinalMessageFor(
                        $"Expected {expected} but got {actual}",
                        customMessage
                    ));
            });
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
                var not = passed ? "not " : "";
                var message = FinalMessageFor(
                    $"Expected {Quote(actual)} {not}to equal {Quote(expected)}",
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
                var not = passed ? "not " : "";
                return new MatcherResult(
                    passed,
                    $"Expected {actual} {not}to be null or empty"
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
                var not = passed ? "not " : "";
                return new MatcherResult(
                    passed,
                    $"Expected {actual} {not}to be null or whitespace"
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
            var not = passed ? "not " : "";
            return new MatcherResult(
                passed,
                $"Expected {actual} {not}to be the same reference as {other}"
            );
        }
    }
}