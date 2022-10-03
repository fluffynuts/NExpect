namespace NExpect.Interfaces;

/// <summary>
/// Test all items in a collection (negated)
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICollectionItemsNot<T> : ICollectionItemsCanAddMatcher<T>
{
    /// <summary>
    /// Provides the .To grammar for negated item-wise collection matching
    /// </summary>
    public ICollectionItemsNotTo<T> To { get; }
}