// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces;

/// <summary>
/// Provides the .Intersection syntax for collection
/// </summary>
/// <typeparam name="T">Collection item type</typeparam>
public interface ICollectionIntersection<T>
    : ICanAddCollectionMatcher<T>
{
    /// <summary>
    /// Provides the .Equivalent grammar extension
    /// </summary>
    ICollectionIntersectionEquivalent<T> Equivalent { get; }
    /// <summary>
    /// Provides the .Equal grammar extension
    /// </summary>
    ICollectionIntersectionEqual<T> Equal { get; }
}