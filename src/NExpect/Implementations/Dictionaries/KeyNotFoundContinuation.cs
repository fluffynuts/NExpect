using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries;

internal class KeyNotFoundContinuation<TKey, TValue>
    : IDictionaryValueContinuation<TValue>
{
    private readonly TKey _key;

    public IDictionaryValueWith<TValue> With
    {
        get
        {
            Assertions.Throw($"Cannot expect against value for missing key '{_key}'.");
            return null;
        }
    }

    public KeyNotFoundContinuation(
        TKey key
    )
    {
        _key = key;
    }
}