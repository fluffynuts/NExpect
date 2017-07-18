using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class EqualityContinuation<T> :
        ExpectationContext<T>,
        IEqualityContinuation<T>
    {
        public T Actual { get; }
        public EqualityContinuation(T actual)
        {
            Actual = actual;
        }
    }
}