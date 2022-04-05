using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings;

internal class StringTo
    : To<string>,
      IStringTo
{
    public IStringStart Start => Next<StringStart>();
    public IStringEnd End => Next<StringEnd>();
    public new IStringNotAfterTo Not => Next<StringNotAfterTo>();
    public new IStringBe Be => Next<StringBe>();
    public new IStringContain Contain => Next<StringContain>();

    public StringTo(Func<string> actualFetcher) : base(actualFetcher)
    {
    }
}