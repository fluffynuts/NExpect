using NExpect.Interfaces;
// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class ExceptionMessageContainuationToStringContainContinuation
        : ExpectationContext<string>, IStringPropertyContinuation
    {
        public string Actual { get; set; }

        public ExceptionMessageContainuationToStringContainContinuation(string actual)
        {
            Actual = actual;
        }

        public IStringPropertyContinuation And =>
            Factory.Create<string, StringPropertyAnd>(Actual, this);

        public IEqualityContinuation<string> Equal 
            => Factory.Create<string, EqualityContinuation<string>>(Actual, this);

        public IStringIn In =>
            Factory.Create<string, StringIn>(Actual, this);

        public IStringPropertyNot Not 
            => Factory.Create<string, StringPropertyNot>(Actual, this);
    }
}