using System;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    internal abstract class ExpectationBase<T>
    {
        public bool IsNegated { get; private set ; }

        public void Negate()
        {
            IsNegated = !IsNegated;
        }

        public void RunMatcher(
            T actual,
            bool negated,
            Func<T, IMatcherResult> matcher)
        {
            MatcherRunner.RunMatcher(actual, negated, matcher);
        }
    }
}