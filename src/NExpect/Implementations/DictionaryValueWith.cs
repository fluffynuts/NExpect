using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class DictionaryValueWith<TValue> : ExpectationContext<TValue>,
        IDictionaryValueWith<TValue>
    {
        public DictionaryValueWith(TValue value)
        {
            Actual = value;
        }

        public TValue Actual { get; }
    }
}