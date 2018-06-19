using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class PropertyNot<TValue>
        : ExpectationContext<TValue>, IPropertyNot<TValue>
    {
        public TValue Actual { get; }

        public IToAfterNot<TValue> To
            => Factory.Create<TValue, ToAfterNot<TValue>>(Actual, this);

        public PropertyNot(TValue actual)
        {
            Actual = actual;
        }
    }
}