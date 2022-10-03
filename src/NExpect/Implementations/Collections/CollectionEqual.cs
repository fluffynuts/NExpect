using System;
using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections;

internal class CollectionEqual<T> :
    ExpectationContextWithLazyActual<IEnumerable<T>>,
    IHasActual<IEnumerable<T>>,
    ICollectionEqual<T>
{
    public CollectionEqual(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}