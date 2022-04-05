using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Null<T> :
    ExpectationContextWithLazyActual<T>,
    IHasActual<T>,
    INull<T>
{
    public INullOr<T> Or => Next<NullOr<T>>();

    public Null(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}