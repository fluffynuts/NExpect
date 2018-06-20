using NExpect.Interfaces;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringPropertyContinuation
        : ExpectationContext<string>, 
            IStringPropertyContinuation,
            IStringPropertyStartingContinuation,
            IStringPropertyEndingContinuation
    {
        public string Actual { get; set; }

        public IStringPropertyContinuation And =>
            Factory.Create<string, StringPropertyAnd>(Actual, this);

        public IEqualityContinuation<string> Equal 
            => Factory.Create<string, EqualityContinuation<string>>(Actual, this);

        public IStringIn In =>
            Factory.Create<string, StringIn>(Actual, this);

        public IStringPropertyNot Not 
            => Factory.Create<string, StringPropertyNot>(Actual, this);
        
        public IStringPropertyEndingContinuation Ending
            => Factory.Create<string, StringPropertyContinuation>(Actual, this);

        public IStringPropertyStartingContinuation Starting 
            => Factory.Create<string, StringPropertyContinuation>(Actual, this);

        public StringPropertyContinuation(string actual)
        {
            Actual = actual;
        }
    }
}