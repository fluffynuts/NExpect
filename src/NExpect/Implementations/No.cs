using System;
using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    // ReSharper disable once UnusedType.Global
    internal class No<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          INo<T>
    {
        public No(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}