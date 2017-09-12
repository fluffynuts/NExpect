using System.Collections.Generic;
// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the ".To" grammar extension
    /// </summary>
    /// <typeparam name="T">Type of the continuation</typeparam>
    public interface ICollectionTo<T> : ICanAddMatcher<IEnumerable<T>>
    {
        /// <summary>
        /// Prepares to test if the value under test contains (an) expected value(s)
        /// </summary>
        IContain<IEnumerable<T>> Contain { get; }
        /// <summary>
        /// Negates the expectation
        /// </summary>
        ICollectionNotAfterTo<T> Not { get; }
        /// <summary>
        /// Prepares to test the state of the collection (eg, for emptiness)
        /// </summary>
        ICollectionBe<T> Be { get; }
        /// <summary>
        /// Prepares to test the state of the collection (eg, for uniqueness)
        /// </summary>
        ICollectionHave<T> Have { get; }
        /// <summary>
        /// Prepares for deep-equality testing
        /// </summary>
        ICollectionDeep<T> Deep { get; }
        /// <summary>
        /// Prepares for intersection-equality testing
        /// </summary>
        ICollectionIntersection<T> Intersection { get; }
    }
}