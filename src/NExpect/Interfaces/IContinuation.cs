using System;
using NExpect.MatcherLogic;

namespace NExpect.Interfaces
{
    internal interface IExpectationParentContext<T>
    {
        void Negate();
        void RunMatcher(Func<T, IMatcherResult> matcher);
    }

    internal interface IExpectationContext<T> : IExpectationParentContext<T>
    {
        IExpectationContext<T> Parent { get; set; }
    }

    public interface IContinuation<T>
    {
        T Actual { get; }
    }
}