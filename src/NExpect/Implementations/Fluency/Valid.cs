using System;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Valid<T>
    : ExpectationContextWithLazyActual<T>,
      IValid<T>,
      IHasActual<T>
{
    public Valid(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}