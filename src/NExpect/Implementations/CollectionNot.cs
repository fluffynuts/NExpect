using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CollectionNot<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionNot<T>
    {
        public IEnumerable<T> Actual { get; }

        public CollectionNot(IEnumerable<T> actual)
        {
            Actual = actual;
            Negate();
        }

        public ICollectionToAfterNot<T> To =>
            Factory.Create<IEnumerable<T>, CollectionToAfterNot<T>>(Actual, this);

    }
}