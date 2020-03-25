using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringNot :
        Not<string>,
        IStringNot
    {
        public IStringStart Start =>
            ContinuationFactory.Create<string, StringStart>(ActualFetcher, this);

        public IStringEnd End =>
            ContinuationFactory.Create<string, StringEnd>(ActualFetcher, this);

        public new IStringToAfterNot To =>
            ContinuationFactory.Create<string, StringToAfterNot>(ActualFetcher, this);

        public StringNot(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}