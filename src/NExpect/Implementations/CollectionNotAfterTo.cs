using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class CollectionNotAfterTo<T>
        : ExpectationContext<IEnumerable<T>>,
            ICollectionNotAfterTo<T>
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public IEnumerable<T> Actual { get; }

        public IContain<IEnumerable<T>> Contain =>
            Factory.Create<IEnumerable<T>, Contain<IEnumerable<T>>>(Actual, this);

        public ICollectionBe<T> Be =>
            Factory.Create<IEnumerable<T>, CollectionBe<T>>(Actual, this);

        public ICollectionHave<T> Have =>
            Factory.Create<IEnumerable<T>, CollectionHave<T>>(Actual, this);

        public ICollectionDeep<T> Deep =>
            Factory.Create<IEnumerable<T>, CollectionDeep<T>>(Actual, this);

        public CollectionNotAfterTo(IEnumerable<T> actual)
        {
            Actual = actual;
            Negate();
        }
    }
}