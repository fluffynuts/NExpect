using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries;

internal class DictionaryValueEqual<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IDictionaryValueEqual<T>
{
    public const string DICTIONARY_VALUE_DEEP_EQUALITY_TESTING = "__dictionary_value_deep_equality_testing__";

    public DictionaryValueEqual(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}