using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
namespace NExpect.Implementations.Fluency;

internal class Have<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IHave<T>
{
    public IA<T> A => Next<A<T>>();
    public IAn<T> An => Next<An<T>>();
    public IBeen<T> Been => Next<Been<T>>();
    public IMax<T> Max => Next<Max<T>>();
    public IDefault<T> Default => Next<Default<T>>();
    public IValid<T> Valid => Next<Valid<T>>();

    public Have(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}