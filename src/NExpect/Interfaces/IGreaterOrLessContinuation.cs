// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces;

/// <summary>
/// Provides the generic extension point for greater/less,
/// to enable creating extension methods for both with one extension point
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGreaterOrLessContinuation<T>: ICanAddMatcher<T>
{
}