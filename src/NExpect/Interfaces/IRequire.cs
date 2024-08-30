namespace NExpect.Interfaces;

/// <summary>
/// Provides the .Require grammar extension
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRequire<T> : ICanAddMatcher<T>
{
}