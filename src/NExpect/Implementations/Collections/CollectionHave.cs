using System;
using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections;

internal class CollectionHave<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      IHasActual<IEnumerable<T>>,
      ICollectionHave<T>
{
    public ICollectionUnique<T> Unique => Next<CollectionUnique<T>>();

    public CollectionHave(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}