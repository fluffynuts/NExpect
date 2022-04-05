using System;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Default<T>
    : ExpectationContextWithLazyActual<T>,
      IDefault<T>,
      IHasActual<T>
{
    public Default(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}