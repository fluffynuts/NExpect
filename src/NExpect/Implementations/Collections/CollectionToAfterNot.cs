using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections
{
    internal class CollectionToAfterNot<T>
        : ExpectationContextWithLazyActual<IEnumerable<T>>,
          IHasActual<IEnumerable<T>>,
          ICollectionToAfterNot<T>
    {
        public IContain<IEnumerable<T>> Contain => Next<Contain<IEnumerable<T>>>();
        public ICollectionBe<T> Be => Next<CollectionBe<T>>();
        public ICollectionHave<T> Have => Next<CollectionHave<T>>();
        public ICollectionDeep<T> Deep => Next<CollectionDeep<T>>();
        public ICollectionIntersection<T> Intersection => Next<CollectionIntersection<T>>();

        public CollectionToAfterNot(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
        {
        }
    }
}