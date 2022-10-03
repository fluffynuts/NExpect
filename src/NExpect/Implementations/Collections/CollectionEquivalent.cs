using System;
using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections;

internal class CollectionEquivalent<T> :
    ExpectationContextWithLazyActual<IEnumerable<T>>,
    IHasActual<IEnumerable<T>>,
    ICollectionEquivalent<T>
{
    public CollectionEquivalent(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}