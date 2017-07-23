using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CollectionHaveAny<T> : ExpectationContext<IEnumerable<T>>, ICollectionHaveAny<T>
    {
        public ICollectionHaveAnyEqual<T> Equal =>
            Factory.Create<IEnumerable<T>, CollectionHaveAnyEqual<T>>(Actual, this);

        public IEnumerable<T> Actual { get; }

        public CollectionHaveAny(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}