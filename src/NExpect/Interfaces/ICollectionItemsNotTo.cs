namespace NExpect.Interfaces;

/// <summary>
/// Test all items in a collection (negated)
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICollectionItemsNotTo<T> : ICollectionItemsCanAddMatcher<T>
{
}