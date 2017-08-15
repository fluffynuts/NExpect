using System;
using System.Diagnostics;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect
{
    public static class TruthExtensions
    {
        public static void True(this IBe<bool> expectation)
        {
            expectation.True(null);
        }

        public static void True(this IBe<bool> expectation, string message)
        {
            TestBoolean(expectation, true, message);
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

        public static void True(this IBe<bool?> expectation)
        {
            expectation.AddMatcher(TruthTestFor(true as bool?, null));
        }

        public static void True(this IBe<bool?> expectation, string message)
        {
            expectation.AddMatcher(TruthTestFor(true as bool?, message));
        }

        public static void False(this IBe<bool> expectation)
        {
            expectation.AddMatcher(TruthTestFor(false, null));
        }

        public static void False(this IBe<bool> expectation, string message)
        {
            expectation.AddMatcher(TruthTestFor(false, message));
        }

        public static void False(this IBe<bool?> expectation)
        {
            expectation.AddMatcher(TruthTestFor(false as bool?, null));
        }

        public static void False(this IBe<bool?> expectation, string message)
        {
            expectation.AddMatcher(TruthTestFor(false as bool?, message));
        }
    }
}