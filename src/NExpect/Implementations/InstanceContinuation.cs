using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    internal class InstanceContinuation<T> :
        IExpectationContext<Type>,
        IInstanceContinuation
    {
        public Type Actual { get; }

        public InstanceContinuation(Type actual, IExpectationContext<T> originalParent)
        {
            Actual = actual;
            _originalParent = originalParent;
        }

        public void Negate()
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
            
            MatcherRunner.ProcessMatcherResult(IsOriginalContextNegated(), result);
        }

        private bool IsOriginalContextNegated()
        {
            var originalContextIsNegated = false;
            try
            {
                _originalParent.RunMatcher(t => new MatcherResult(true));
            }
            catch
            {
                originalContextIsNegated = true;
            }
            return originalContextIsNegated;
        }

        private readonly IExpectationContext<T> _originalParent;
        public IExpectationContext<Type> Parent { get; set; }
    }
}