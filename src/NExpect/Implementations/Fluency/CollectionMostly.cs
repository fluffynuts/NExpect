using System;
using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CollectionMostly<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      IHasActual<IEnumerable<T>>,
      ICollectionMostly<T>
{
    public CollectionMostly(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}