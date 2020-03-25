using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency
{
    internal class Intersection<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        IIntersection<T>
    {
        public IIntersectionEqual<T> Equal
            => ContinuationFactory.Create<T, IntersectionEqual<T>>(ActualFetcher, this);

        public Intersection(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}