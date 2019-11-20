using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Numerics
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

        public ILessThan<T> Than =>
            ContinuationFactory.Create<T, LessThan<T>>(Actual, this);
    }

    internal class LessThan<T>
    : ExpectationContext<T>,
        ILessThan<T>
    {
        public T Actual { get; }
        public ILessThanOr<T> Or 
            => ContinuationFactory.Create<T, LessThanOr<T>>(Actual, this);

        public LessThan(T actual)
        {
            Actual = actual;
        }
    }

    internal class LessThanOr<T>
    : ExpectationContext<T>,
        ILessThanOr<T>
    {
        public T Actual { get; }
        public ILessThanOrEqual<T> Equal 
            => ContinuationFactory.Create<T, LessThanOrEqual<T>>(Actual, this);

        public LessThanOr(T actual)
        {
            Actual = actual;
        }
    }

    internal class LessThanOrEqual<T>
    : ExpectationContext<T>,
        ILessThanOrEqual<T>
    {
        public T Actual { get; }

        public LessThanOrEqual(T actual)
        {
            Actual = actual;
        }
    }
}