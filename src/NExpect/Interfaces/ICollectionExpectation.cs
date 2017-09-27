namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the expected contract for expectations around collections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionExpectation<T>
    {
        /// <summary>
        /// Provides the ".To" grammar extension
        /// </summary>
        ICollectionTo<T> To { get; }
        /// <summary>
        /// Negates the current expectation
        /// </summary>
        ICollectionNot<T> Not { get; }

        /// <summary>
        /// Provides the .As syntax on collection expectations to transform
        /// item type
        /// </summary>
        ICollectionAs<T> As { get; }
    }
}