using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect
{
    internal static class CollectionFluencyExtensions
    {
        internal static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return !collection?.Any() 
                   ?? throw new ArgumentNullException(nameof(collection), 
                       "Cannot test null for emptiness");

        }

        internal static bool IsDistinct<T>(this IEnumerable<T> collection)
        {
            return (collection == null)
                // ReSharper disable once ExpressionIsAlwaysNull
                ? throw new ArgumentNullException(nameof(collection), $"Expected IEnumerable<{typeof(T).Name}>, but found {collection.LimitedPrint()}")
                : collection.Distinct().Count() == collection.Count();
        }
    }
}