using System;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringMatched :
        ExpectationContextWithLazyActual<string>,
        IStringMatched,
        IHasActual<string>
    {
        public StringMatched(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}