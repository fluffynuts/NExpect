// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the initial ".To" grammar extension
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITo<T> : ICanAddMatcher<T>
    {
        /// <summary>
        /// Negates the expectation
        /// </summary>
        INotAfterTo<T> Not { get; }
        /// <summary>
        /// Starts an expectation some kind of similarity
        /// </summary>
        IBe<T> Be { get; }
        /// <summary>        
        /// Starts a test for contains on arbitrary objects
        /// </summary>
        IContain<T> Contain { get; }
        /// <summary>
        /// Starts an expectation for some property
        /// </summary>
        IHave<T> Have { get; }
        /// <summary>
        /// Starts an expectation for a deep equality test
        /// </summary>
        IDeep<T> Deep { get; }
        /// <summary>
        /// Starts an expectation for an intersection equality test
        /// -> only tests properties and fields with the same name and type
        /// </summary>
        IIntersection<T> Intersection { get; }

        /// <summary>
        /// Starts an expectation for approximate equality
        /// </summary>
        IApproximately<T> Approximately { get; }
    }
}