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
    }
}
