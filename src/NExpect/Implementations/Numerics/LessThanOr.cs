using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class LessThanOr<T>
        : ExpectationContextWithLazyActual<T>,
          ILessThanOr<T>
    {
        public ILessThanOrEqual<T> Equal => Next<LessThanOrEqual<T>>();

        public LessThanOr(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}