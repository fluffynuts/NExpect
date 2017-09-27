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
            Factory.Create<TValue, DictionaryValueWith<TValue>>(Actual, this);

        public DictionaryValueContinuation(TValue value)
        {
            Actual = value;
        }
    }
}