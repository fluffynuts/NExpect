using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect.Extensions
{
    public static class EqualityExtensions
    {
        public static void Equal<T>(this IContinuation<T> expectation, T expected)
        {
            expectation.Equal(expected, null);
        }

        public static void Equal<T>(this IContinuation<T> continuation, T expected, string customMessage)
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

        public static void Match<T>(
            this IContinuation<T> continuation,
            Func<T, bool> test,
            string customMessage
        )
        {
            continuation.AddMatcher(actual =>
            {
                var passed = test(actual);
                var message = passed
                    ? $"Expected {actual} not to be matched"
                    : $"Expected {actual} to be matched";
                return new MatcherResult(
                    passed, 
                    FinalMessageFor(message, customMessage)
                );
            });
        }

        public static void Null<T>(this IBe<T> continuation)
        {
            continuation.AddMatcher(actual =>
            {
                var passed = actual == null;
                return new MatcherResult(
                    passed,
                    passed
                        ? $"Expected not to get null"
                        : $"Expected null but got {Quote(actual)}"
                );
            });
        }

        public static void To<T>(
            this IEqualityContinuation<T> continuation,
            T expected
        )
        {
            continuation.AddMatcher(actual =>
            {
                var passed = actual.Equals(expected);
                var message = passed
                    ? $"Expected {Quote(actual)} not to equal {Quote(expected)}"
                    : $"Expected {Quote(actual)} to equal {Quote(expected)}";
                return new MatcherResult(passed, message);
            });
        }
    }
}