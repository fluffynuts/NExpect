using System.Collections.Generic;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class CollectionBe<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionBe<T>
    {
        public ICollectionEquivalent<T> Equivalent =>
            Factory.Create<IEnumerable<T>, CollectionEquivalent<T>>(Actual, this);

        public ICollectionEqual<T> Equal =>
            Factory.Create<IEnumerable<T>, CollectionEqual<T>>(Actual, this);

        public ICollectionDeep<T> Deep =>
            Factory.Create<IEnumerable<T>, CollectionDeep<T>>(Actual, this);

        public ICollectionIntersection<T> Intersection =>
            Factory.Create<IEnumerable<T>, CollectionIntersection<T>>(Actual, this);

        public ICollectionAn<T> An =>
            Factory.Create<IEnumerable<T>, CollectionAn<T>>(Actual, this);

        public ICollectionFor<T> For =>
            Factory.Create<IEnumerable<T>, CollectionFor<T>>(Actual, this);

        public IEnumerable<T> Actual { get; }

        public CollectionBe(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}