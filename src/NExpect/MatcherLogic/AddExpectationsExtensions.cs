using System;
using NExpect.Interfaces;

namespace NExpect.MatcherLogic
{
    public static class AddExpectationsExtensions
    {
        public static void AddMatcher<T>(
            this IContinuation<T> continuation, 
            Func<T, IMatcherResult> matcher)
        {
            AddMatcherPrivate(continuation, matcher);
        }

        public static void AddMatcher(
            this IExceptionMessageContinuation continuation,
            Func<string, IMatcherResult> matcher
        )
        {
            AddMatcherPrivate(continuation, matcher);
        }

        private static void AddMatcherPrivate<T>(
            object continuation,
            Func<T, IMatcherResult> matcher)
        {
            var asContext = continuation as IExpectationContext<T>;
            if (asContext == null)
            {
                throw new InvalidOperationException($"{continuation} does not implement IExpectationContext<T>");
            }
            asContext.RunMatcher(matcher);
        }
    }
}