using System.Collections.Generic;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class CollectionIntersectionEquivalent<T>:
        ExpectationContext<IEnumerable<T>>,
        ICollectionIntersectionEquivalent<T>
    {
        public IEnumerable<T> Actual { get; }
        public CollectionIntersectionEquivalent(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}