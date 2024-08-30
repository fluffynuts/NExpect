namespace NExpect.Interfaces;

/// <summary>
/// Provides the .Valid continuation
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IValid<T> : ICanAddMatcher<T>
{
}