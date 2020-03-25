using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringAnd :
        And<string>,
        IStringAnd
    {
        public new IStringNot Not =>
            ContinuationFactory.Create<string, StringNot>(ActualFetcher, this);

        public IStringEnd End =>
            ContinuationFactory.Create<string, StringEnd>(ActualFetcher, this);

        public IStringStart Start =>
            ContinuationFactory.Create<string, StringStart>(ActualFetcher, this);

        public new IStringTo To =>
            ContinuationFactory.Create<string, StringTo>(ActualFetcher, this);

        public StringAnd(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}