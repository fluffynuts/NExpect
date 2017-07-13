using System;

namespace NExpect.Interfaces
{
    public interface IWithAfterThrowContinuation : IContinuation<Exception>
    {
        IExceptionMessageContinuation Message { get; }
    }
}