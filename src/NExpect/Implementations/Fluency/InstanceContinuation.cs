using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations.Fluency
{
    internal class InstanceContinuation :
        ExpectationContextWithLazyActual<Type>,
        IExpectationContext<Type>,
        IInstanceContinuation
    {
        public IExpectationContext<Type> TypedParent { get; set; }
        public override IExpectationContext Parent { get; }

        public InstanceContinuation(Func<Type> actualFetcher, IExpectationContext originalParent)
            : base(actualFetcher)
        {
            Parent = originalParent;
        }

        public override void Negate()
        {
            throw new InvalidOperationException("This context cannot be negated");
        }

        public override void ResetNegation()
        {
            throw new InvalidOperationException("This context cannot be negated");
        }

        public override void RunMatcher(Func<Type, IMatcherResult> matcher)
        {
            IMatcherResult result;
            try
            {
                result = matcher(Actual);
            }
            catch (Exception ex)
            {
                MatcherRunner.ProcessMatcherException(ex);
                return;
            }

            MatcherRunner.ProcessMatcherResult(Parent.IsNegated(), result);
        }
    }
}