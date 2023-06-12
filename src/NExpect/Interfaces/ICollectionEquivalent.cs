// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces;

/// <summary>
/// Provides the ".Equivalent" grammar extension
/// </summary>
/// <typeparam name="T">Type of the continuation</typeparam>
public interface ICollectionEquivalent<T>: ICanAddCollectionMatcher<T>
{
}