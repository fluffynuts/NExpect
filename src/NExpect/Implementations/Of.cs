using System;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    // ReSharper disable once ClassNeverInstantiated.Global
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