using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class ExceptionStringPropertyContinuation : 
        Be<string>, 
        IStringPropertyContinuation
    {
        public new IStringPropertyNot Not 
            => Factory.Create<string, StringPropertyNot>(Actual, this);

        public IStringPropertyContinuation And 
            => Factory.Create<string, StringPropertyAnd>(Actual, this);

        public IStringPropertyStartingContinuation Starting 
            => Factory.Create<string, StringPropertyContinuation>(Actual, this);
        
        public IStringPropertyEndingContinuation Ending 
            => Factory.Create<string, StringPropertyContinuation>(Actual, this);

        public ExceptionStringPropertyContinuation(string actual) : base(actual)
        {
        }
    }
}