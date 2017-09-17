using System.Collections.Generic;
using System.Linq;

namespace NExpect
{
    /// <summary>
    /// Provides some convenience extensions for deep equality testing
    /// </summary>
    public static class DeepEqualityTestingHelperExtensions
    {
        /// <summary>
        /// "Dumbs down" a collection to be of IEnumerable&lt;object&gt; 
        ///   to make deep equality testing convenient on different types
        /// </summary>
        /// <param name="collection">Collection to convert</param>
        /// <typeparam name="T">Original item type</typeparam>
        /// <returns></returns>
        public static IEnumerable<object> AsObjects<T>(this IEnumerable<T> collection)
        {
            return collection.Select(o => o as object);
        }
    }
}