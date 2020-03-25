using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency
{
    internal class Null<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        INull<T>
    {
        public INullOr<T> Or => ContinuationFactory.Create<T, NullOr<T>>(ActualFetcher, this);

        public Null(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}