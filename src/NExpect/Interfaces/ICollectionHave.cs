namespace NExpect.Interfaces;

/// <summary>
/// Provides the ".Have" grammar continuation for collections
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICollectionHave<T> 
    : ICanAddCollectionMatcher<T>
{
    /// <summary>
    /// Prepares to do an match with an expected collection
    /// </summary>
    ICollectionUnique<T> Unique { get; }
}