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
            ContinuationFactory.Create<string, StringAnd>(ActualFetcher, this);

        public StringMore(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}