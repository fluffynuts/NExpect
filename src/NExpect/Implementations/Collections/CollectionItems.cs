using System;
using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections;

internal class CollectionItems<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      IHasActual<IEnumerable<T>>,
      ICollectionItems<T>
{
    public ICollectionItemsTo<T> To
        => Next<CollectionItemsTo<T>>();

    public ICollectionItemsNot<T> Not
        => NextNegated<CollectionItemsNot<T>>();

    public CollectionItems(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }

    public CollectionItems(Func<IEnumerable<T>> actualFetcher, bool negate) : base(actualFetcher, negate)
    {
    }
}