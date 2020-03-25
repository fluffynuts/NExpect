using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Collections
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Contain<T> :
        ExpectationContextWithLazyActual<T>, 
        IHasActual<T>,
        IContain<T>
    {
        public IContainAt<T> At =>
            ContinuationFactory.Create<T, ContainAt<T>>(ActualFetcher, this);

        public Contain(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}