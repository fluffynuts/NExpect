using System.Collections.Generic;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the ".Have" grammar continuation for collections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionHave<T> : ICanAddMatcher<IEnumerable<T>>
    {
        /// <summary>
        /// Prepares to do an match with an expected collection
        /// </summary>
        ICollectionUnique<T> Unique { get; }
    }
}