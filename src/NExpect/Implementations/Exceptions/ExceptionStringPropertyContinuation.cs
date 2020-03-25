using System;
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
            => ContinuationFactory.Create<string, StringPropertyNot>(ActualFetcher, this);

        public IStringPropertyContinuation And 
            => ContinuationFactory.Create<string, StringPropertyAnd>(ActualFetcher, this);

        public IStringPropertyStartingContinuation Starting 
            => ContinuationFactory.Create<string, StringPropertyContinuation>(ActualFetcher, this);
        
        public IStringPropertyEndingContinuation Ending 
            => ContinuationFactory.Create<string, StringPropertyContinuation>(ActualFetcher, this);

        public ExceptionStringPropertyContinuation(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}