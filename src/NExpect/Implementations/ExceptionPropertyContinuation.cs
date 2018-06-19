using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class ExceptionPropertyContinuation<TValue> :
        Be<TValue>,
        IExceptionPropertyContinuation<TValue>
    {
        public new IPropertyNot<TValue> Not
            => Factory.Create<TValue, PropertyNot<TValue>>(Actual, this);
        
        public ExceptionPropertyContinuation(TValue value): base(value)
        {
        }
    }
}