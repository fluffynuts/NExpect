// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces;

/// <summary>
/// Continuation to provide the ".A" grammar
/// </summary>
/// <typeparam name="T">Type of the continuation</typeparam>
public interface IA<T> : ICanAddMatcher<T>
{
    /// <summary>
    /// Provides the ".Valid" grammar
    /// </summary>
    IValid<T> Valid { get; }
}
