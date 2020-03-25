using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections
{
    internal class CollectionToAfterNot<T> :
        ExpectationContextWithLazyActual<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionToAfterNot<T>
    {
        public IContain<IEnumerable<T>> Contain =>
            ContinuationFactory.Create<IEnumerable<T>, Contain<IEnumerable<T>>>(ActualFetcher, this);

        public ICollectionBe<T> Be =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionBe<T>>(ActualFetcher, this);

        public ICollectionHave<T> Have =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionHave<T>>(ActualFetcher, this);

        public ICollectionDeep<T> Deep =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionDeep<T>>(ActualFetcher, this);

        public ICollectionIntersection<T> Intersection =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionIntersection<T>>(ActualFetcher, this);

        public CollectionToAfterNot(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
        {
        }
    }
}