using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency;

internal class NullOr<T> :
    ExpectationContextWithLazyActual<T>,
    IHasActual<T>,
    INullOr<T>
{
    public NullOr(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}