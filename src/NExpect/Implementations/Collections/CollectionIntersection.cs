using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections
{
    internal class CollectionIntersection<T>
        : ExpectationContextWithLazyActual<IEnumerable<T>>,
          IHasActual<IEnumerable<T>>,
          ICollectionIntersection<T>
    {
        public ICollectionIntersectionEquivalent<T> Equivalent =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionIntersectionEquivalent<T>>(
                ActualFetcher, this
            );

        public ICollectionIntersectionEqual<T> Equal =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionIntersectionEqual<T>>(
                ActualFetcher, this
            );

        public CollectionIntersection(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
        {
        }
    }
}