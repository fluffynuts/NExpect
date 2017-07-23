using System;

namespace NExpect.Interfaces
{
    public interface IWithAfterThrowContinuation : ICanAddMatcher<Exception>
    {
        IExceptionMessageContinuation Message { get; }
    }
}