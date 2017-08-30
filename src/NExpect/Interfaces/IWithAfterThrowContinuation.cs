using System;
// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the ".With" grammar extension after .Throw() or .Throw&lt;&gt;
    /// </summary>
    public interface IWithAfterThrowContinuation<T> 
        : ICanAddMatcher<T> 
        where T : Exception
    {
        /// <summary>
        /// Provides the ".Message" continuation for testing exception message contents
        /// </summary>
        IExceptionPropertyContinuation<string> Message { get; }

        /// <summary>
        /// Provides the ".Property" continuation for testing custom exception properties
        /// </summary>
        IExceptionPropertyContinuation<TValue> Property<TValue>(Func<T, TValue> propertyValueFetcher);
    }
}