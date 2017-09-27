using System.Collections.Generic;
using System.Linq;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class CollectionAs<T>:
        ExpectationContext<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionAs<T>
    {
        public IEnumerable<T> Actual { get; }

        public ICollectionExpectation<object> Objects =>
            new CollectionExpectation<object>(Actual.Select(o => o as object));

        public CollectionAs(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}