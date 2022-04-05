namespace NExpect.Interfaces;

/// <summary>
/// Provides the .With dangling grammar type
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IWith<T> : ICanAddMatcher<T>
{
    /// <summary>
    /// Provides the .Required dangling grammar type
    /// </summary>
    public IRequired<T> Required { get; }
}