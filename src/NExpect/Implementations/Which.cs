using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class Which<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IWhich<T>
    {
        public Which(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}