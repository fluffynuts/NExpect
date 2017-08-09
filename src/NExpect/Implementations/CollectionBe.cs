using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CollectionBe<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionBe<T>
    {
        public ICollectionEquivalent<T> Equivalent =>
            Factory.Create<IEnumerable<T>, CollectionEquivalent<T>>(Actual, this);

        public IEnumerable<T> Actual { get; }

        public CollectionBe(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}