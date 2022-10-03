using System;
using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections;

internal class CollectionItemsNotTo<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      IHasActual<IEnumerable<T>>,
      ICollectionItemsNotTo<T>
{
    public CollectionItemsNotTo(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }

    public CollectionItemsNotTo(Func<IEnumerable<T>> actualFetcher, bool negate) : base(actualFetcher, negate)
    {
    }
}