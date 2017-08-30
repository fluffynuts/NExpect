using System.Collections.Generic;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class CollectionEquivalent<T>
        : ExpectationContext<IEnumerable<T>>,
            ICollectionEquivalent<T>
    {
        public IEnumerable<T> Actual { get; }

        public CollectionEquivalent(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}