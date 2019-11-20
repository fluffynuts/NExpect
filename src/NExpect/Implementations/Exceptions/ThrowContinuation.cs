using System;
using NExpect.Implementations.Collections;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations.Exceptions
{
    internal class ThrowContinuation<T> : 
        ExpectationContext<T>,
        IHasActual<T>,
        IThrowContinuation<T> where T : Exception
    {
        public T Actual => Exception;
        public T Exception { get; set; }

        public IWithAfterThrowContinuation<T> With => 
            ContinuationFactory.Create<T, WithAfterThrowContinuation<T>>(Exception, this);

        public override void RunMatcher(Func<T, IMatcherResult> matcher)
        {
            MatcherRunner.RunMatcher(Exception, this.IsNegated(), matcher);
        }
    }

    internal class ThrowAndContinuation<T>
        : ExpectationContext<T>,
          IHasActual<T>,
          IThrowAndContinuation<T> where T : Exception
    {
        public T Actual => Exception;
        public T Exception { get; set; }
        public IAndAfterWithAfterThrowContinuation<T> And =>
        ContinuationFactory.Create<T, AndAfterWithAfterThrowContinuation<T>>(Exception, this);
    }
}