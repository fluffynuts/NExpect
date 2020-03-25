using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Fluency;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Numerics
{
    internal class GreaterThanAnd<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        IGreaterThanAnd<T>
    {
        public ILessContinuation<T> Less =>
            ContinuationFactory.Create<T, LessContinuation<T>>(ActualFetcher, this);

        public ITo<T> To =>
            ContinuationFactory.Create<T, To<T>>(ActualFetcher, this);

        public GreaterThanAnd(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}