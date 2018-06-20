using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class StringPropertyAnd
        : ExpectationContext<string>,
            IStringPropertyContinuation
    {
        public string Actual { get; }

        public IStringPropertyNot Not
            => Factory.Create<string, StringPropertyNot>(Actual, this);

        public IStringPropertyContinuation And
            => Factory.Create<string, StringPropertyAnd>(Actual, this);

        public IEqualityContinuation<string> Equal
            => Factory.Create<string, EqualityContinuation<string>>(Actual, this);

        public IStringPropertyStartingContinuation Starting
            => Factory.Create<string, StringPropertyContinuation>(Actual, this);

        public IStringPropertyEndingContinuation Ending
            => Factory.Create<string, StringPropertyContinuation>(Actual, this);

        public StringPropertyAnd(string actual)
        {
            Actual = actual;
        }
    }
}