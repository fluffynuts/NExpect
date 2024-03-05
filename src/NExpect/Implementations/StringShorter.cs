using System;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations;

internal class StringShorter
    : ExpectationContextWithLazyActual<string>,
      IStringShorter,
      IHasActual<string>
{
    public StringShorter(Func<string> actualFetcher) : base(actualFetcher)
    {
    }
}