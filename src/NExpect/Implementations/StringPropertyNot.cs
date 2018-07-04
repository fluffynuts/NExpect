using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class StringPropertyNot
        : ExpectationContext<string>, IStringPropertyNot
    {
        public string Actual { get; set; }

        public IToAfterNot<string> To 
            => ContinuationFactory.Create<string, ToAfterNot<string>>(Actual, this);
        
        public IStringPropertyEndingContinuation Ending
            => ContinuationFactory.Create<string, StringPropertyContinuation>(Actual, this);

        public IStringPropertyEndingContinuation Starting 
            => ContinuationFactory.Create<string, StringPropertyContinuation>(Actual, this);

        public StringPropertyNot(string actual)
        {
            Actual = actual;
            // ReSharper disable once VirtualMemberCallInConstructor
            Negate();
        }
    }
}