namespace NExpect.Interfaces;

/// <summary>
/// Provides the ".Unique" grammar continuation for collections
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICollectionUnique<T> 
    : ICanAddCollectionMatcher<T>
{
}