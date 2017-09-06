using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CollectionDeepEquivalent<T>:
        ExpectationContext<IEnumerable<T>>,
        ICollectionDeepEquivalent<T>
    {
        public IEnumerable<T> Actual { get; }
        public CollectionDeepEquivalent(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}