using System;
using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations.Collections;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Superset<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>, ISuperset<T>
{
    public Superset(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}