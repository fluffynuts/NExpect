using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency
{
    internal class Approximately<T>
        : ExpectationContextWithLazyActual<T>,
          IApproximately<T>
    {
        public Approximately(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}