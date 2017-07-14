using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Extensions
{
    public static class StringExtensions
    {
        public static void Contain(
            this IContinuation<string> continuation,
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
    }
}