// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces;

/// <summary>
/// Provides the ".At" grammar continuation
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IContainAt<T>: ICanAddMatcher<T>
{
}