using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CollectionHaveAnyEqual<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionHaveAnyEqual<T>
    {
        public IEnumerable<T> Actual { get; }
        public CollectionHaveAnyEqual(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}