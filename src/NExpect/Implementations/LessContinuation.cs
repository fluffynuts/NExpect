using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class LessContinuation<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        ILessContinuation<T>
    {
        public T Actual { get; }

        public LessContinuation(T actual)
        {
            Actual = actual;
        }
    }
}