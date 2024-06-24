using System;

namespace NExpect.Interfaces;

/// <summary>
/// Continuation for Throw
/// </summary>
public interface IThrowContinuation<out T> : IExpectationContext, ICanAddMatcher<T> where T : Exception
{
    /// <summary>
    /// Throw continuation to facilitate testing the exception message
    /// </summary>
    IWithAfterThrowContinuation<T> With { get; }
}

/// <summary>
/// Facilitates burrowing into inner exceptions
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IInnerExceptionAfterThrowContinuation<out T>
    : IThrowContinuation<T> where T : Exception
{
}

/// <summary>
/// Fluency continuation for Throw, after a Type() expectation
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IThrowAndContinuation<T> : ICanAddMatcher<T> where T : Exception
{
    /// <summary>
    /// Continuation to facilitate testing of properties or message on the exception
    /// </summary>
    IAndAfterWithAfterThrowContinuation<T> And { get; }
}

/// <summary>
/// Fluency continuation for Throw() after a Type() expectation and a prior .With
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAndAfterWithAfterThrowContinuation<T>
    : IWithAfterThrowContinuation<T> where T : Exception
{
}