using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics;

// ReSharper disable once ClassNeverInstantiated.Global
internal class LessThanOrEqual<T>
    : ExpectationContextWithLazyActual<T>,
      ILessThanOrEqual<T>
{
    public LessThanOrEqual(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}