using System;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class Require<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IRequire<T>
{
    public Require(Func<T> actualFetcher) : base(actualFetcher)
    {
    }

    public Require(Func<T> actualFetcher, bool negate) : base(actualFetcher, negate)
    {
    }
}