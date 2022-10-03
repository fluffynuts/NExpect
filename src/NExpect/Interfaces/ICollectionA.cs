using System.Collections.Generic;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation to provide the ".A" grammar for collections
    /// </summary>
    /// <typeparam name="T">Type of the continuation</typeparam>
    public interface ICollectionA<T>: ICanAddMatcher<IEnumerable<T>>
    {
        /// <summary>
        /// Test if the provided actual collection is a superset of another
        /// </summary>
        public ISuperset<T> Superset { get; }
        /// <summary>
        /// Test if the provided actual collection is a subset of another
        /// </summary>
        public ISubset<T> Subset { get; }
    }
}