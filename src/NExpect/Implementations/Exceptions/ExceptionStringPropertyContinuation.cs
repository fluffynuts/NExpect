using NExpect.Implementations.Collections;
using NExpect.Implementations.Fluency;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Exceptions
{
    internal class ExceptionStringPropertyContinuation : 
        Be<string>, 
        IStringPropertyContinuation
    {
        public new IStringPropertyNot Not 
            => ContinuationFactory.Create<string, StringPropertyNot>(Actual, this);

        public IStringPropertyContinuation And 
            => ContinuationFactory.Create<string, StringPropertyAnd>(Actual, this);

        public IStringPropertyStartingContinuation Starting 
            => ContinuationFactory.Create<string, StringPropertyContinuation>(Actual, this);
        
        public IStringPropertyEndingContinuation Ending 
            => ContinuationFactory.Create<string, StringPropertyContinuation>(Actual, this);

        public ExceptionStringPropertyContinuation(string actual) : base(actual)
        {
        }
    }
}