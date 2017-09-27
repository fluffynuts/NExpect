namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .As.Objects projection for collection expectations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionAs<T>
    {
        /// <summary>
        /// Provides a collection dumbed-down to Type object for the items
        /// to facilitate easier Deep and Intersection equality testing
        /// </summary>
        ICollectionExpectation<object> Objects { get; }
    }
}