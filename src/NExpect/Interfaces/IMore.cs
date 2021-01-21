namespace NExpect.Interfaces
{
    /// <summary>
    /// Allows user-chaining of .And (ie, "More expectations plz")
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMore<T>
    {
        /// <summary>
        /// Provides the .And grammar extension
        /// </summary>
        IAnd<T> And { get; }

        /// <summary>
        /// Provides the .With grammar extension
        /// </summary>
        IWith<T> With { get; }

        /// <summary>
        /// Provides the .Of grammar extension
        /// </summary>
        IOf<T> Of { get; }
    }
}
