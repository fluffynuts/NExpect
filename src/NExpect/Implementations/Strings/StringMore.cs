using System;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringMore
        : ExpectationContextWithLazyActual<string>,
          IHasActual<string>,
          IStringMore
    {
        public IStringAnd And =>
            Next<StringAnd>();

        public StringMore(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}