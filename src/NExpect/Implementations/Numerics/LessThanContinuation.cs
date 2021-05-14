using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class LessThanContinuation<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          ILessThanContinuation<T>
    {
        public ILessThanAnd<T> And => Next<LessThanAnd<T>>();

        public LessThanContinuation(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}