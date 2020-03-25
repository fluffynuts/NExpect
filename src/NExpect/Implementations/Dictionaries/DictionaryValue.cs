using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class DictionaryValue<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IDictionaryValue<T>
    {
        public IDictionaryValueDeep<T> Deep
            => ContinuationFactory.Create<T, DictionaryValueDeep<T>>(ActualFetcher, this);

        public IDictionaryValueIntersection<T> Intersection
            => ContinuationFactory.Create<T, DictionaryValueIntersection<T>>(ActualFetcher, this);

        public DictionaryValue(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}