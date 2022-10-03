using System;
using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections;

internal class CollectionItemsTo<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      IHasActual<IEnumerable<T>>,
      ICollectionItemsTo<T>
{
    public ICollectionItemsToNot<T> Not
        => NextNegated<CollectionItemsToNot<T>>();

    public CollectionItemsTo(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }

    public CollectionItemsTo(Func<IEnumerable<T>> actualFetcher, bool negate) : base(actualFetcher, negate)
    {
    }
}