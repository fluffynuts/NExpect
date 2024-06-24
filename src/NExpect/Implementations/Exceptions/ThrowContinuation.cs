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
        get
        {
            if (_exceptionFetcher is not null)
            {
                return _exception ??= _exceptionFetcher();
            }

            return _exception;
        }

        set
        {
            Actual = value;
            _exception = value;
            _exceptionFetcher = null;
        }
    }

    private T _exception;
    private Func<T> _exceptionFetcher;

    public IWithAfterThrowContinuation<T> With =>
        ContinuationFactory.Create<T, WithAfterThrowContinuation<T>>(() => Exception, this);

    public IInnerExceptionAfterThrowContinuation<T> Inner =>
        ContinuationFactory.Create<T, InnerExceptionAfterThrowContinuation<T>>(
            () => Exception,
            this
        );

    public override IMatcherResult RunMatcher(Func<T, IMatcherResult> matcher)
    {
        return MatcherRunner.RunMatcher(this, Exception, this.IsNegated(), matcher);
    }

    public ThrowContinuation(Func<T> actualFetcher) : base(actualFetcher)
    {
        _exceptionFetcher = actualFetcher;
    }

    public ThrowContinuation() : base(() => null)
    {
        Assertions.Forget(this);
    }
}

internal class InnerExceptionAfterThrowContinuation<T>
    : ThrowContinuation<T>,
      IHasActual<T>,
      IExpectationContext<T>,
      IInnerExceptionAfterThrowContinuation<T> where T : Exception
{
    public InnerExceptionAfterThrowContinuation(
        Func<T> actualFetcher
    ) : base(actualFetcher)
    {
    }

    public InnerExceptionAfterThrowContinuation(
        Func<T> actualFetcher,
        bool negate
    ) : base(actualFetcher)
    {
    }
}