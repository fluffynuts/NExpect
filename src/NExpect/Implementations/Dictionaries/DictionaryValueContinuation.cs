using System;
using NExpect.Exceptions;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Dictionaries
{
    internal class DictionaryValueContinuation<TValue> :
        ExpectationContextWithLazyActual<TValue>,
        IHasActual<TValue>,
        IDictionaryValueContinuation<TValue>
    {
        public IDictionaryValueWith<TValue> With =>
            ContinuationFactory.Create<TValue, DictionaryValueWith<TValue>>(ActualFetcher, this);

        public DictionaryValueContinuation(Func<TValue> actualFetcher) : base(actualFetcher)
        {
        }
    }

    internal class KeyNotFoundContinuation<TKey, TValue> : IDictionaryValueContinuation<TValue>
    {
        private readonly TKey _key;

        public IDictionaryValueWith<TValue> With =>
            throw new UnmetExpectationException($"Cannot expect against value for missing key '{_key}'.");

        public KeyNotFoundContinuation(
            TKey key)
        {
            _key = key;
        }
    }
}