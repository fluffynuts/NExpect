using System;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringContain
        : ExpectationContextWithLazyActual<string>,
            IStringContain,
            IHasActual<string>
    {
        public IStringIn In =>
            Next<StringIn>();

        public StringContain(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}