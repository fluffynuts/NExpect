using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries;

// ReSharper disable once ClassNeverInstantiated.Global
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