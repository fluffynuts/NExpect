using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class StringPropertyNot
        : ExpectationContext<string>, IStringPropertyNot
    {
        public string Actual { get; set; }

        public IToAfterNot<string> To 
            => Factory.Create<string, ToAfterNot<string>>(Actual, this);

        public StringPropertyNot(string actual)
        {
            Actual = actual;
            Negate();
        }
    }
}