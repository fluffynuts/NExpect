using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries
{
    internal class DictionaryValueDeep<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IDictionaryValueDeep<T>
    {
        public IDictionaryValueEqual<T> Equal
            => ContinuationFactory.Create<T, DictionaryValueEqual<T>>(ActualFetcher, this);

        public DictionaryValueDeep(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}