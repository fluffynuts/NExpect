using System;
using NExpect.Implementations.Collections;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations.Strings
{
    internal class StringContainContinuation :
        IStringMore, 
        IHasActual<string>,
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
            ContinuationFactory.Create<string, StringAnd>(Actual, this);
    }
}