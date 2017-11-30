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
                            new[]
                            {
                                "Expected",
                                actual.Stringify(),
                                $"{passed.AsNot()}to deep equal",
                                expected.Stringify()
                            },
                            customMessage
                        )
                    );
                }
            );
        }
    }
}