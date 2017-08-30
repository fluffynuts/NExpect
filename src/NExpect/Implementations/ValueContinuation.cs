using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class ValueContinuation<TValue>
        : ExpectationContext<TValue>,
            IExceptionPropertyContinuation<TValue>
    {
        public ValueContinuation(TValue value)
        {
            Actual = value;
        }

        public TValue Actual { get; }

        public IEqualityContinuation<TValue> Equal =>
            Factory.Create<TValue, EqualityContinuation<TValue>>(Actual, this);

        public INot<TValue> Not =>
            Factory.Create<TValue, Not<TValue>>(Actual, this);
    }
}