using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency;

internal class Deep<T> :
    ExpectationContextWithLazyActual<T>,
    IHasActual<T>,
    IDeep<T>
{
    public IDeepEqual<T> Equal => Next<DeepEqual<T>>();

    public Deep(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}