// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces;

/// <summary>
/// Provides the interface for the .For continuation
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFor<T>: ICanAddMatcher<T>
{
}