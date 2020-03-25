using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency
{
    internal class Deep<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        IDeep<T>
    {
        public IDeepEqual<T> Equal 
            => ContinuationFactory.Create<T, DeepEqual<T>>(ActualFetcher, this);

        public Deep(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}