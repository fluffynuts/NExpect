using NExpect.Implementations.Collections;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Exceptions
{
    internal class ExceptionPropertyContinuation<TValue> :
        Be<TValue>,
        IExceptionPropertyContinuation<TValue>
    {
        public new IPropertyNot<TValue> Not
            => ContinuationFactory.Create<TValue, PropertyNot<TValue>>(Actual, this);
        
        public ExceptionPropertyContinuation(TValue value): base(value)
        {
        }
    }
}