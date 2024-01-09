using System;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class EquivalenceContinuation<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IEquivalenceContinuation<T>
{
    public EquivalenceContinuation(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}