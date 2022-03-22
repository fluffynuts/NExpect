// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation from, eg At.Least(N)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICountMatchContinuation<T>: ICanAddMatcher<T>, ICountMatch
    {
        /// <summary>
        /// Continuation to attempt to match the collection items exactly
        /// </summary>
        ICountMatchEqual<T> Equal { get; }

        /// <summary>
        /// Continuation to attempt to match the collection items with a user-defined
        /// function
        /// </summary>
        ICountMatchMatched<T> Matched { get; }

        /// <summary>
        /// Prepares for deep equality testing
        /// </summary>
        ICountMatchDeep<T> Deep { get; }

        /// <summary>
        /// Prepares for intersection equality testing
        /// </summary>
        ICountMatchIntersection<T> Intersection { get; }

        /// <summary>
        /// Prepares for type matching
        /// </summary>
        ICountMatchOf<T> Of { get; }
    }
}