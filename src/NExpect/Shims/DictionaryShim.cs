using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace NExpect.Shims;

/// <summary>
/// Provides a shimming dictionary around a NameValueCollection
/// - used for Expectations around NameValueCollections
/// </summary>
public class DictionaryShim : IDictionary<string, string>
{
    private readonly NameValueCollection _actual;

    /// <inheritdoc />
    public DictionaryShim(
        NameValueCollection actual
    )
    {
        _actual = actual;
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return new DictionaryShimEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public void Add(KeyValuePair<string, string> item)
    {
        Add(item.Key, item.Value);
    }

    /// <inheritdoc />
    public void Clear()
    {
        _actual.Clear();
    }

    /// <inheritdoc />
    public bool Contains(KeyValuePair<string, string> item)
    {
        if (!ContainsKey(item.Key))
            return false;
        var existing = _actual[item.Key];
        return existing == item.Value;
    }

    /// <inheritdoc />
    public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
    {
        foreach (var kvp in this)
        {
            array[arrayIndex++] = kvp;
        }
    }

    /// <inheritdoc />
    public bool Remove(KeyValuePair<string, string> item)
    {
        if (!Contains(item))
            return false;
        _actual.Remove(item.Key);
        return true;
    }

    /// <inheritdoc />
    public int Count => _actual.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public void Add(string key, string value)
    {
        _actual.Add(key, value);
    }

    /// <inheritdoc />
    public bool ContainsKey(string key)
    {
        return _actual.AllKeys.Contains(key);
    }

    /// <inheritdoc />
    public bool Remove(string key)
    {
        if (!ContainsKey(key))
            return false;
        _actual.Remove(key);
        return true;
    }

    /// <inheritdoc />
    public bool TryGetValue(string key, out string value)
    {
        return ContainsKey(key)
            ? SetOutValue(key, out value)
            : MissOutValue(out value);
    }

    private bool SetOutValue(string key, out string value)
    {
        value = this[key];
        return true;
    }

    private bool MissOutValue(out string value)
    {
        value = null;
        return false;
    }

    /// <inheritdoc />
    public string this[string key]
    {
        get => _actual[key];
        set => _actual[key] = value;
    }

    /// <inheritdoc />
    public ICollection<string> Keys => _actual.AllKeys;

    /// <inheritdoc />
    public ICollection<string> Values => GetValues();

    private ICollection<string> GetValues()
    {
        return Keys.Select(k => this[k]).ToArray();
    }
}