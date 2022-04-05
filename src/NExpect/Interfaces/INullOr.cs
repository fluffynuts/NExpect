// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces;

/// <summary>
/// Continuation to test for null or something else (typically empty or whitespace)
/// </summary>
/// <typeparam name="T"></typeparam>
public interface INullOr<T>: ICanAddMatcher<T>
{
}