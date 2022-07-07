using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace NExpect.Matchers.AspNet.Tests.Implementations
{
    internal class FormCollection : IFormCollection
    {
        private FileCollection _files = new();

        public void AddFile(IFormFile file)
        {
            _files.Add(file);
        }

        private Dictionary<string, StringValues> _store = new();

        public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator()
        {
            return _store.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool ContainsKey(string key)
        {
            return _store.ContainsKey(key);
        }

        public bool TryGetValue(string key, out StringValues value)
        {
            return _store.TryGetValue(key, out value);
        }

        public int Count => _store.Count;
        public ICollection<string> Keys => _store.Keys;

        public StringValues this[string key]
        {
            get => _store[key];
            set => _store[key] = value;
        }

        public IFormFileCollection Files => _files;
    }
}