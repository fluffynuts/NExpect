using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Dictionaries
{
    internal class DictionaryValueWith<TValue>
        : ExpectationContext<TValue>,
          IHasActual<TValue>,
          IDictionaryValueWith<TValue>
    {
        public TValue Actual { get; }

        public DictionaryValueWith(TValue value)
        {
            Actual = value;
        }

        public IDictionaryValue<TValue> Value =>
            ContinuationFactory.Create<TValue, DictionaryValue<TValue>>(Actual, this);
    }
}