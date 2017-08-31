using System.Collections.Generic;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class CollectionUnique<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionUnique<T>
    {
        public IEnumerable<T> Actual { get; }

        public CollectionUnique(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}