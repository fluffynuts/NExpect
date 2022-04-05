using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics;

// ReSharper disable once ClassNeverInstantiated.Global
internal class LessThanAnd<T> :
    ExpectationContextWithLazyActual<T>,
    IHasActual<T>,
    ILessThanAnd<T>
{
    public IGreaterContinuation<T> Greater => Next<Greater<T>>();

    public LessThanAnd(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}