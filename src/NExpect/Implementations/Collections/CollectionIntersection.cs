using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections
{
    internal class CollectionIntersection<T> :
        ExpectationContext<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionIntersection<T>
    {
        public IEnumerable<T> Actual { get; }

        public ICollectionIntersectionEquivalent<T> Equivalent =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionIntersectionEquivalent<T>>(Actual, this);

        public ICollectionIntersectionEqual<T> Equal =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionIntersectionEqual<T>>(Actual, this);

        public CollectionIntersection(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}