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
            var result = new StringContainContinuation(continuation);
            AddContainsMatcherTo(continuation, search, customMessage, result);
            return result;
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
            var result = new StringContainContinuation(continuation);
            continuation.SetMetadata(SearchOffset, 0); // And will reset the offset -- it's not ordered
            AddContainsMatcherTo(continuation, search, customMessage, result);
            return result;
        }

        /// <summary>
        /// Continue testing a string for another substring from beyond the end of the last match
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">String to search for</param>
        /// <returns></returns>
        public static IStringContainContinuation Then(
            this IStringContainContinuation continuation,
            string search
        )
        {
            return continuation.Then(search, null);
        }

        /// <summary>
        /// Continue testing a string for another substring from beyond the end of the last match
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="search">String to search for</param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IStringContainContinuation Then(
            this IStringContainContinuation continuation,
            string search,
            string customMessage
        )
        {
            var result = new StringContainContinuation(continuation);
            AddContainsMatcherTo(continuation, search, customMessage, result);
            return result;
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


        private const string SearchOffset = "SearchOffset";

        private static void AddContainsMatcherTo(
            ICanAddMatcher<string> continuation,
            string search,
            string customMessage,
            StringContainContinuation next
        )
        {
            continuation.AddMatcher(s =>
            {
                var priorOffset = continuation.GetMetadata<int>(SearchOffset);
                var nextOffset = s?.IndexOf(search, priorOffset) ?? -1;
                if (nextOffset > -1)
                    nextOffset += search?.Length ?? 0;

                next.SetMetadata(SearchOffset, nextOffset);

                var passed = nextOffset > -1;
                var offsetMessage = priorOffset > 0 ? $" after index {priorOffset}" : "";
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        $"Expected {s.Stringify()} {passed.AsNot()}to contain {search.Stringify()}{offsetMessage}",
                        customMessage
                    )
                );
            });
        }
    }
}