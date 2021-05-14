using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Dictionaries
{
    internal class DictionaryValueWith<TValue>
        : ExpectationContextWithLazyActual<TValue>,
          IHasActual<TValue>,
          IDictionaryValueWith<TValue>
    {

        public IDictionaryValue<TValue> Value => Next<DictionaryValue<TValue>>();

        public DictionaryValueWith(Func<TValue> actualFetcher) : base(actualFetcher)
        {
        }
    }
}