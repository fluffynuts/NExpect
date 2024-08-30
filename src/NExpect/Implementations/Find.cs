using System;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class Find<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IFind<T>
{
    public Find(Func<T> actualFetcher) : base(actualFetcher)
    {
    }

    public Find(Func<T> actualFetcher, bool negate) : base(actualFetcher, negate)
    {
    }
}