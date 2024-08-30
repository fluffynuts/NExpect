using System;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class Then<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IThen<T>
{
    public Then(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}