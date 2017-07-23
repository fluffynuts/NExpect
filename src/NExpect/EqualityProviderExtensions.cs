using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

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
                    MessageHelpers.FinalMessageFor(
                        $"Expected {expected} but got {actual}",
                        customMessage
                    ));
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
                        : $"Expected null but got {MessageHelpers.Quote(actual)}"
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
                    ? $"Expected {MessageHelpers.Quote(actual)} not to equal {MessageHelpers.Quote(expected)}"
                    : $"Expected {MessageHelpers.Quote(actual)} to equal {MessageHelpers.Quote(expected)}";
                return new MatcherResult(passed, message);
            });
        }
    }
}