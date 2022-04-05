using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics;

// ReSharper disable once ClassNeverInstantiated.Global
internal class GreaterThanContinuation<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IGreaterThanContinuation<T>
{
    public IGreaterThanAnd<T> And => Next<GreaterThanAnd<T>>();

    public GreaterThanContinuation(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}