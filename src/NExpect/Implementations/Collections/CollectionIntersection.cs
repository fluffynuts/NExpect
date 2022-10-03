using System;
using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections;

internal class CollectionIntersection<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      IHasActual<IEnumerable<T>>,
      ICollectionIntersection<T>
{
    public ICollectionIntersectionEquivalent<T> Equivalent =>
        Next<CollectionIntersectionEquivalent<T>>();

    public ICollectionIntersectionEqual<T> Equal =>
        Next<CollectionIntersectionEqual<T>>();

    public CollectionIntersection(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}