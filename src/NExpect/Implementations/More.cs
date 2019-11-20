using NExpect.Implementations.Collections;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class More<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IMore<T>
    {
        public T Actual { get; }

        public IAnd<T> And =>
            ContinuationFactory.Create<T, And<T>>(Actual, this);

        public More(T actual)
        {
            Actual = actual;
        }
    }
}