using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringAnd :
        And<string>,
        IStringAnd
    {
        public new IStringNot Not =>
            ContinuationFactory.Create<string, StringNot>(Actual, this);

        public IStringEnd End =>
            ContinuationFactory.Create<string, StringEnd>(Actual, this);

        public IStringStart Start =>
            ContinuationFactory.Create<string, StringStart>(Actual, this);

        public new IStringTo To =>
            ContinuationFactory.Create<string, StringTo>(Actual, this);

        public StringAnd(string actual)
            : base(actual)
        {
        }
    }
}