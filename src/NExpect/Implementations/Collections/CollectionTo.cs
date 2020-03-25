using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections
{
    internal class CollectionTo<T> :
        ExpectationContextWithLazyActual<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionTo<T>
    {
        public IContain<IEnumerable<T>> Contain
            => ContinuationFactory.Create<IEnumerable<T>, Contain<IEnumerable<T>>>(ActualFetcher, this);

        public ICollectionNotAfterTo<T> Not =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionNotAfterTo<T>>(ActualFetcher, this);

        public ICollectionBe<T> Be =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionBe<T>>(ActualFetcher, this);

        public ICollectionHave<T> Have =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionHave<T>>(ActualFetcher, this);

        public ICollectionDeep<T> Deep =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionDeep<T>>(ActualFetcher, this);

        public ICollectionIntersection<T> Intersection =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionIntersection<T>>(ActualFetcher, this);

        public CollectionTo(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
        {
        }
    }
}