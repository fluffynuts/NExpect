using System;
using System.Collections.Generic;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

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
            be.Be(expected, NULL_STRING);
        }

        /// <summary>
        /// Performs reference equality checking between your actual and the provided expected value
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(
            this ITo<T> be,
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
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(
            this ITo<T> be,
            object expected,
            Func<string> customMessageGenerator)
        {
            be.AddMatcher(CreateRefEqualMatcherFor<T>(expected, customMessageGenerator));
        }

        /// <summary>
        /// Performs reference equality checking between your actual and the provided expected value
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(this IToAfterNot<T> be, object expected)
        {
            be.Be(expected, NULL_STRING);
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
            be.Be(expected, () => customMessage);
        }

        /// <summary>
        /// Performs reference equality checking between your actual and the provided expected value
        /// </summary>
        /// <param name="be">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T">Type of the object being tested</typeparam>
        public static void Be<T>(
            this IToAfterNot<T> be,
            object expected,
            Func<string> customMessageGenerator)
        {
            be.AddMatcher(CreateRefEqualMatcherFor<T>(expected, customMessageGenerator));
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
            continuation.Equal(expected, NULL_STRING);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ITo<T> continuation,
            T expected,
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
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ITo<T> continuation,
            T? expected
        ) where T : struct
        {
            continuation.Equal(expected, NULL_STRING);
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
            continuation.Equal(expected, () => customMessage);
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessageGenerator">Custom message to add into failure messages</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ITo<T> continuation,
            T? expected,
            Func<string> customMessageGenerator
        ) where T : struct
        {
            continuation.AddMatcher(
                GenerateNullableEqualityMatcherFor(expected, customMessageGenerator)
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
            continuation.Equal(expected, NULL_STRING);
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
            this IToAfterNot<T> continuation,
            T expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(expected, customMessageGenerator)
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
            continuation.Equal(expected, NULL_STRING);
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
            this IToAfterNot<T> continuation,
            T? expected,
            Func<string> customMessageGenerator
        ) where T : struct
        {
            continuation.AddMatcher(
                GenerateNullableEqualityMatcherFor(expected, customMessageGenerator)
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
            continuation.Equal(expected, NULL_STRING);
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
            this INotAfterTo<T> continuation,
            T expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(expected, customMessageGenerator)
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
            continuation.Equal(expected, NULL_STRING);
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
            this INotAfterTo<T> continuation,
            T? expected,
            Func<string> customMessageGenerator
        ) where T : struct
        {
            continuation.AddMatcher(
                GenerateNullableEqualityMatcherFor(expected, customMessageGenerator)
            );
        }

        /// <summary>
        /// Performs equality checking -- the end of .To.Equal()
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessageGenerator">Custom message to include when failing</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static void Equal<T>(
            this ITo<T> continuation,
            T expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                GenerateEqualityMatcherFor(expected, customMessageGenerator)
            );
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
            return continuation.Null(() => customMessage);
        }

        /// <summary>
        /// Tests if a value is null
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="customMessageGenerator">Generates a custom message to include when failing</param>
        /// <typeparam name="T">Type of object being tested</typeparam>
        public static IMore<T> Null<T>(
            this IBe<T> continuation,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                actual =>
                {
                    var passed = actual == null;
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => passed
                                ? new[] {"Expected not to get null"}
                                : new[] {"Expected null but got", Quote(actual)},
                            customMessageGenerator)
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
            return continuation.Null(NULL_STRING);
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
            continuation.To(expected, NULL_STRING);
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
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Last part of the .To.Be.Equal.To() chain
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessageGenerator">Generates a custom message to include when failing</param>
        /// <typeparam name="T">Object type being tested</typeparam>
        public static void To<T>(
            this IEqualityContinuation<T> continuation,
            T expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                actual =>
                {
                    var passed = (actual == null && expected == null) ||
                                 (actual?.Equals(expected) ?? false);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => new[]
                            {
                                "Expected",
                                Quote(actual),
                                $"{passed.AsNot()}to equal",
                                Quote(expected)
                            },
                            customMessageGenerator
                        ));
                });
        }

        /// <summary>
        /// Tests if a string is empty, with a provided custom error message
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to include when failing</param>
        public static void Empty(
            this IBe<string> continuation,
            string customMessage)
        {
            continuation.Empty(() => customMessage);
        }

        /// <summary>
        /// Tests if a string is empty, with a provided custom error message
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="customMessageGenerator">Generates a custom message to include when failing</param>
        public static void Empty(
            this IBe<string> continuation,
            Func<string> customMessageGenerator)
        {
            continuation.AddMatcher(
                actual =>
                {
                    var passed = actual == "";
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => passed
                                ? new[] {"Expected not to be empty"}
                                : new[] {"Expected empty string but got", Quote(actual)},
                            customMessageGenerator)
                    );
                });
        }

        /// <summary>
        /// Tests if a string is empty
        /// </summary>
        /// <param name="continuation"></param>
        public static void Empty(this IBe<string> continuation)
        {
            continuation.Empty(NULL_STRING);
        }

        /// <summary>
        /// Tests if a string is null or empty
        /// </summary>
        /// <param name="nullOr"></param>
        public static void Empty(
            this INullOr<string> nullOr
        )
        {
            nullOr.Empty(NULL_STRING);
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
            nullOr.Empty(() => customMessage);
        }

        /// <summary>
        /// Tests if a string is null or empty
        /// </summary>
        /// <param name="nullOr"></param>
        /// <param name="customMessageGenerator">Generates a custom message to add to the final failure message</param>
        public static void Empty(
            this INullOr<string> nullOr,
            Func<string> customMessageGenerator
        )
        {
            nullOr.AddMatcher(
                actual =>
                {
                    var passed = string.IsNullOrEmpty(actual);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => $"Expected {actual} {passed.AsNot()}to be null or empty",
                            customMessageGenerator
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
            nullOr.Whitespace(NULL_STRING);
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
            nullOr.Whitespace(() => customMessage);
        }

        /// <summary>
        /// Test if string is null or whitespace
        /// </summary>
        /// <param name="nullOr"></param>
        /// <param name="customMessageGenerator">Generates a custom message to add to the final failure message</param>
        public static void Whitespace(
            this INullOr<string> nullOr,
            Func<string> customMessageGenerator
        )
        {
            nullOr.AddMatcher(
                actual =>
                {
                    var passed = string.IsNullOrWhiteSpace(actual);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => $"Expected {actual} {passed.AsNot()}to be null or whitespace",
                            customMessageGenerator
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

        private static Func<T, IMatcherResult> GenerateNullableEqualityMatcherFor<T>(
            T? expected,
            Func<string> customMessage
        ) where T : struct
        {
            return actual =>
            {
                var nullableActual = actual as T?;
                return CompareForEquality(nullableActual, expected, customMessage);
            };
        }

        internal static Func<T, IMatcherResult> GenerateEqualityMatcherFor<T>(
            T expected,
            Func<string> customMessageGenerator
        )
        {
            return actual => CompareForEquality(actual, expected, customMessageGenerator);
        }

        private static IMatcherResult CompareForEquality<T>(
            T actual,
            T expected,
            Func<string> customMessageGenerator)
        {
            if (ValuesAreEqual(expected, actual) ||
                BothAreNull(expected, actual))
            {
                return new MatcherResult(
                    true,
                    FinalMessageFor(
                        () => new[]
                        {
                            "Did not expect",
                            Quote(expected),
                            "but got exactly that"
                        },
                        customMessageGenerator)
                );
            }

            return new MatcherResult(
                false,
                FinalMessageFor(
                    () => new[]
                    {
                        "Expected",
                        Quote(expected),
                        "but got",
                        Quote(actual)
                    },
                    customMessageGenerator
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
    }
}