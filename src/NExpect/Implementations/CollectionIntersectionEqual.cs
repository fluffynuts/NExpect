using System.Collections.Generic;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class CollectionIntersectionEqual<T> :
        ExpectationContext<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionIntersectionEqual<T>
    {
        public IEnumerable<T> Actual { get; }

        public CollectionIntersectionEqual(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}