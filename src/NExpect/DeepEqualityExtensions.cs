using System;
using System.Linq;
using NExpect.Helpers;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using Imported.PeanutButter.Utils;
using MH = NExpect.Implementations.MessageHelpers;

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
        /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
        /// for customising equality testing for properties of type TProperty</param>
        /// <typeparam name="T">Type of object</typeparam>
        public static void Equal<T>(
            this IDeep<T> continuation,
            object expected,
            params object[] customEqualityComparers
        )
        {
            continuation.Equal(expected, MH.NULL_STRING, customEqualityComparers);
        }

        /// <summary>
        /// Performs deep equality testing on two objects
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
        /// for customising equality testing for properties of type TProperty</param>
        /// <typeparam name="T">Type of object</typeparam>
        public static void Equal<T>(
            this IDeep<T> continuation,
            object expected,
            string customMessage,
            params object[] customEqualityComparers
        )
        {
            continuation.Equal(expected, () => customMessage, customEqualityComparers);
        }

        /// <summary>
        /// Performs deep equality testing on two objects
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Expected value</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
        /// for customising equality testing for properties of type TProperty</param>
        /// <typeparam name="T">Type of object</typeparam>
        public static void Equal<T>(
            this IDeep<T> continuation,
            object expected,
            Func<string> customMessageGenerator,
            params object[] customEqualityComparers
        )
        {
            continuation.AddMatcher(
                actual =>
                {
                    var passed = DeepTestHelpers.AreDeepEqual(
                        actual,
                        expected,
                        customEqualityComparers);
                    return new MatcherResult(
                        passed,
                        MH.FinalMessageFor(
                            () =>
                            {
                                var result = new[]
                                {
                                    "Expected",
                                    actual.Stringify(),
                                    $"{MH.AsNot(passed)}to deep equal",
                                    expected.Stringify()
                                };
                                return customEqualityComparers.Any()
                                    ? result
                                        .And("Using custom equality comparers:")
                                        .And(customEqualityComparers.Select(o => o.GetType()).Stringify())
                                    : result;
                            },
                            customMessageGenerator
                        )
                    );
                }
            );
        }
    }
}