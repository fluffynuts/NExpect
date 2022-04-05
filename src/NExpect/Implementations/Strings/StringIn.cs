using System;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings;

internal class StringIn
    : ExpectationContextWithLazyActual<string>,
      IHasActual<string>,
      IStringIn
{
    public StringIn(Func<string> actualFetcher) : base(actualFetcher)
    {
    }
}