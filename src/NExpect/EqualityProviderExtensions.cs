using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect
{
    public static class EqualityProviderExtensions
    {
        public static void Equal<T>(this ICanAddMatcher<T> expectation, T expected)
        {
            expectation.Equal(expected, null);
        }

        public static void Equal<T>(this ICanAddMatcher<T> continuation, T expected, string customMessage)
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

        public static void Null<T>(this IBe<T> continuation)
        {
            continuation.Null(null);
        }

        public static void To<T>(
            this IEqualityContinuation<T> continuation,
            T expected
        ) {
            continuation.To(expected, null);
        }

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
    }
}