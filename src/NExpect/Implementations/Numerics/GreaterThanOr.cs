using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class GreaterThanOr<T>
        : ExpectationContextWithLazyActual<T>,
          IGreaterThanOr<T>
    {
        public IGreaterThanOrEqual<T> Equal => Next<GreaterThanOrEqual<T>>();

        public GreaterThanOr(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}