namespace NExpect.Interfaces;

/// <summary>
/// Continuation to provide the ".Default" grammar
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDefault<T> : ICanAddMatcher<T>
{
}