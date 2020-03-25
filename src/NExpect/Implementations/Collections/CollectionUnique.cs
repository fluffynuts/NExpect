using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections
{
    internal class CollectionUnique<T> :
        ExpectationContextWithLazyActual<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionUnique<T>
    {
        public CollectionUnique(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
        {
        }
    }
}