namespace NExpect.Interfaces;

/// <summary>
/// Provides the .Which continuation
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IWhich<T> : ICanAddMatcher<T>
{
}