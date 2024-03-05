using System;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations;

internal class StringLonger
    : ExpectationContextWithLazyActual<string>,
      IStringLonger,
      IHasActual<string>
{
    public StringLonger(Func<string> actualFetcher) : base(actualFetcher)
    {
    }
}