using System;
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
}