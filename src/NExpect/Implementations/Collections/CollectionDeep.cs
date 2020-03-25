using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections
{
    internal class CollectionDeep<T> :
        ExpectationContextWithLazyActual<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionDeep<T>
    {
        public ICollectionDeepEqual<T> Equal =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionDeepEqual<T>>(ActualFetcher, this);

        public ICollectionDeepEquivalent<T> Equivalent =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionDeepEquivalent<T>>(ActualFetcher, this);

        public CollectionDeep(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
        {
        }
    }
}