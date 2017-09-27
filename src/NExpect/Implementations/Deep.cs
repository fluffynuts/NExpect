using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class Deep<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IDeep<T>
    {
        public T Actual { get; }

        public Deep(T actual)
        {
            Actual = actual;
        }
    }
}