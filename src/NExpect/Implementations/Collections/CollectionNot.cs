using System;
using System.Collections;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections
{
    internal class CollectionNot<T> :
        ExpectationContextWithLazyActual<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionNot<T>
    {
        public CollectionNot(Func<IEnumerable<T>> actualFetcher): base(actualFetcher)
        {
            Negate();
        }

        public ICollectionToAfterNot<T> To =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionToAfterNot<T>>(ActualFetcher, this);

    }
    
    internal class CollectionPropertyContinuationNot<T> :
        ExpectationContextWithLazyActual<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionPropertyContinuationNot<T>
    {
        public IContain<IEnumerable<T>> Containing =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionTo<T>>(ActualFetcher, this).Contain;

        public CollectionPropertyContinuationNot(Func<IEnumerable<T>> actualFetcher): base(actualFetcher)
        {
            Negate();
        }

        public ICollectionToAfterNot<T> To =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionToAfterNot<T>>(ActualFetcher, this);
    }
}