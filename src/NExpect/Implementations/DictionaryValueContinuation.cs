using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class DictionaryValueContinuation<TValue> : ExpectationContext<TValue>,
        IDictionaryValueContinuation<TValue>
    {
        public DictionaryValueContinuation(TValue value)
        {
            Actual = value;
        }

        public TValue Actual { get; }

        public IDictionaryValueWith<TValue> With =>
            Factory.Create<TValue, DictionaryValueWith<TValue>>(Actual, this);
    }
}