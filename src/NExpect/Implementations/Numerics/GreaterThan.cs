using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics;

// ReSharper disable once ClassNeverInstantiated.Global
internal class GreaterThan<T>
    : ExpectationContextWithLazyActual<T>,
      IGreaterThan<T>
{
    public IGreaterThanOr<T> Or => Next<GreaterThanOr<T>>();

    public GreaterThan(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}