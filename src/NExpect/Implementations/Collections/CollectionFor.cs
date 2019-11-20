using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Collections
{
    internal class CollectionFor<T> :
        ExpectationContext<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionFor<T>
    {
        public IEnumerable<T> Actual { get; }

        public CollectionFor(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}