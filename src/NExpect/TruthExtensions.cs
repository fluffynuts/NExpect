using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
// ReSharper disable UnusedMember.Global

namespace NExpect
{
    /// <summary>
    /// Provides extension methods to test True / False
    /// </summary>
    public static class TruthExtensions
    {
        /// <summary>
        /// Tests if a boolean value is True
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        public static void True(this IBe<bool> continuation)
        {
            continuation.True(null);
        }

        /// <summary>
        /// Tests if a boolean value is True
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="message">Custom message to include on failure</param>
        public static void True(this IBe<bool> continuation, string message)
        {
            TestBoolean(continuation, true, message);
        }

        /// <summary>
        /// Tests if a boolean value is True
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        public static void True(this IBe<bool?> continuation)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            continuation.AddMatcher(TruthTestFor(true as bool?, null));
        }

        /// <summary>
        /// Tests if a boolean value is True
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="message">Custom message to include on failure</param>
        public static void True(this IBe<bool?> continuation, string message)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            continuation.AddMatcher(TruthTestFor(true as bool?, message));
        }

        /// <summary>
        /// Tests if a boolean value is False
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        public static void False(this IBe<bool> continuation)
        {
            continuation.AddMatcher(TruthTestFor(false, null));
        }

        /// <summary>
        /// Tests if a boolean value is False
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="message">Custom message to include on failure</param>
        public static void False(this IBe<bool> continuation, string message)
        {
            continuation.AddMatcher(TruthTestFor(false, message));
        }

        /// <summary>
        /// Tests if a boolean value is False
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        public static void False(this IBe<bool?> continuation)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            continuation.AddMatcher(TruthTestFor(false as bool?, null));
        }

        /// <summary>
        /// Tests if a boolean value is False
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="message">Custom message to include on failure</param>
        public static void False(this IBe<bool?> continuation, string message)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            continuation.AddMatcher(TruthTestFor(false as bool?, message));
        }

        private static void TestBoolean(
            IBe<bool> expectation,
            bool expected,
            string message
        )
        {
            expectation.AddMatcher(TruthTestFor(expected, message));
        }

        private static Func<T, MatcherResult> TruthTestFor<T>(
            T expected, string message
        )
        {
            return actual =>
            {
                if (actual.Equals(expected))
                    return new MatcherResult(true, $"Did not expect {true}");
                return new MatcherResult(
                    false,
                    MessageHelpers.FinalMessageFor(
                        $"Expected {expected} but got {actual}",
                        message
                    ));
            };
        }
    }
}