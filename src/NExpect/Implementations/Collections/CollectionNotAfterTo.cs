using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable VirtualMemberCallInConstructor

namespace NExpect.Implementations.Collections
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class CollectionNotAfterTo<T>: 
        ExpectationContextWithLazyActual<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionNotAfterTo<T>
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

        public CollectionNotAfterTo(Func<IEnumerable<T>> actualFetcher): base(actualFetcher)
        {
            Negate();
        }
    }
}