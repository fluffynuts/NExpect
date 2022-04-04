using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

namespace NExpect.Implementations;

// ReSharper disable once ClassNeverInstantiated.Global
internal class In<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IIn<T>
{
    public IAn<T> An => Next<An<T>>();
    public IA<T> A => Next<A<T>>();

    public In(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}