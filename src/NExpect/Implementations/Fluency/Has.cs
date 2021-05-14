using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Has<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IHas<T>
    {
        public IA<T> A => Next<A<T>>();
        public IAn<T> An => Next<An<T>>();
        public IMax<T> Max => Next<Max<T>>();

        public Has(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}