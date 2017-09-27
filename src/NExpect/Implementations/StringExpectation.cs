using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class StringExpectation :
        Expectation<string>,
        IStringExpectation
    {
        public new IStringTo To =>
            Factory.Create<string, StringTo>(Actual, this);

        public new IStringNot Not =>
            Factory.Create<string, StringNot>(Actual, this);

        public StringExpectation(string actual)
            : base(actual)
        {
        }
    }
}