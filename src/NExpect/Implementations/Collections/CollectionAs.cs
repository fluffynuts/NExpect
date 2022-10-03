using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections;

internal class CollectionAs<T>:
    ExpectationContextWithLazyActual<IEnumerable<T>>,
    IHasActual<IEnumerable<T>>,
    ICollectionAs<T>
{
    public ICollectionExpectation<object> Objects =>
        new CollectionExpectation<object>(Actual.Select(o => o as object));

    public CollectionAs(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}