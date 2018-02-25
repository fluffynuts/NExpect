using System.Collections.Generic;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation from, eg Exactly(N), operating on collection of strings,
    /// to facilitate string-specific item testing.
    /// </summary>
    public interface ICountMatchContinuationOfStringCollection : 
        ICountMatchContinuation<IEnumerable<string>>
    {
        /// <summary>
        /// Tests for strings ending with a provided substring
        /// within a collection
        /// </summary>
        ICountMatchContinuationOfStringCollectionEnding Ending { get; }

        /// <summary>
        /// Tests for strings starting with a provided substring
        /// within a collection
        /// </summary>
        ICountMatchContinuationOfStringCollectionStarting Starting { get; }
    }
}