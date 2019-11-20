using NExpect.Implementations.Collections;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class LessThanAnd<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        ILessThanAnd<T>
    {
        public T Actual { get; }

        public IGreaterContinuation<T> Greater =>
            ContinuationFactory.Create<T, GreaterContinuation<T>>(Actual, this);

        public LessThanAnd(T actual)
        {
            Actual = actual;
        }
    }
}