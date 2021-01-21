using System;
using NExpect.Implementations.Fluency;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class More<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IMore<T>
    {
        public IAnd<T> And =>
            ContinuationFactory.Create<T, And<T>>(ActualFetcher, this);

        public IWith<T> With =>
            ContinuationFactory.Create<T, With<T>>(ActualFetcher, this);

        public IOf<T> Of =>
            ContinuationFactory.Create<T, Of<T>>(ActualFetcher, this);

        public More(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}