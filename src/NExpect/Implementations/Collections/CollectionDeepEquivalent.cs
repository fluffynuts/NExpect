using System;
using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections;

internal class CollectionDeepEquivalent<T> :
    ExpectationContextWithLazyActual<IEnumerable<T>>,
    IHasActual<IEnumerable<T>>,
    ICollectionDeepEquivalent<T>
{
    public CollectionDeepEquivalent(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}