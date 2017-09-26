using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    internal class StringContainContinuation :
        IStringContainContinuation,
        IExpectationContext<string>
    {
        public IExpectationContext Parent => _expectationContext;

        IExpectationContext<string> IExpectationContext<string>.TypedParent
        {
            get => _expectationContext.TypedParent;
            set => _expectationContext.TypedParent = value;
        }

        private readonly IExpectationContext<string> _expectationContext;
        public string Actual { get; }

        public StringContainContinuation(
            ICanAddMatcher<string> continuation
        )
        {
            Actual = continuation.GetActual();
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

        public IStringAnd And =>
            Factory.Create<string, StringAnd>(Actual, this);
    }
}