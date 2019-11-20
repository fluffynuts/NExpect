using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable VirtualMemberCallInConstructor

namespace NExpect.Implementations.Collections
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class CollectionNotAfterTo<T>: 
        ExpectationContext<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionNotAfterTo<T>
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public IEnumerable<T> Actual { get; }

        public IContain<IEnumerable<T>> Contain =>
            ContinuationFactory.Create<IEnumerable<T>, Contain<IEnumerable<T>>>(Actual, this);

        public ICollectionBe<T> Be =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionBe<T>>(Actual, this);

        public ICollectionHave<T> Have =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionHave<T>>(Actual, this);

        public ICollectionDeep<T> Deep =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionDeep<T>>(Actual, this);

        public ICollectionIntersection<T> Intersection =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionIntersection<T>>(Actual, this);

        public CollectionNotAfterTo(IEnumerable<T> actual)
        {
            Actual = actual;
            Negate();
        }
    }
}