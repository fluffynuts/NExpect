using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect
{
    /// <summary>
    /// Provides in-built string-testing matcher extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Tests if the value under test contains a given string. May be continued
        /// with ".And"
        /// </summary>
        /// <param name="continuation">Continuation to act on</param>
        /// <param name="search">String value to search for</param>
        /// <returns>IStringContainContinuation onto which you can chain .And</returns>
        public static IStringContainContinuation Contain(
            this ICanAddMatcher<string> continuation,
            string search
        )
        {
            AddContainsMatcherTo(continuation, search);
            return new StringContainContinuation(continuation);
        }

        /// <summary>
        /// Continue testing a string for another substring
        /// </summary>
        /// <param name="continuation">Existing continuation fron a Contain()</param>
        /// <param name="search">string to search for</param>
        /// <returns>IStringContainContinuation onto which you can chain .And</returns>
        public static IStringContainContinuation And(
            this IStringContainContinuation continuation,
            string search
        )
        {
            AddContainsMatcherTo(continuation, search);
            return new StringContainContinuation(continuation);
        }

        private static void AddContainsMatcherTo(
            ICanAddMatcher<string> continuation,
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