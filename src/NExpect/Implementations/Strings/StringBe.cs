using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringBe :
        Be<string>,
        IStringBe
    {
        public IStringMatched Matched => Next<StringMatched>();

        public StringBe(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}