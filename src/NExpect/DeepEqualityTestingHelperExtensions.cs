using System;
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
        [Obsolete("Please use the 'Expect({collection}).As.Objects...' syntax. This extension method will be deprecated in a future release")]
        public static IEnumerable<object> AsObjects<T>(this IEnumerable<T> collection)
        {
            return collection.Select(o => o as object);
        }
    }
}