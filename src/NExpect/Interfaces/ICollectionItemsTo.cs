namespace NExpect.Interfaces;

/// <summary>
/// Test all items in a collection
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICollectionItemsTo<T> : ICollectionItemsCanAddMatcher<T>
{
    /// <summary>
    /// Negates the item-wise assertion on the collection
    /// </summary>
    public ICollectionItemsToNot<T> Not { get; }
}