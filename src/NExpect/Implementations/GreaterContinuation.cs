using NExpect.Interfaces;

namespace NExpect.Implementations
{
    public class GreaterContinuation<T> :
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