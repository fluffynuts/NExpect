using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringAnd :
        And<string>,
        IStringAnd
    {
        public new IStringNot Not =>
            Factory.Create<string, StringNot>(Actual, this);

        public IStringEnd End =>
            Factory.Create<string, StringEnd>(Actual, this);

        public IStringStart Start =>
            Factory.Create<string, StringStart>(Actual, this);

        public new IStringTo To =>
            Factory.Create<string, StringTo>(Actual, this);

        public StringAnd(string actual)
            : base(actual)
        {
        }
    }
}