namespace NExpect.Interfaces
{
    /// <summary>
    /// Provide the negated item-wise assertions for collections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionItemsToNot<T>: ICollectionItemsCanAddMatcher<T>
    {
    }
}