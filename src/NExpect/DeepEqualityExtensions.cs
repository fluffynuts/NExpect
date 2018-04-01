using System;
using NExpect.Helpers;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect
{
    /// <summary>
    /// Provides extensions for deep equality testing of objects
    /// </summary>
    public static class DeepEqualityExtensions
    {
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
            continuation.Equal(expected, NULL_STRING);
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
            continuation.Equal(expected, () => customMessage);
        }

        /// <summary>
        /// Performs deep equality testing on two objects
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T">Type of object</typeparam>
        public static void Equal<T>(
            this IDeep<T> continuation,
            object expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                actual =>
                {
                    var passed = DeepTestHelpers.AreDeepEqual(actual, expected);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => new[]
                            {
                                "Expected",
                                actual.Stringify(),
                                $"{passed.AsNot()}to deep equal",
                                expected.Stringify()
                            },
                            customMessageGenerator
                        )
                    );
                }
            );
        }
    }
}