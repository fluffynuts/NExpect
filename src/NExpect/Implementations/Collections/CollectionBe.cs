using System;
using System.Collections.Generic;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections;

internal class CollectionBe<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      IHasActual<IEnumerable<T>>,
      ICollectionBe<T>
{
    public ICollectionEquivalent<T> Equivalent => Next<CollectionEquivalent<T>>();
    public ICollectionEqual<T> Equal => Next<CollectionEqual<T>>();
    public ICollectionDeep<T> Deep => Next<CollectionDeep<T>>();
    public ICollectionIntersection<T> Intersection => Next<CollectionIntersection<T>>();
    public ICollectionAn<T> An => Next<CollectionAn<T>>();
    public ICollectionA<T> A => Next<CollectionA<T>>();
    public ICollectionFor<T> For => Next<CollectionFor<T>>();
    public ICollectionOrdered<T> Ordered => Next<CollectionOrdered<T>>();
    public ICollectionMostly<T> Mostly => Next<CollectionMostly<T>>();

    public CollectionBe(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}