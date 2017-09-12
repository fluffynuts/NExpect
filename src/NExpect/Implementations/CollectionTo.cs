using System.Collections.Generic;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class CollectionTo<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionTo<T>
    {
        public IEnumerable<T> Actual { get; }

        public IContain<IEnumerable<T>> Contain
            => Factory.Create<IEnumerable<T>, Contain<IEnumerable<T>>>(Actual, this);

        public ICollectionNotAfterTo<T> Not =>
            Factory.Create<IEnumerable<T>, CollectionNotAfterTo<T>>(Actual, this);

        public ICollectionBe<T> Be =>
            Factory.Create<IEnumerable<T>, CollectionBe<T>>(Actual, this);

        public ICollectionHave<T> Have =>
            Factory.Create<IEnumerable<T>, CollectionHave<T>>(Actual, this);

        public ICollectionDeep<T> Deep =>
            Factory.Create<IEnumerable<T>, CollectionDeep<T>>(Actual, this);

        public ICollectionIntersection<T> Intersection =>
            Factory.Create<IEnumerable<T>, CollectionIntersection<T>>(Actual, this);

        public CollectionTo(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}