using System;
using System.Collections.Generic;

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
        IStringPropertyContinuation Message { get; }

        /// <summary>
        /// Provides the ".Property" continuation for testing custom exception properties
        /// </summary>
        IBe<TValue> Property<TValue>(
            Func<T, TValue> propertyValueFetcher
        );

        /// <summary>
        /// Provides the .CollectionProperty continuation for testing collection
        ///     properties on thrown exceptions
        /// </summary>
        /// <param name="propertyValueFetcher">Fetches the property</param>
        /// <typeparam name="TItem">Type of underlying item</typeparam>
        /// <returns></returns>
        IExceptionCollectionPropertyContinuation<TItem> CollectionProperty<TItem>(
            Func<T, IEnumerable<TItem>> propertyValueFetcher
        );
    }
}