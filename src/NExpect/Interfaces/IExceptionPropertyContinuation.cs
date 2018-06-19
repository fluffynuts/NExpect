namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the extension point for exception messages
    /// </summary>
    public interface IExceptionPropertyContinuation<TValue>
    {
        /// <summary>
        /// Begins the expectation for an exact match on the message
        /// </summary>
        IEqualityContinuation<TValue> Equal { get; }
        /// <summary>
        /// Negates the expectation
        /// </summary>
        IPropertyNot<TValue> Not { get; }
    }
}