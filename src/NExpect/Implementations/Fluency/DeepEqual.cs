using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency;

// ReSharper disable once ClassNeverInstantiated.Global
internal class DeepEqual<T>
    : ExpectationContextWithLazyActual<T>, 
      IDeepEqual<T>,
      IHasActual<T>
{
    public DeepEqual(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}