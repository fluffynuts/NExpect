using System.Collections.Generic;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class CollectionIntersection<T>: 
        ExpectationContext<IEnumerable<T>>,
        ICollectionIntersection<T>
    {
        public IEnumerable<T> Actual { get; }

        public ICollectionIntersectionEquivalent<T> Equivalent =>
            Factory.Create<IEnumerable<T>, CollectionIntersectionEquivalent<T>>(Actual, this);

        public ICollectionIntersectionEqual<T> Equal =>
            Factory.Create<IEnumerable<T>, CollectionIntersectionEqual<T>>(Actual, this);

        public CollectionIntersection(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}