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

        public ICollectionHave<T> Have =>
            Factory.Create<IEnumerable<T>, CollectionHave<T>>(Actual, this);

        public CollectionNotAfterTo(IEnumerable<T> actual)
        {
            Actual = actual;
            Negate();
        }
    }

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