using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    internal class InstanceContinuation :
        IExpectationContext<Type>,
        IInstanceContinuation
    {
        public Type Actual { get; }
        public IExpectationContext<Type> TypedParent { get; set; }
        public IExpectationContext Parent { get; }

        public InstanceContinuation(Type actual, IExpectationContext originalParent)
        {
            Actual = actual;
            Parent = originalParent;
        }

        public void Negate()
        {
            throw new InvalidOperationException("This context cannot be negated");
        }

        public void ResetNegation()
        {
            throw new InvalidOperationException("This context cannot be negated");
        }

        public void RunMatcher(Func<Type, IMatcherResult> matcher)
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