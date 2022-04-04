using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

namespace NExpect.Implementations;

// ReSharper disable once ClassNeverInstantiated.Global
internal class On<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IOn<T>
{
    public IAn<T> An => Next<An<T>>();
    public IA<T> A => Next<A<T>>();

    public On(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}