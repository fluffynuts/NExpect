namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the extension point for exception messages
    /// </summary>
    public interface IExceptionMessageContinuation
    {
        /// <summary>
        /// Begins the expectation for an exact match on the message
        /// </summary>
        IEqualityContinuation<string> Equal { get; }
        /// <summary>
        /// Negates the expectation
        /// </summary>
        INot<string> Not { get; }
    }
}