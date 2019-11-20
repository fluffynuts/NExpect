using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency
{
    internal class Null<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        INull<T>
    {
        public T Actual { get; }

        public Null(T actual)
        {
            Actual = actual;
        }

        public INullOr<T> Or => ContinuationFactory.Create<T, NullOr<T>>(Actual, this);
    }
}