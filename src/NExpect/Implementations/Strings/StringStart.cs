using System;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringStart :
        ExpectationContextWithLazyActual<string>,
        IHasActual<string>,
        IStringStart
    {
        public StringStart(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}