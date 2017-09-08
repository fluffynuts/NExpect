using System;
using NExpect.MatcherLogic;
// ReSharper disable MemberCanBeProtected.Global

namespace NExpect.Implementations
{
    internal abstract class ExpectationBase
    {
        public bool IsNegated { get; protected set ; }
    }

    internal abstract class ExpectationBase<T>: ExpectationBase
    {

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