using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Intersection<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        IIntersection<T>
    {
        public IIntersectionEqual<T> Equal => Next<IntersectionEqual<T>>();

        public Intersection(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}