using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class CollectionDeepEquivalent<T> :
        ExpectationContext<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionDeepEquivalent<T>
    {
        public IEnumerable<T> Actual { get; }

        public CollectionDeepEquivalent(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}