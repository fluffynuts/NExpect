using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Numerics
{
    internal class GreaterThanContinuation<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        IGreaterThanContinuation<T>
    {
        public IGreaterThanAnd<T> And =>
            ContinuationFactory.Create<T, GreaterThanAnd<T>>(ActualFetcher, this);

        public GreaterThanContinuation(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
    internal class LessThanContinuation<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        ILessThanContinuation<T>
    {
        public ILessThanAnd<T> And =>
            ContinuationFactory.Create<T, LessThanAnd<T>>(ActualFetcher, this);

        public LessThanContinuation(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}