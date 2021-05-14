using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Collections
{
    internal class CollectionPropertyContinuationNot<T>
        : ExpectationContextWithLazyActual<IEnumerable<T>>,
          IHasActual<IEnumerable<T>>,
          ICollectionPropertyContinuationNot<T>
    {
        public IContain<IEnumerable<T>> Containing => Next<CollectionTo<T>>().Contain;
        public ICollectionToAfterNot<T> To => Next<CollectionToAfterNot<T>>();

        public CollectionPropertyContinuationNot(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
        {
            Negate();
        }
    }
}