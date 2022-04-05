using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations;

internal class EqualityContinuation<T> :
    ExpectationContextWithLazyActual<T>,
    IHasActual<T>,
    IEqualityContinuation<T>
{
    public EqualityContinuation(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}