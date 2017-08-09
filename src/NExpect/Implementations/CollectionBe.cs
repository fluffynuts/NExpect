using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CollectionBe<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionBe<T>
    {
        public IEnumerable<T> Actual { get; }
        public CollectionBe(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}