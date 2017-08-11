using System.Collections.Generic;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation from, eg At.Least(N)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICountMatchContinuation<T>: ICanAddMatcher<T>
    {
        /// <summary>
        /// Continuation to attempt to match the collection items exactly
        /// </summary>
        ICountMatchEqual<T> Equal { get; }

        ICountMatchMatched<T> Matched { get; }
    }


}