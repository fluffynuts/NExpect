namespace NExpect.Interfaces;

/// <summary>
/// Provides item-wise collection assertion grammar
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICollectionItems<T>
{
    /// <summary>
    /// Prepare for an item-wise collection assertion
    /// </summary>
    public ICollectionItemsTo<T> To { get; }

    /// <summary>
    /// Prepare for a negated item-wise collection assertion
    /// </summary>
    public ICollectionItemsNot<T> Not { get; }
}