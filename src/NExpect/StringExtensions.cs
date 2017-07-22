using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect
{
    public static class StringExtensions
    {
        public static IStringContainContinuation Contain(
            this IContinuation<string> continuation,
            string search
        )
        {
            AddContainsMatcherTo(continuation, search);
            return new StringContainContinuation(continuation);
        }

        private static void AddContainsMatcherTo(
            IContinuation<string> continuation,
            string search
        )
        {
            continuation.AddMatcher(s =>
            {
                var passed = s?.Contains(search) ?? false;
                return new MatcherResult(
                    passed,
                    MessageHelpers.MessageForContainsResult(
                        passed, s, search
                    )
                );
            });
        }

        public static IStringContainContinuation And(
            this IStringContainContinuation continuation,
            string search
        )
        {
            AddContainsMatcherTo(continuation, search);
            return new StringContainContinuation(continuation);
        }
    }
}