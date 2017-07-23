using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect
{
    public static class MatchProviderExtensions
    {
        // TODO: similarly, allow Func<T, IMatcherResult>
        public static void Match<T>(
            this ITo<T> continuation,
            Func<T, bool> test,
            string customMessage
        )
        {
            continuation.AddMatcher(MatchMatcherFor(test, customMessage));
        }

        private static Func<T, IMatcherResult> MatchMatcherFor<T>(
            Func<T, bool> test,
            string customMessage
        )
        {
            return actual =>
            {
                var passed = test(actual);
                var message = passed
                    ? $"Expected {actual} not to be matched"
                    : $"Expected {actual} to be matched";
                return new MatcherResult(
                    passed,
                    MessageHelpers.FinalMessageFor(message, customMessage)
                );
            };
        }

        public static void Match<T>(
            this ITo<T> continuation,
            Func<T, bool> test
        )
        {
            continuation.Match(test, null);
        }

        public static void Match<T>(
            this IToAfterNot<T> continuation,
            Func<T, bool> test,
            string customMessage
        )
        {
            continuation.AddMatcher(MatchMatcherFor(test, customMessage));
        }

        public static void Match<T>(
            this IToAfterNot<T> continuation,
            Func<T, bool> test
        )
        {
            continuation.Match(test, null);
        }

        public static void Match<T>(
            this INotAfterTo<T> continuation,
            Func<T, bool> test,
            string customMessage
        )
        {
            continuation.AddMatcher(MatchMatcherFor(test, customMessage));
        }

        public static void Match<T>(
            this INotAfterTo<T> continuation,
            Func<T, bool> test
        )
        {
            continuation.Match(test, null);
        }
    }
}