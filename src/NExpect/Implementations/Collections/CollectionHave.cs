using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections
{
    internal class CollectionHave<T> :
        ExpectationContextWithLazyActual<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionHave<T>
    {
        public ICollectionUnique<T> Unique =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionUnique<T>>(ActualFetcher, this);

        public CollectionHave(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
        {
        }
    }
}