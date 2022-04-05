using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
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
        return MatcherRunner.RunMatcher(Exception, this.IsNegated(), matcher);
    }

    public ThrowContinuation() : base(() => null)
    {
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
        ContinuationFactory.Create<T, AndAfterWithAfterThrowContinuation<T>>(() => Exception, this);
}