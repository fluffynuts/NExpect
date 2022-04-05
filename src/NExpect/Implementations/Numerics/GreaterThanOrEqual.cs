using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics;

// ReSharper disable once ClassNeverInstantiated.Global
internal class GreaterThanOrEqual<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IGreaterThanOrEqual<T>
{
    public GreaterThanOrEqual(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}