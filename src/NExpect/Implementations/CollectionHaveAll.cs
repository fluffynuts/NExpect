using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CollectionHaveAll<T> : ExpectationContext<IEnumerable<T>>, ICollectionHaveAll<T>
    {
        public ICollectionHaveAllEqual<T> Equal =>
            Factory.Create<IEnumerable<T>, CollectionHaveAllEqual<T>>(Actual, this);

        public IEnumerable<T> Actual { get; }

        public CollectionHaveAll(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}