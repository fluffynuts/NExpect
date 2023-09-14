using System;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class With<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IWith<T>
{
    public IRequired<T> Required 
        => ContinuationFactory.Create<T, Required<T>>(ActualFetcher, this);
    
    public INo<T> No
        => ContinuationFactory.Create<T, No<T>>(ActualFetcher, this);
    
    public IOptional<T> Optional
        => ContinuationFactory.Create<T, Optional<T>>(ActualFetcher, this);

    public With(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}