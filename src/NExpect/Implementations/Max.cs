using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class Max<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IMax<T>
{
    public Max(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}