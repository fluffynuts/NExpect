using System;
using NExpect.MatcherLogic;

namespace NExpect.Interfaces
{
    internal interface IExpectationParentContext<T>
    {
        void Negate();
        void ResetNegation();
        void RunMatcher(Func<T, IMatcherResult> matcher);
    }
}