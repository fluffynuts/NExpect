using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringPropertyContinuation
        : ExpectationContext<string>, 
            IStringPropertyContinuation,
            IStringPropertyStartingContinuation,
            IStringPropertyEndingContinuation
    {
        public string Actual { get; set; }

        public IStringPropertyContinuation And =>
            ContinuationFactory.Create<string, StringPropertyAnd>(Actual, this);

        public IEqualityContinuation<string> Equal 
            => ContinuationFactory.Create<string, EqualityContinuation<string>>(Actual, this);

        public IStringIn In =>
            ContinuationFactory.Create<string, StringIn>(Actual, this);

        public IStringPropertyNot Not 
            => ContinuationFactory.Create<string, StringPropertyNot>(Actual, this);
        
        public IStringPropertyEndingContinuation Ending
            => ContinuationFactory.Create<string, StringPropertyContinuation>(Actual, this);

        public IStringPropertyStartingContinuation Starting 
            => ContinuationFactory.Create<string, StringPropertyContinuation>(Actual, this);

        public StringPropertyContinuation(string actual)
        {
            Actual = actual;
        }
    }
}