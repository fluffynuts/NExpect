using NExpect.Implementations;
using NExpect.Interfaces;

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
    }
}