using System;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Having<T>
    : ExpectationContextWithLazyActual<T>,
      IHaving<T>
{
    public Having(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}

// ReSharper disable once ClassNeverInstantiated.Global