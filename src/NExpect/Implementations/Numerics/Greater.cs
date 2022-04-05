using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Greater<T>
    : ExpectationContextWithLazyActual<T>,
      IGreaterContinuation<T>
{
    public IGreaterThan<T> Than => Next<GreaterThan<T>>();

    public Greater(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}