using System;
using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections;

internal class CollectionItemsNot<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      IHasActual<IEnumerable<T>>,
      ICollectionItemsNot<T>
{
    public ICollectionItemsNotTo<T> To =>
        Next<CollectionItemsNotTo<T>>();

    public CollectionItemsNot(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }

    public CollectionItemsNot(Func<IEnumerable<T>> actualFetcher, bool negate) : base(actualFetcher, negate)
    {
    }
}