using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CollectionExpectation<T> :
        Expectation<IEnumerable<T>>,
        ICollectionExpectation<T>
    {
        public CollectionExpectation(IEnumerable<T> actual)
            : base(actual)
        {
        }

        public new ICollectionTo<T> To =>
            Factory.Create<IEnumerable<T>, CollectionTo<T>>(Actual, this);

        public new ICollectionNot<T> Not =>
            Factory.Create<IEnumerable<T>, CollectionNot<T>>(Actual, this);
    }
}