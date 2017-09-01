using System.Collections.Generic;
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

    internal class ExceptionCollectionPropertyContinuation<T>
        : ExpectationContext<IEnumerable<T>>,
            IExceptionCollectionPropertyContinuation<T>
    {
        public ExceptionCollectionPropertyContinuation(IEnumerable<T> value)
        {
            Actual = value;
        }

        public IEnumerable<T> Actual { get; }

        public ICollectionEquivalent<T> Equivalent =>
            Factory.Create<IEnumerable<T>, CollectionEquivalent<T>>(Actual, this);

        public ICollectionEqual<T> Equal =>
            Factory.Create<IEnumerable<T>, CollectionEqual<T>>(Actual, this);
    }
}