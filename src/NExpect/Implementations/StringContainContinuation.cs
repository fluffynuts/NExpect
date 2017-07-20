using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    public class StringContainContinuation : IStringContainContinuation,
        IExpectationContext<string>
    {
        private readonly IExpectationContext<string> _expectationContext;

        public StringContainContinuation(IContinuation<string> continuation)
        {
            _expectationContext = continuation as IExpectationContext<string>;
        }


        public void Negate()
        {
            _expectationContext.Negate();
        }

        public void RunMatcher(Func<string, IMatcherResult> matcher)
        {
            _expectationContext.RunMatcher(matcher);
        }

        IExpectationContext<string> IExpectationContext<string>.Parent
        {
            get => _expectationContext.Parent;
            set => _expectationContext.Parent = value;
        }
    }
}