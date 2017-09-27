using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class EqualityContinuation<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IEqualityContinuation<T>
    {
        public T Actual { get; }

        public EqualityContinuation(T actual)
        {
            Actual = actual;
        }
    }
}