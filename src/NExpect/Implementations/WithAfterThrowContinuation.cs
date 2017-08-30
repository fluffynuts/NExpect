using System;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class WithAfterThrowContinuation<T> :
        ExpectationContext<T>,
        IWithAfterThrowContinuation<T>,
        IHasActual<T>
        where T : Exception
    {
        public IExceptionPropertyContinuation<string> Message =>
            Factory.Create<string, ValueContinuation<string>>(
                Actual.Message,
                new WrappingContinuation<Exception, string>(
                    this, c => c.Actual?.Message
                )
            );
        public IExceptionPropertyContinuation<TValue> Property<TValue>(Func<T, TValue> propertyValueFetcher)
        {
            var exceptionPropertyValue = propertyValueFetcher(Actual);

            return Factory.Create<TValue, ValueContinuation<TValue>>(
                exceptionPropertyValue,
                new WrappingContinuation<Exception, TValue>(
                    this, c => exceptionPropertyValue
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