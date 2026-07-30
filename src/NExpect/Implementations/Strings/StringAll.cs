using System;
using NExpect.Interfaces;

namespace NExpect.Implementations.Strings;

internal class StringAll
    : ExpectationContextWithLazyActual<string>,
        IHasActual<string>,
        IStringAll
{
    public StringAll(Func<string> actualFetcher) : base(actualFetcher)
    {
    }
}