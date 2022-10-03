using System;
using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections;

internal class CollectionItemsToNot<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      IHasActual<IEnumerable<T>>,
      ICollectionItemsToNot<T>
{
    public CollectionItemsToNot(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }

    public CollectionItemsToNot(Func<IEnumerable<T>> actualFetcher, bool negate) : base(actualFetcher, negate)
    {
    }
}