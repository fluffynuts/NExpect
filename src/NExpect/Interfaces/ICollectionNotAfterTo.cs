using System.Collections.Generic;
// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the ".Not" grammar extensions for collections, after an existing ".To"
    /// </summary>
    /// <typeparam name="T">Type of the collection</typeparam>
    public interface ICollectionNotAfterTo<T> : ICanAddMatcher<IEnumerable<T>>
    {
        /// <summary>
        /// Prepares to check if the object under test contains (an) expected value(s)
        /// </summary>
        IContain<IEnumerable<T>> Contain { get; }
        /// <summary>
        /// Prepares to test the state of the collection (eg, for emptiness)
        /// </summary>
        ICollectionBe<T> Be { get; }
        /// <summary>
        /// Prepares to test the state of the collection (eg, for uniqueness)
        /// </summary>
        ICollectionHave<T> Have { get; }
    }
}