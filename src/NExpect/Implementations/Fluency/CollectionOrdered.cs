using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency;

// ReSharper disable once UnusedType.Global
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