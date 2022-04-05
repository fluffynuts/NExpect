using System;
using Imported.PeanutButter.Utils;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries;

internal class DictionaryValueIntersection<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IDictionaryValueIntersection<T>
{
    public IDictionaryValueEqual<T> Equal
        => ContinuationFactory.Create<T, DictionaryValueEqual<T>>(
            ActualFetcher,
            this,
            SetIntersectionFlag
        );

    private void SetIntersectionFlag(DictionaryValueEqual<T> continuation)
    {
        continuation.SetMetadata(
            DictionaryValueEqual<T>.DICTIONARY_VALUE_DEEP_EQUALITY_TESTING,
            false
        );
    }

    public DictionaryValueIntersection(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}