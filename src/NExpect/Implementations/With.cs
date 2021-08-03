using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class With<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IWith<T>
    {
        public IRequired<T> Required 
            => ContinuationFactory.Create<T, Required<T>>(ActualFetcher, this);

        public With(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}