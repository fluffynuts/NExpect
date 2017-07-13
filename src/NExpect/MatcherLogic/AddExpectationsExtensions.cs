using System;
using NExpect.Interfaces;

namespace NExpect.MatcherLogic
{
    public static class AddExpectationsExtensions
    {
        public static void AddMatcher<T>(
            this IContinuation<T> continuation, 
            Func<T, IMatcherResult> expectation)
        {
            var asContext = continuation as IExpectationContext<T>;
            if (asContext == null)
            {
                throw new InvalidOperationException($"{continuation} does not implement IExpectationContext<T>");
            }
            asContext.RunMatcher(expectation);
        }
    }
}