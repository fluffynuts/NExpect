using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections;

internal class CollectionTo<T> :
    ExpectationContextWithLazyActual<IEnumerable<T>>,
    IHasActual<IEnumerable<T>>,
    ICollectionTo<T>
{
    public IContain<IEnumerable<T>> Contain => Next<Contain<IEnumerable<T>>>();
    public ICollectionNotAfterTo<T> Not => Next<CollectionNotAfterTo<T>>();
    public ICollectionBe<T> Be => Next<CollectionBe<T>>();
    public ICollectionHave<T> Have => Next<CollectionHave<T>>();
    public ICollectionDeep<T> Deep => Next<CollectionDeep<T>>();
    public ICollectionIntersection<T> Intersection => Next<CollectionIntersection<T>>();

    public CollectionTo(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}