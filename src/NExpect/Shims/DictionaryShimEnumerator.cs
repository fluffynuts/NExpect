using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NExpect.Shims;

internal class DictionaryShimEnumerator : IEnumerator<KeyValuePair<string, string>>
{
    public DictionaryShim Subject => _dictionaryShim;
    private readonly DictionaryShim _dictionaryShim;
    private int _currentOffset;

    public DictionaryShimEnumerator(DictionaryShim dictionaryShim)
    {
        _dictionaryShim = dictionaryShim;
        Reset();
    }

    public bool MoveNext()
    {
        return ++_currentOffset < _dictionaryShim.Count;
    }

    public void Reset()
    {
        _currentOffset = -1;
    }

    public KeyValuePair<string, string> Current
    {
        get {
            var key = _dictionaryShim.Keys.Skip(_currentOffset).First();
            var value = _dictionaryShim[key];
            return new KeyValuePair<string, string>(key, value);
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}