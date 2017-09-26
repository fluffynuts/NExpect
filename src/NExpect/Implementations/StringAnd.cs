using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class StringAnd: 
        And<string>, 
        IStringAnd
    {
        public new IStringNot Not => Factory.Create<string, StringNot>(Actual, this);
        public new IStringTo To => Factory.Create<string, StringTo>(Actual, this);

        public StringAnd(string actual): base(actual)
        {
        }
    }
}