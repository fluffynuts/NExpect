using System;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringEnd :
        ExpectationContextWithLazyActual<string>,
        IHasActual<string>,
        IStringEnd
    {
        public StringEnd(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}