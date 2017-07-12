using NExpect.Interfaces;
using NExpect.MatcherLogic;

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
                        $"Expected {expected} but got {continuation.Actual}",
                        customMessage
                    ));
            });
        }

        private static string FinalMessageFor(
            string standardMessage,
            string customMessage
        )
        {
            return string.IsNullOrWhiteSpace(customMessage) ? standardMessage : $"{customMessage}\n\n{standardMessage}";
        }
    }
}