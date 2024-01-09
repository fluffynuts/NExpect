namespace NExpect.Interfaces;

/// <summary>
/// Prepares for equivalence matching, eg when comparing
/// two documents where whitespace doesn't matter
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEquivalenceContinuation<T> : ICanAddMatcher<T>
{
}