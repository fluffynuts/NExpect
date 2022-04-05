using System.Collections.Generic;

// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces;

/// <summary>
/// Provides the .Equivalent grammar extension for .Deep
/// </summary>
/// <typeparam name="T">Collection item type</typeparam>
public interface ICollectionDeepEquivalent<T>: 
    ICanAddMatcher<IEnumerable<T>>
{
}