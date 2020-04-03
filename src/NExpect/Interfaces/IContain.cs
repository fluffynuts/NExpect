// ReSharper disable InheritdocConsiderUsage

using System.Collections.Generic;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the ".Contain" grammar extension
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContain<T> : ICanAddMatcher<T>
    {
        /// <summary>
        /// Prepares for setting an expected count to test for, eg:
        /// At.Least, At.Most
        /// </summary>
        IContainAt<T> At { get; }

        /// <summary>
        /// Prepares for matching no items in a non-empty collection
        /// </summary>
        ICountMatchContinuation<T> None { get; }

        /// <summary>
        /// Prepares for matching an empty collection
        /// </summary>
        ICountMatchContinuation<T> No { get; }

        /// <summary>
        /// Prepares for matching the entire collection
        /// </summary>
        ICountMatchContinuation<T> All { get; }

        /// <summary>
        /// Prepares for matching any item in the collection
        /// </summary>
        ICountMatchContinuation<T> Any { get; }
    }
}