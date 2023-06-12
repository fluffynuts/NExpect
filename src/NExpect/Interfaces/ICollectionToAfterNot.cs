using System.Collections.Generic;

// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces;

/// <summary>
/// Provides the ".To" grammar extension after an existing ".Not"
/// </summary>
/// <typeparam name="T">Type of the continuation</typeparam>
public interface ICollectionToAfterNot<T> 
    : ICanAddCollectionMatcher<T>
{
    /// <summary>
    /// Prepares to test if the collection under test contains (an) expected value(s)
    /// </summary>
    IContain<IEnumerable<T>> Contain { get; }
    /// <summary>
    /// Prepares to test the state of the collection (eg, for emptiness)
    /// </summary>
    ICollectionBe<T> Be { get; }
    /// <summary>
    /// Prepares to test the state of the collection (eg, for uniqueness)
    /// </summary>
    ICollectionHave<T> Have { get; }
    /// <summary>
    /// Provides the negated .Deep grammar extension for collection
    /// deep-equality testing
    /// </summary>
    ICollectionDeep<T> Deep { get; }
    /// <summary>
    /// Provides the negated .Deep grammar extension for collection
    /// intersection-equality testing
    /// </summary>
    ICollectionIntersection<T> Intersection { get; }
}