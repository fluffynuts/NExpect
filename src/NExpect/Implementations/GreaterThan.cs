using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class GreaterThan<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IGreaterThan<T>
    {
        public T Actual { get; }

        public IGreaterThanAnd<T> And =>
            Factory.Create<T, GreaterThanAnd<T>>(Actual, this);

        public GreaterThan(T actual)
        {
            Actual = actual;
        }
    }
}