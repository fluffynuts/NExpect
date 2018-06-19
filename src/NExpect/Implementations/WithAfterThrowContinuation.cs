using System;
using System.Collections.Generic;
using NExpect.Interfaces;
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect.Implementations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class WithAfterThrowContinuation<T> :
        ExpectationContext<T>,
        IWithAfterThrowContinuation<T>,
        IHasActual<T>
        where T : Exception
    {
        public IStringPropertyContinuation Message =>
            Factory.Create<string, ExceptionStringPropertyContinuation>(
                Actual.Message,
                new WrappingContinuation<Exception, string>(
                    this, c => c.Actual?.Message
                )
            );

        public IBe<TValue> Property<TValue>(
            Func<T, TValue> propertyValueFetcher
        )
        {
            return CreateFor(propertyValueFetcher);
        }

        public ICollectionBe<TItem> CollectionProperty<TItem>(
            Func<T, IEnumerable<TItem>> propertyValueFetcher
        )
        {
            var continuationValue = propertyValueFetcher(Actual);
            return Factory.Create<IEnumerable<TItem>,
                ExceptionCollectionPropertyContinuation<TItem>>(
                continuationValue,
                new WrappingContinuation<Exception, IEnumerable<TItem>>(
                    this, c => continuationValue
                )
            );
        }

        private ExceptionPropertyContinuation<TContinuationValue> CreateFor<TContinuationValue>(
            Func<T, TContinuationValue> propertyValueFetcher
        )
        {
            var continuationValue = propertyValueFetcher(Actual);
            return Factory.Create<TContinuationValue, ExceptionPropertyContinuation<TContinuationValue>>(
                continuationValue,
                new WrappingContinuation<Exception, TContinuationValue>(
                    this, c => continuationValue
                )
            );
        }

        public T Actual { get; }


        public WithAfterThrowContinuation(T ex)
        {
            Actual = ex;
        }
    }
}