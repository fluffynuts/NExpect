using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class ExceptionPropertyContinuation<TValue>
        : ExpectationContext<TValue>,
            IExceptionPropertyContinuation<TValue>
    {
        public ExceptionPropertyContinuation(TValue value)
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