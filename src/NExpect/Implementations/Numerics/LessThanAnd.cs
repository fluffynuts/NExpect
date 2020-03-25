using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class LessThanAnd<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        ILessThanAnd<T>
    {
        public IGreaterContinuation<T> Greater =>
            ContinuationFactory.Create<T, GreaterContinuation<T>>(ActualFetcher, this);

        public LessThanAnd(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}