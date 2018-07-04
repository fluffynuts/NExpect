using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class StringExpectation :
        Expectation<string>,
        IStringExpectation
    {
        public new IStringTo To =>
            ContinuationFactory.Create<string, StringTo>(Actual, this);

        public new IStringNot Not =>
            ContinuationFactory.Create<string, StringNot>(Actual, this);

        public StringExpectation(string actual)
            : base(actual)
        {
        }
    }
}