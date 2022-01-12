using System;
using NExpect.Implementations.Fluency;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class Of<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IOf<T>
    {
        public Of(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
    
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
}