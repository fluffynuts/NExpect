using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings;

internal class StringBe :
    Be<string>,
    IStringBe
{
    IStringMatched IStringBe.Matched => Next<StringMatched>();
    public IStringShorter Shorter => Next<StringShorter>();
    public IStringLonger Longer => Next<StringLonger>();

    public StringBe(Func<string> actualFetcher) : base(actualFetcher)
    {
    }
}