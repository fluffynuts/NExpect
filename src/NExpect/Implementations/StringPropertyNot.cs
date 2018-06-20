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
            => Factory.Create<string, ToAfterNot<string>>(Actual, this);
        
        public IStringPropertyEndingContinuation Ending
            => Factory.Create<string, StringPropertyContinuation>(Actual, this);

        public IStringPropertyEndingContinuation Starting 
            => Factory.Create<string, StringPropertyContinuation>(Actual, this);

        public StringPropertyNot(string actual)
        {
            Actual = actual;
            // ReSharper disable once VirtualMemberCallInConstructor
            Negate();
        }
    }
}