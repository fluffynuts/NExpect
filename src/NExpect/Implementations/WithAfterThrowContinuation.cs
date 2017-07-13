using System;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    public class WithAfterThrowContinuation :
        ExpectationContext<Exception>, IWithAfterThrowContinuation
    {
        public IExceptionMessageContinuation Message => 
            Factory.Create<string, StringValueContinuation<string>>(
                Actual.Message, 
                new WrappingContinuation<Exception,string>(this, c => c.Actual?.Message)
            );
        public Exception Actual { get; set; }


        public WithAfterThrowContinuation(Exception ex)
        {
            Actual = ex;
        }
    }
}