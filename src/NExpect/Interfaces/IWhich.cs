using System.Collections.Generic;

namespace NExpect.Interfaces;

/// <summary>
/// Provides the .Which continuation
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IWhich<T> : ICanAddMatcher<T>
{
    /// <summary>
    /// Provides for the grammar extension point
    /// .Which.Is
    /// </summary>
    public IIs<T> Is { get; }
}