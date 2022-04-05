using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings;

internal class StringNot
    : Not<string>,
      IStringNot
{
    public IStringStart Start => Next<StringStart>();
    public IStringEnd End => Next<StringEnd>();
    public new IStringToAfterNot To => Next<StringToAfterNot>();

    public StringNot(Func<string> actualFetcher) : base(actualFetcher)
    {
    }
}