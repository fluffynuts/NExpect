using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

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
            return continuation.Contain(search, null);
        }

        /// <summary>
        /// Tests if the value under test contains a given string. May be continued
        /// with ".And"
        /// </summary>
        /// <param name="continuation">Continuation to act on</param>
        /// <param name="search">String value to search for</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <returns>IStringContainContinuation onto which you can chain .And</returns>
        public static IStringContainContinuation Contain(
            this ICanAddMatcher<string> continuation,
            string search,
            string customMessage
        )
        {
            AddContainsMatcherTo(continuation, search, customMessage);
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
            return continuation.And(search, null);
        }

        /// <summary>
        /// Continue testing a string for another substring
        /// </summary>
        /// <param name="continuation">Existing continuation fron a Contain()</param>
        /// <param name="search">string to search for</param>
        /// <param name="customMessage">Custom message to include in failure messages</param>
        /// <returns>IStringContainContinuation onto which you can chain .And</returns>
        public static IStringContainContinuation And(
            this IStringContainContinuation continuation,
            string search,
            string customMessage
        )
        {
            AddContainsMatcherTo(continuation, search, customMessage);
            return new StringContainContinuation(continuation);
        }

        /// <summary>
        /// Tests if a string starts with an expected value
        /// </summary>
        /// <param name="start">Continuation to operate on</param>
        /// <param name="expected">String that is expected at the start of the Actual</param>
        public static IStringMore With(
            this IStringStart start,
            string expected
        )
        {
            return start.With(expected, null);
        }

        /// <summary>
        /// Tests if a string starts with an expected value
        /// </summary>
        /// <param name="start">Continuation to operate on</param>
        /// <param name="expected">String that is expected at the start of the Actual</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IStringMore With(
            this IStringStart start,
            string expected,
            string customMessage
        )
        {
            start.AddMatcher(actual =>
            {
                var passed = actual?.StartsWith(expected) ?? false;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        $"Expected {actual.Stringify()} {passed.AsNot()}to start with {expected.Stringify()}",
                        customMessage
                    )
                );
            });
            return start.More();
        }

        /// <summary>
        /// Tests if a string ends with an expected value
        /// </summary>
        /// <param name="end">Continuation to operate on</param>
        /// <param name="expected">String that is expected at the end of the Actual</param>
        public static IStringMore With(
            this IStringEnd end,
            string expected
        )
        {
            return end.With(expected, null);
        }

        /// <summary>
        /// Tests if a string ends with an expected value
        /// </summary>
        /// <param name="end">Continuation to operate on</param>
        /// <param name="expected">String that is expected at the start of the Actual</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IStringMore With(
            this IStringEnd end,
            string expected,
            string customMessage
        )
        {
            end.AddMatcher(actual =>
            {
                var passed = actual?.EndsWith(expected) ?? false;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        $"Expected {actual.Stringify()} {passed.AsNot()}to end with {expected.Stringify()}",
                        customMessage
                    )
                );
            });
            return end.More();
        }


        private static void AddContainsMatcherTo(
            ICanAddMatcher<string> continuation,
            string search,
            string customMessage
        )
        {
            continuation.AddMatcher(s =>
            {
                var passed = s?.Contains(search) ?? false;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        MessageForContainsResult(
                            passed, s, search
                        ),
                        customMessage
                    )
                );
            });
        }
    }
}