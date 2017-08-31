using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CollectionTo<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionTo<T>
    {
        public IEnumerable<T> Actual { get; }

        public IContain<IEnumerable<T>> Contain
            => Factory.Create<IEnumerable<T>, Contain<IEnumerable<T>>>(Actual, this);

        public ICollectionNotAfterTo<T> Not =>
            Factory.Create<IEnumerable<T>, CollectionNotAfterTo<T>>(Actual, this);

        public ICollectionBe<T> Be =>
            Factory.Create<IEnumerable<T>, CollectionBe<T>>(Actual, this);

        public ICollectionHave<T> Have =>
            Factory.Create<IEnumerable<T>, CollectionHave<T>>(Actual, this);

        public CollectionTo(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}