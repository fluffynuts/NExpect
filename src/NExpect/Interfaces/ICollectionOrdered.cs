namespace NExpect.Interfaces;

/// <summary>
/// Provides the .Ordered grammar extension
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICollectionOrdered<T>: 
    ICanAddCollectionMatcher<T>
{
}