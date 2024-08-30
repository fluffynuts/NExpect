namespace NExpect.Interfaces;

/// <summary>
/// Provides the .And continuation for collections
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICollectionAnd<T> 
    : ICanAddCollectionMatcher<T>
{
    /// <summary>
    /// Provides the ".To" grammar extension
    /// </summary>
    ICollectionTo<T> To { get; }

    /// <summary>
    /// Negates the current expectation
    /// </summary>
    ICollectionNot<T> Not { get; }
}