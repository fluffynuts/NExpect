using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Required<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IRequired<T>
{
    public Required(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}