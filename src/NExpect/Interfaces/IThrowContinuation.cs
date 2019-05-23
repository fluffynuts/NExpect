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

    public interface IThrowAndContinuation<T> : ICanAddMatcher<T> where T : Exception
    {
        IAndAfterWithAfterThrowContinuation<T> And { get; }
    }

    public interface IAndAfterWithAfterThrowContinuation<T>
        : IWithAfterThrowContinuation<T> where T: Exception
    {
    }
}