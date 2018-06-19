namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the extension point for exception messages
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public interface IExceptionCollectionPropertyContinuation<TValue>
        : ICollectionBe<TValue>
    {
    }
}