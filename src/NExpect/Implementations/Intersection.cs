using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class Intersection<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IIntersection<T>
    {
        public T Actual { get; }

        public Intersection(T actual)
        {
            Actual = actual;
        }
    }
}