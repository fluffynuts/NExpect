using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections
{
    internal class CollectionUnique<T> :
        ExpectationContext<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionUnique<T>
    {
        public IEnumerable<T> Actual { get; }

        public CollectionUnique(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}