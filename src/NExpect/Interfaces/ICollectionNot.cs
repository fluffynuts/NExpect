namespace NExpect.Interfaces;

/// <summary>
/// Provides the ".Not" grammar extension for collections.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICollectionNot<T> 
    : ICanAddCollectionMatcher<T>
{
    /// <summary>
    /// Provides the negated ".To" grammar extension for collections
    /// </summary>
    ICollectionToAfterNot<T> To { get; }
}