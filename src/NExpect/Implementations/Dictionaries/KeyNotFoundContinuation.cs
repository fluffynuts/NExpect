using NExpect.Exceptions;
using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries;

internal class KeyNotFoundContinuation<TKey, TValue> 
    : IDictionaryValueContinuation<TValue>
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