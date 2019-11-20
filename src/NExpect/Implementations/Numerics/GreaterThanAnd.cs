using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Numerics
{
    internal class GreaterThanAnd<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IGreaterThanAnd<T>
    {
        public T Actual { get; }

        public ILessContinuation<T> Less =>
            ContinuationFactory.Create<T, LessContinuation<T>>(Actual, this);

        public GreaterThanAnd(T actual)
        {
            Actual = actual;
        }
    }
}