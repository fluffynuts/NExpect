using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Numerics
{
    internal class GreaterContinuation<T>
        : ExpectationContextWithLazyActual<T>,
            IGreaterContinuation<T>
    {
        public IGreaterThan<T> Than
            => ContinuationFactory.Create<T, GreaterThan<T>>(ActualFetcher, this);

        public GreaterContinuation(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }

    internal class GreaterThan<T>
        : ExpectationContextWithLazyActual<T>,
            IGreaterThan<T>
    {
        public IGreaterThanOr<T> Or 
        => ContinuationFactory.Create<T, GreaterThanOr<T>>(ActualFetcher, this);

        public GreaterThan(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
    
    internal class GreaterThanOr<T>
        : ExpectationContextWithLazyActual<T>,
            IGreaterThanOr<T>
    {
        public IGreaterThanOrEqual<T> Equal 
            => ContinuationFactory.Create<T, GreaterThanOrEqual<T>>(ActualFetcher, this);

        public GreaterThanOr(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }

    internal class GreaterThanOrEqual<T> : 
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        IGreaterThanOrEqual<T>
    {
        public GreaterThanOrEqual(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}