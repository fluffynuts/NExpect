using System;
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
}