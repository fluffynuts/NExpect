using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
namespace NExpect.Implementations.Exceptions;

internal class ExceptionPropertyContinuation<TValue> :
    Be<TValue>,
    IExceptionPropertyContinuation<TValue>
{
    public new IPropertyNot<TValue> Not
        => Next<PropertyNot<TValue>>();
        
    public ExceptionPropertyContinuation(Func<TValue> actualFetcher): base(actualFetcher)
    {
    }
}