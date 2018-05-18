using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class GreaterContinuation<T>
        : ExpectationContext<T>,
            IHasActual<T>,
            IGreaterContinuation<T>
    {
        public T Actual { get; }

        public GreaterContinuation(T actual)
        {
            Actual = actual;
        }

        public IGreaterThan<T> Than
            => Factory.Create<T, GreaterThan<T>>(Actual, this);
    }

    internal class GreaterThan<T>
        : ExpectationContext<T>,
            IGreaterThan<T>
    {
        public T Actual { get; }
        public IGreaterThanOr<T> Or 
        => Factory.Create<T, GreaterThanOr<T>>(Actual, this);

        public GreaterThan(T actual)
        {
            Actual = actual;
        }
    }
    
    internal class GreaterThanOr<T>
        : ExpectationContext<T>,
            IGreaterThanOr<T>
    {
        public IGreaterThanOrEqual<T> Equal 
            => Factory.Create<T, GreaterThanOrEqual<T>>(Actual, this);
        public T Actual { get; }

        public GreaterThanOr(T actual)
        {
            Actual = actual;
        }
    }

    internal class GreaterThanOrEqual<T> : 
        ExpectationContext<T>,
        IHasActual<T>,
        IGreaterThanOrEqual<T>
    {
        public T Actual { get; }

        public GreaterThanOrEqual(T actual)
        {
            Actual = actual;
        }
    }
}