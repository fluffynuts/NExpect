using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class DictionaryValueDeep<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IDictionaryValueDeep<T>
    {
        public IDictionaryValueEqual<T> Equal
            => Next<DictionaryValueEqual<T>>();

        public DictionaryValueDeep(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}