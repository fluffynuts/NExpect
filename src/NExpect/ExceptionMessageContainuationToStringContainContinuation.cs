using NExpect.Implementations;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect
{
    internal class ExceptionMessageContainuationToStringContainContinuation
        : ExpectationContext<string>, IStringContainContinuation
    {
        public string Actual { get; set; }

        public ExceptionMessageContainuationToStringContainContinuation(string actual)
        {
            Actual = actual;
        }

        public IStringAnd And =>
            Factory.Create<string, StringAnd>(Actual, this);

        public IStringIn In =>
            Factory.Create<string, StringIn>(Actual, this);
    }
}