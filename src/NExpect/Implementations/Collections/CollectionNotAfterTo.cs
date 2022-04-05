using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable VirtualMemberCallInConstructor

namespace NExpect.Implementations.Collections;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CollectionNotAfterTo<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      IHasActual<IEnumerable<T>>,
      ICollectionNotAfterTo<T>
{
    public IContain<IEnumerable<T>> Contain => Next<Contain<IEnumerable<T>>>();
    public ICollectionBe<T> Be => Next<CollectionBe<T>>();
    public ICollectionHave<T> Have => Next<CollectionHave<T>>();
    public ICollectionDeep<T> Deep => Next<CollectionDeep<T>>();
    public ICollectionIntersection<T> Intersection => Next<CollectionIntersection<T>>();

    public CollectionNotAfterTo(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
        Negate();
    }
}