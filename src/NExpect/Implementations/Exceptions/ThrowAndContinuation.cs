using System;
using NExpect.Interfaces;

namespace NExpect.Implementations.Exceptions;

internal class ThrowAndContinuation<T>
    : ExpectationContext<T>,
      IHasActual<T>,
      IThrowAndContinuation<T> where T : Exception
{
    public T Actual => Exception;
    public T Exception { get; set; }

    public IAndAfterWithAfterThrowContinuation<T> And =>
        ContinuationFactory.Create<T, AndAfterWithAfterThrowContinuation<T>>(() => Exception, this);

    public ThrowAndContinuation()
    {
        Assertions.Forget(this);
    }
}