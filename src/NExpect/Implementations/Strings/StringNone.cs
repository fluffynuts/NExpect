using System;
using NExpect.Interfaces;

namespace NExpect.Implementations.Strings;

internal class StringNone
    : ExpectationContextWithLazyActual<string>,
        IHasActual<string>,
        IStringNone
{
    public StringNone(Func<string> actualFetcher) : base(actualFetcher)
    {
    }
}