using System;
using NExpect.Implementations.Fluency;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Numerics
{
    internal class GreaterThanAnd<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IGreaterThanAnd<T>
    {
        public ILessContinuation<T> Less => Next<LessContinuation<T>>();
        public ITo<T> To => Next<To<T>>();

        public GreaterThanAnd(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}