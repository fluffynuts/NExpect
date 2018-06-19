using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class ExceptionStringPropertyContinuation : Be<string>, IStringPropertyContinuation
    {
        public new IStringPropertyNot Not 
            => Factory.Create<string, StringPropertyNot>(Actual, this);

        public IStringPropertyContinuation And 
            => Factory.Create<string, StringPropertyAnd>(Actual, this);

        public ExceptionStringPropertyContinuation(string actual) : base(actual)
        {
        }
    }
}