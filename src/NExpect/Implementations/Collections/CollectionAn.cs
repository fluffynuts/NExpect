using System;
using System.Collections.Generic;
using NExpect.Implementations.Fluency;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections;

internal class CollectionAn<T> :
    ExpectationContextWithLazyActual<IEnumerable<T>>,
    IHasActual<IEnumerable<T>>,
    ICollectionAn<T>
{
    public IInstanceContinuation Instance => 
        new InstanceContinuation(() => ActualFetcher()?.GetType(), this);

    public CollectionAn(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}