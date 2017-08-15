using System;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class ThrowContinuation<T> : 
        ExpectationContext<T>,
        IHasActual<T>,
        IThrowContinuation<T> where T : Exception
    {
        public T Actual => Exception;
        public T Exception { get; set; }

        public IWithAfterThrowContinuation<T> With => 
            Factory.Create<T, WithAfterThrowContinuation<T>>(Exception, this);

    }
}