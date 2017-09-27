using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class DictionaryValueWith<TValue> : 
        ExpectationContext<TValue>,
        IHasActual<TValue>,
        IDictionaryValueWith<TValue>
    {
        public TValue Actual { get; }

        public DictionaryValueWith(TValue value)
        {
            Actual = value;
        }
    }
}