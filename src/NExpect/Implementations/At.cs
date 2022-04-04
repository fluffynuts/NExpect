using System;
using NExpect.Interfaces;

namespace NExpect.Implementations;

// ReSharper disable once ClassNeverInstantiated.Global
internal class At<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IAt<T>
{
    public At(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}