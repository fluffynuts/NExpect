using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringNotAfterTo :
        NotAfterTo<string>,
        IStringNotAfterTo
    {
        public IStringStart Start =>
            ContinuationFactory.Create<string, StringStart>(ActualFetcher, this);

        public IStringEnd End =>
            ContinuationFactory.Create<string, StringEnd>(ActualFetcher, this);

        public new IStringBe Be =>
            ContinuationFactory.Create<string, StringBe>(ActualFetcher, this);

        public StringNotAfterTo(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}