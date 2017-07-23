using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CollectionHave<T>
        : ExpectationContext<IEnumerable<T>>,
            ICollectionHave<T>
    {
        public IEnumerable<T> Actual { get; }

        public CollectionHave(IEnumerable<T> actual)
        {
            Actual = actual;
        }

        public ICollectionHaveAll<T> All => 
            Factory.Create<IEnumerable<T>, CollectionHaveAll<T>>(Actual, this);

        public ICollectionHaveAny<T> Any =>
            Factory.Create<IEnumerable<T>, CollectionHaveAny<T>>(Actual, this);
    }
}