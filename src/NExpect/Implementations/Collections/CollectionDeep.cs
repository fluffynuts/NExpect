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
            Next<CollectionDeepEqual<T>>();

        public ICollectionDeepEquivalent<T> Equivalent =>
            Next<CollectionDeepEquivalent<T>>();

        public CollectionDeep(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
        {
        }
    }
}