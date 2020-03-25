using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Numerics
{
    internal class LessContinuation<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        ILessContinuation<T>
    {
        public ILessThan<T> Than =>
            ContinuationFactory.Create<T, LessThan<T>>(ActualFetcher, this);

        public LessContinuation(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }

    internal class LessThan<T>
    : ExpectationContextWithLazyActual<T>,
        ILessThan<T>
    {
        public ILessThanOr<T> Or 
            => ContinuationFactory.Create<T, LessThanOr<T>>(ActualFetcher, this);

        public LessThan(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }

    internal class LessThanOr<T>
    : ExpectationContextWithLazyActual<T>,
        ILessThanOr<T>
    {
        public ILessThanOrEqual<T> Equal 
            => ContinuationFactory.Create<T, LessThanOrEqual<T>>(ActualFetcher, this);

        public LessThanOr(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }

    internal class LessThanOrEqual<T>
    : ExpectationContextWithLazyActual<T>,
        ILessThanOrEqual<T>
    {
        public LessThanOrEqual(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}