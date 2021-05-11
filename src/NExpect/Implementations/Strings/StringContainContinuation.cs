using System;
using NExpect.Implementations.Collections;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations.Strings
{
    internal class StringContainContinuation :
        ExpectationContextWithLazyActual<string>,
        IStringMore, 
        IHasActual<string>,
        IExpectationContext<string>
    {
        public override IExpectationContext Parent => _expectationContext;

        IExpectationContext<string> IExpectationContext<string>.TypedParent
        {
            get => _expectationContext.TypedParent;
            set => _expectationContext.TypedParent = value;
        }

        private readonly IExpectationContext<string> _expectationContext;

        public StringContainContinuation(
            ICanAddMatcher<string> continuation
        ): base(continuation.GetActual)
        {
            Actual = continuation.GetActual();
            _expectationContext = continuation as IExpectationContext<string>;
        }


        public override void Negate()
        {
            _expectationContext.Negate();
        }

        public override void ResetNegation()
        {
            _expectationContext.ResetNegation();
        }

        public override IMatcherResult RunMatcher(Func<string, IMatcherResult> matcher)
        {
            return _expectationContext.RunMatcher(matcher);
        }

        public IStringAnd And =>
            ContinuationFactory.Create<string, StringAnd>(ActualFetcher, this);
    }
}