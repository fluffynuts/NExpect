using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency;

// ReSharper disable once ClassNeverInstantiated.Global
internal class For<T>:
    ExpectationContextWithLazyActual<T>,
    IFor<T>
{
    public For(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}
// ReSharper disable once ClassNeverInstantiated.Global