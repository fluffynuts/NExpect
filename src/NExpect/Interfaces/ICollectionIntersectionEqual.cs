// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces;

/// <summary>
/// Provides the .Intersection.Equal grammar extension for collections
/// </summary>
/// <typeparam name="T">Collection item type</typeparam>
public interface ICollectionIntersectionEqual<T>
    : ICanAddCollectionMatcher<T>
{
}