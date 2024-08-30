using System;
using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations.Collections;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Subset<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>, ISubset<T>
{
    public Subset(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}