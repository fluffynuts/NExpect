using System;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation for Throw
    /// </summary>
    public interface IThrowContinuation<T>: ICanAddMatcher<T> where T : Exception
    {
        /// <summary>
        /// Throw continuation to facilitate testing the exception message
        /// </summary>
        IWithAfterThrowContinuation<T> With { get; }
    }
}