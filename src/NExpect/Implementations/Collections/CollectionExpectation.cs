using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations.Collections
{
    internal class CollectionExpectation<T> :
        Expectation<IEnumerable<T>>,
        ICollectionExpectation<T>
    {
        public new ICollectionTo<T> To =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionTo<T>>(() => Actual, this);

        public new ICollectionNot<T> Not =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionNot<T>>(() => Actual, this);

        public ICollectionAs<T> As =>
            ContinuationFactory.Create<IEnumerable<T>, CollectionAs<T>>(() => Actual, this);

        public CollectionExpectation(IEnumerable<T> actual)
            : base(actual)
        {
        }
    }
}