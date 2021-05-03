using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries
{
    internal class DictionaryValueMatched<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IDictionaryValueMatched<T>
    {
        public DictionaryValueMatched(
            Func<T> actualFetcher
        ) : base(actualFetcher)
        {
        }
    }
}