using System;
using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CollectionHaving<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      ICollectionHaving<T>
{
    public CollectionHaving(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}