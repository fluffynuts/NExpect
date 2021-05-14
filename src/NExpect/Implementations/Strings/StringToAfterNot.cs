using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringToAfterNot
        : ToAfterNot<string>,
          IStringToAfterNot
    {
        public IStringStart Start => Next<StringStart>();
        public IStringEnd End => Next<StringEnd>();
        public new IStringBe Be => Next<StringBe>();

        public StringToAfterNot(Func<string> actualFetcher)
            : base(actualFetcher)
        {
        }
    }
}