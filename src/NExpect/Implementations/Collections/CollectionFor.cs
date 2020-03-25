using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections
{
    internal class CollectionFor<T> :
        ExpectationContextWithLazyActual<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionFor<T>
    {
        public CollectionFor(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
        {
        }
    }
}