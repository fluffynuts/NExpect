using System;

namespace NExpect.MatcherLogic
{
    /// <summary>
    /// Matcher result with a dangling Next value for
    /// continuations where the underlying type is being switched up
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMatcherResultWithNext<T>
        : IMatcherResult
    {
        /// <summary>
        /// Next value to continue with
        /// </summary>
        Func<T> NextFetcher { get; }
    }
}