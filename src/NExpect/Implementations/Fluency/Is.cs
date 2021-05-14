using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency
{
    internal class Is<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IIs<T>
    {
        public IA<T> A => Next<A<T>>();
        public IAn<T> An => Next<An<T>>();

        public Is(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}