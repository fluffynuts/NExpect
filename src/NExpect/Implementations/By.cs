using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class By<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IBy<T>
{
    public By(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}