using System;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Matched<T> :
    ExpectationContextWithLazyActual<T>,
    IHasActual<T>,
    IMatched<T>
{
    public Matched(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}