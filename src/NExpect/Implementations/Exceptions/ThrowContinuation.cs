using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations.Exceptions;

internal class ThrowContinuation<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IThrowContinuation<T> where T : Exception
{
    public T Exception
    {
        get => _exception;
        set 
        { 
            Actual = value;
            _exception = value;
        }
    }

    private T _exception;

    public IWithAfterThrowContinuation<T> With =>
        ContinuationFactory.Create<T, WithAfterThrowContinuation<T>>(() => Exception, this);

    public override IMatcherResult RunMatcher(Func<T, IMatcherResult> matcher)
    {
        return MatcherRunner.RunMatcher(this, Exception, this.IsNegated(), matcher);
    }

    public ThrowContinuation() : base(() => null)
    {
        Assertions.Forget(this);
    }
}