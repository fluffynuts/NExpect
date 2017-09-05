using System.Collections.Generic;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class CollectionDeepEqual<T>
        : ExpectationContext<IEnumerable<T>>,
            ICollectionDeepEqual<T>
    {
        IEnumerable<T> Actual { get; }

        public CollectionDeepEqual(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}