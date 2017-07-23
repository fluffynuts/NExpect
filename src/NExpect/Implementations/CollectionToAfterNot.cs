using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CollectionToAfterNot<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionToAfterNot<T>
    {
        public IEnumerable<T> Actual { get; }

        public IContain<IEnumerable<T>> Contain =>
            Factory.Create<IEnumerable<T>, Contain<IEnumerable<T>>>(Actual, this);

        public ICollectionHave<T> Have =>
            Factory.Create<IEnumerable<T>, CollectionHave<T>>(Actual, this);

        public CollectionToAfterNot(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}