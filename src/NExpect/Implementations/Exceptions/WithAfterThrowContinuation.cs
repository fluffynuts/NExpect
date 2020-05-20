using System;
using System.Collections.Generic;
using NExpect.Helpers;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable PossibleMultipleEnumeration

namespace NExpect.Implementations.Exceptions
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class AndAfterWithAfterThrowContinuation<T>
        : WithAfterThrowContinuation<T>,
          IAndAfterWithAfterThrowContinuation<T> where T : Exception
    {
        public AndAfterWithAfterThrowContinuation(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class WithAfterThrowContinuation<T>
        : ExpectationContextWithLazyActual<T>,
          IWithAfterThrowContinuation<T>,
          IHasActual<T>
        where T : Exception
    {
        public IStringPropertyContinuation Message =>
            ContinuationFactory.Create<string, ExceptionStringPropertyContinuation>(
                () => Actual.Message,
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

        public IExceptionCollectionPropertyContinuation<TItem> CollectionProperty<TItem>(
            Func<T, IEnumerable<TItem>> propertyValueFetcher
        )
        {
            var fetcher = FuncFactory.Memoize(
                () => propertyValueFetcher(Actual)
            );
            return ContinuationFactory.Create<IEnumerable<TItem>,
                ExceptionCollectionPropertyContinuation<TItem>>(
                fetcher,
                new WrappingContinuation<Exception, IEnumerable<TItem>>(
                    this, c => fetcher()
                )
            );
        }

        private ExceptionPropertyContinuation<TContinuationValue> CreateFor<TContinuationValue>(
            Func<T, TContinuationValue> propertyValueFetcher
        )
        {
            var fetcher = FuncFactory.Memoize(
                () => propertyValueFetcher(Actual)
            );
            return ContinuationFactory.Create<TContinuationValue, ExceptionPropertyContinuation<TContinuationValue>>(
                fetcher,
                new WrappingContinuation<Exception, TContinuationValue>(
                    this, c => fetcher()
                )
            );
        }

        public WithAfterThrowContinuation(
            Func<T> actualFetcher
        ) : base(actualFetcher)
        {
        }
    }
}