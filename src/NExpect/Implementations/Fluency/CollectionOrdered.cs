using System;
using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CollectionOrdered<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      IHasActual<IEnumerable<T>>,
      ICollectionOrdered<T>
{
    public CollectionOrdered(
        Func<IEnumerable<T>> actualFetcher
    ) : base(actualFetcher)
    {
    }
}