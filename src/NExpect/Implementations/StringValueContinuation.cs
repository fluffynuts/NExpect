using NExpect.Interfaces;

namespace NExpect.Implementations
{
    public class StringValueContinuation<T>
        : ExpectationContext<string>, IExceptionMessageContinuation
    {
        public StringValueContinuation(string value)
        {
            Actual = value;
        }
        public string Actual { get; }
    }
}