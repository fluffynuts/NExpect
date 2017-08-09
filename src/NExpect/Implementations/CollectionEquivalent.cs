using System.Collections.Generic;
using NExpect.Interfaces;

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