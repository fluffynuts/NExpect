using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect
{
    /// <summary>
    /// Provides extension methods for testing Intersection equality between objects
    /// </summary>
    public static class IntersectionEqualityExtensions
    {
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
            continuation.Equal(expected, NULL_STRING);
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
            continuation.Equal(expected, () => customMessage);
        }

        /// <summary>
        /// Performs intersection-equality testing on two objects
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Object to test against</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="T">Original type</typeparam>
        public static void Equal<T>(
            this IIntersection<T> continuation,
            object expected,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                actual =>
                {
                    var passed = DeepTestHelpers.AreIntersectionEqual(actual, expected);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => new[]
                            {
                                "Expected",
                                actual.Stringify(),
                                $"{passed.AsNot()}to intersection equal",
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