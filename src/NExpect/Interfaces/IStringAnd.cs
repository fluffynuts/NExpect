namespace NExpect.Interfaces
{
    /// <summary>
    /// Starts .And for strings
    /// </summary>
    public interface IStringAnd
        : IAnd<string>
    {
        /// <summary>
        /// Starts .To for more continuations for strings
        /// </summary>
        new IStringTo To { get; }
        /// <summary>
        /// Starts .Not for more continuations for strings
        /// </summary>
        new IStringNot Not { get; }
    }
}