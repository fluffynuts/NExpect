using System.Collections.Generic;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the ".To" grammar extension after an existing ".Not"
    /// </summary>
    /// <typeparam name="T">Type of the continuation</typeparam>
    public interface ICollectionToAfterNot<T> : ICanAddMatcher<IEnumerable<T>>
    {
        /// <summary>
        /// Prepares to test if the collection under test contains (an) expected value(s)
        /// </summary>
        IContain<IEnumerable<T>> Contain { get; }
        /// <summary>
        /// Prepares to test the state of the collection (eg, for emptiness)
        /// </summary>
        ICollectionBe<T> Be { get; }
    }
}