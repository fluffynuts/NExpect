using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    internal class StringContainContinuation : IStringContainContinuation,
        IExpectationContext<string>
    {
        public IExpectationContext Parent => _expectationContext;
        IExpectationContext<string> IExpectationContext<string>.TypedParent
        {
            get => _expectationContext.TypedParent;
            set => _expectationContext.TypedParent = value;
        }

        private readonly IExpectationContext<string> _expectationContext;

        public StringContainContinuation(ICanAddMatcher<string> continuation)
        {
            _expectationContext = continuation as IExpectationContext<string>;
        }


        public void Negate()
        {
            _expectationContext.Negate();
        }

        public void ResetNegation()
        {
            _expectationContext.ResetNegation();
        }

        public void RunMatcher(Func<string, IMatcherResult> matcher)
        {
            _expectationContext.RunMatcher(matcher);
        }
    }
}