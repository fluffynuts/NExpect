using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringTo :
        To<string>,
        IStringTo
    {
        public IStringStart Start =>
            ContinuationFactory.Create<string, StringStart>(ActualFetcher, this);

        public IStringEnd End =>
            ContinuationFactory.Create<string, StringEnd>(ActualFetcher, this);

        public new IStringNotAfterTo Not =>
            ContinuationFactory.Create<string, StringNotAfterTo>(ActualFetcher, this);

        public new IStringBe Be =>
            ContinuationFactory.Create<string, StringBe>(ActualFetcher, this);

        public new IStringContain Contain =>
            ContinuationFactory.Create<string, StringContain>(ActualFetcher, this);

        public StringTo(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}