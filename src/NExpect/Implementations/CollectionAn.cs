using System.Collections.Generic;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class CollectionAn<T> :
        ExpectationContext<IEnumerable<T>>,
        IHasActual<IEnumerable<T>>,
        ICollectionAn<T>
    {
        public IInstanceContinuation Instance => 
            new InstanceContinuation(Actual.GetType(), this);

        public IEnumerable<T> Actual { get; }

        public CollectionAn(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}