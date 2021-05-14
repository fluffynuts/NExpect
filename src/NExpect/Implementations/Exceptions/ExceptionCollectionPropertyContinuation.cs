using System;
using System.Collections.Generic;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Fluency;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Exceptions
{
    internal class ExceptionCollectionPropertyContinuation<T>
        : ExpectationContextWithLazyActual<IEnumerable<T>>,
          IHasActual<IEnumerable<T>>,
          IExceptionCollectionPropertyContinuation<T>
    {
        public ICollectionEquivalent<T> Equivalent => Next<CollectionEquivalent<T>>();
        public ICollectionEqual<T> Equal => Next<CollectionEqual<T>>();
        public ICollectionDeep<T> Deep => Next<CollectionDeep<T>>();
        public ICollectionIntersection<T> Intersection => Next<CollectionIntersection<T>>();
        public ICollectionAn<T> An => Next<CollectionAn<T>>();
        public ICollectionFor<T> For => Next<CollectionFor<T>>();
        public ICollectionOrdered<T> Ordered => Next<CollectionOrdered<T>>();
        public IContain<IEnumerable<T>> Containing => Next<CollectionTo<T>>().Contain;
        public ICollectionPropertyContinuationNot<T> Not => Next<CollectionPropertyContinuationNot<T>>();

        public ExceptionCollectionPropertyContinuation(Func<IEnumerable<T>> actualFetcher)
            : base(actualFetcher)
        {
        }
    }
}