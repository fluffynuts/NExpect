using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class GreaterContinuation<T> :
        ExpectationContext<T>,
        IGreaterContinuation<T>
    {
        public T Actual { get; }

        public GreaterContinuation(T actual)
        {
            Actual = actual;
        }
    }
}