using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringAnd
        : And<string>,
          IStringAnd
    {
        public new IStringNot Not => Next<StringNot>();
        public IStringEnd End => Next<StringEnd>();
        public IStringStart Start => Next<StringStart>();
        public new IStringTo To => Next<StringTo>();

        public StringAnd(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}