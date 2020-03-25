using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections
{
    internal class ContainAt<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        IContainAt<T>
    {
        public ContainAt(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}