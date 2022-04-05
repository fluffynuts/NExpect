using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings;

internal class StringNotAfterTo
    : NotAfterTo<string>,
      IStringNotAfterTo
{
    public IStringStart Start => Next<StringStart>();
    public IStringEnd End => Next<StringEnd>();
    public new IStringBe Be => Next<StringBe>();

    public StringNotAfterTo(Func<string> actualFetcher) : base(actualFetcher)
    {
    }
}