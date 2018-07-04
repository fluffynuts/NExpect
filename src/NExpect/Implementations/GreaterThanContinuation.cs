using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class GreaterThanContinuation<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IGreaterThanContinuation<T>
    {
        public T Actual { get; }

        public IGreaterThanAnd<T> And =>
            ContinuationFactory.Create<T, GreaterThanAnd<T>>(Actual, this);

        public GreaterThanContinuation(T actual)
        {
            Actual = actual;
        }
    }
}