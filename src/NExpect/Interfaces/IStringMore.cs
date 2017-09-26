namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides chaining of expectations with extra string-specific
    /// grammar
    /// </summary>
    public interface IStringMore
    {
        /// <summary>
        /// Starts .And for more continuations for strings
        /// </summary>
        IStringAnd And { get; }
    }
}