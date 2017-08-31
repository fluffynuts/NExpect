using System.Collections.Generic;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class CollectionEqual<T>
        : ExpectationContext<IEnumerable<T>>,
            ICollectionEqual<T>
    {
        public IEnumerable<T> Actual { get; }

        public CollectionEqual(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}