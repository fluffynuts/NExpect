using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CollectionHaveAllEqual<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionHaveAllEqual<T>
    {
        public IEnumerable<T> Actual { get; }
        public CollectionHaveAllEqual(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}