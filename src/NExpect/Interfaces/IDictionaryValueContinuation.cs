namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the extension point for dictionary keys
    /// </summary>
    public interface IDictionaryValueContinuation<TValue>
    {
        /// <summary>
        /// Begins the expectation for an exact match on the Key Value
        /// </summary>
        IDictionaryValueWith<TValue> With { get; }

    }

}