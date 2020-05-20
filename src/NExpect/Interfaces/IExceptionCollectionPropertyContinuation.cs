using System.Collections.Generic;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the extension point for exception messages
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public interface IExceptionCollectionPropertyContinuation<TValue>
        : ICollectionBe<TValue>
    {
        /// <summary>
        /// Provides the .Containing continuation for collection properties pulled off of exception
        /// </summary>
        IContain<IEnumerable<TValue>> Containing { get; }

        /// <summary>
        /// Provides negated collection continuations for properties grabbed off of exceptions
        /// </summary>
        ICollectionPropertyContinuationNot<TValue> Not { get; }
    }

    /// <summary>
    /// Provides negated collection continuations for properties grabbed off of exceptions
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface ICollectionPropertyContinuationNot<TValue>
    {
        /// <summary>
        /// Provides the negated .Containing continuation for collection properties pulled off of exception
        /// </summary>
        IContain<IEnumerable<TValue>> Containing { get; }
    }

}