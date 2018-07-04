using NExpect.Exceptions;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class DictionaryValueContinuation<TValue> :
        ExpectationContext<TValue>,
        IHasActual<TValue>,
        IDictionaryValueContinuation<TValue>
    {
        public TValue Actual { get; }

        public IDictionaryValueWith<TValue> With =>
            ContinuationFactory.Create<TValue, DictionaryValueWith<TValue>>(Actual, this);

        public DictionaryValueContinuation(TValue value)
        {
            Actual = value;
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