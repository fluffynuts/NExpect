using System;
using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations.Collections;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CollectionA<T> :
    ExpectationContextWithLazyActual<IEnumerable<T>>,
    IHasActual<IEnumerable<T>>,
    ICollectionA<T>
{
    public ISuperset<T> Superset => Next<Superset<T>>();
    public ISubset<T> Subset => Next<Subset<T>>();

    public CollectionA(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
    }
}