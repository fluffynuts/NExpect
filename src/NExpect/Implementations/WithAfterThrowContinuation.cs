using System;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WithAfterThrowContinuation :
        ExpectationContext<Exception>, 
        IWithAfterThrowContinuation, 
        IHasActual<Exception>
    {
        public IExceptionMessageContinuation Message => 
            Factory.Create<string, StringValueContinuation<string>>(
                Actual.Message, 
                new WrappingContinuation<Exception,string>(this, c => c.Actual?.Message)
            );
        public Exception Actual { get; }


        public WithAfterThrowContinuation(Exception ex)
        {
            Actual = ex;
        }
    }
}