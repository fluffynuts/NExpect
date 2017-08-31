using System.Collections.Generic;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class CollectionHave<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionHave<T>
    {
        public ICollectionUnique<T> Unique =>
            Factory.Create<IEnumerable<T>, CollectionUnique<T>>(Actual, this);

        public IEnumerable<T> Actual { get; }

        public CollectionHave(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}