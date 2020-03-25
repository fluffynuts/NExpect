using System;
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
            => ContinuationFactory.Create<TValue, PropertyNot<TValue>>(ActualFetcher, this);
        
        public ExceptionPropertyContinuation(Func<TValue> actualFetcher): base(actualFetcher)
        {
        }
    }
}