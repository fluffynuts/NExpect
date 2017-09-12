using System.Collections.Generic;
// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the ".Be" grammar continuation for collections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionBe<T> : ICanAddMatcher<IEnumerable<T>>
    {
        /// <summary>
        /// Prepares to do an out-of-order match with an expected collection
        /// </summary>
        ICollectionEquivalent<T> Equivalent { get; }

        /// <summary>
        /// Prepares to do an in-order match with an expected collection
        /// </summary>
        ICollectionEqual<T> Equal { get; }

        /// <summary>
        /// Prepares for deep-equality testing
        /// </summary>
        ICollectionDeep<T> Deep { get; }

        /// <summary>
        /// Prepares for intersection-equality testing
        /// -> only item properties with the same name and type are tested
        /// </summary>
        ICollectionIntersection<T> Intersection { get; }

        /// <summary>
        /// Prepares for instance testing
        /// </summary>
        ICollectionAn<T> An { get; }
    }
}