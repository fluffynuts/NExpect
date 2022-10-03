using System;
using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace NExpect.Implementations.Collections;

internal class CollectionDeepEqual<T> :
    ExpectationContextWithLazyActual<IEnumerable<T>>,
    IHasActual<IEnumerable<T>>,
    ICollectionDeepEqual<T>
{
    public CollectionDeepEqual(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}