using System.Collections.Generic;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class CollectionDeep<T>: 
        ExpectationContext<IEnumerable<T>>,
        ICollectionDeep<T>
    {
        public ICollectionDeepEqual<T> Equal =>
            Factory.Create<IEnumerable<T>, CollectionDeepEqual<T>>(Actual, this);

        public ICollectionDeepEquivalent<T> Equivalent =>
            Factory.Create<IEnumerable<T>, CollectionDeepEquivalent<T>>(Actual, this);

        public IEnumerable<T> Actual { get; }
        public CollectionDeep(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}