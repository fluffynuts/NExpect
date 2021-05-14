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
        public IDictionaryValueDeep<T> Deep => Next<DictionaryValueDeep<T>>();
        public IDictionaryValueIntersection<T> Intersection => Next<DictionaryValueIntersection<T>>();
        public IDictionaryValueMatched<T> Matched => Next<DictionaryValueMatched<T>>();

        public DictionaryValue(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}