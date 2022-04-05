using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics;

// ReSharper disable once ClassNeverInstantiated.Global
internal class LessThan<T>
    : ExpectationContextWithLazyActual<T>,
      ILessThan<T>
{
    public ILessThanOr<T> Or => Next<LessThanOr<T>>();

    public LessThan(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}