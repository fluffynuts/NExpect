using System.Collections.Generic;

namespace NExpect.Interfaces;

/// <summary>
/// Provides the extension point for Deep Equality Testing on collections
/// </summary>
/// <typeparam name="T"></typeparam>
// ReSharper disable once InheritdocConsiderUsage
public interface ICollectionDeep<T> :
    ICanAddMatcher<IEnumerable<T>>
{
    /// <summary>
    /// Prepares to do an in-order match with an expected collection
    /// </summary>
    ICollectionDeepEqual<T> Equal { get; }

    /// <summary>
    /// Prepares to do an out-of-order match with an expected collection
    /// </summary>
    ICollectionDeepEquivalent<T> Equivalent { get; }
}