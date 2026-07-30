using System;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
namespace NExpect.Implementations.Strings;

internal class StringContain
    : ExpectationContextWithLazyActual<string>,
        IStringContain,
        IHasActual<string>
{
    public IStringIn In =>
        Next<StringIn>();
    public IStringAll All =>
        Next<StringAll>();
    public IStringNone None =>
        Next<StringNone>();

    public StringContain(Func<string> actualFetcher) : base(actualFetcher)
    {
    }
}