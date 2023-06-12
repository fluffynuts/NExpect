using System;
using System.Collections.Generic;
using NExpect.Implementations.Collections;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class CollectionAnd<T>
        : ExpectationContextWithLazyActual<IEnumerable<T>>,
          IHasActual<IEnumerable<T>>,
          ICollectionAnd<T>
    {
        public CollectionAnd(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
        {
        }

        public CollectionAnd(Func<IEnumerable<T>> actualFetcher, bool negate) : base(actualFetcher, negate)
        {
        }

        public ICollectionTo<T> To => Next<CollectionTo<T>>();
        public ICollectionNot<T> Not => Next<CollectionNot<T>>();
    }
}