using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
namespace NExpect.Implementations.Dictionaries;

internal class DictionaryValueContinuation<TValue>
    : ExpectationContextWithLazyActual<TValue>,
      IHasActual<TValue>,
      IDictionaryValueContinuation<TValue>
{
    public IDictionaryValueWith<TValue> With =>
        Next<DictionaryValueWith<TValue>>();

    public DictionaryValueContinuation(
        Func<TValue> actualFetcher
    ) : base(actualFetcher)
    {
    }
}