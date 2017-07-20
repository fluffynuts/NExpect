using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class StringValueContinuation<T>
        : ExpectationContext<string>, 
        IExceptionMessageContinuation
    {
        public StringValueContinuation(string value)
        {
            Actual = value;
        }

        public string Actual { get; }

        public IEqualityContinuation<string> Equal =>
            Factory.Create<string, EqualityContinuation<string>>(Actual, this);

        public INot<string> Not =>
            Factory.Create<string, Not<string>>(Actual, this);
    }
}