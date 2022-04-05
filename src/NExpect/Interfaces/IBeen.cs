namespace NExpect.Interfaces;

/// <summary>
/// Continuation to provide the ".Been" grammar
/// </summary>
/// <typeparam name="T">Type of the continuation</typeparam>
public interface IBeen<T> : ICanAddMatcher<T>
{
}