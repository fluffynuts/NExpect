using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency
{
    internal class NullOr<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        INullOr<T>
    {
        public T Actual { get; }

        public NullOr(T actual)
        {
            Actual = actual;
        }
    }
}