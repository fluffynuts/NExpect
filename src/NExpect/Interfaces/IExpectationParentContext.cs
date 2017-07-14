using System;
using NExpect.MatcherLogic;

namespace NExpect.Interfaces
{
    internal interface IExpectationParentContext<T>
    {
        void Negate();
        void RunMatcher(Func<T, IMatcherResult> matcher);
    }
}