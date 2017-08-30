using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class Null<T> :
        ExpectationContext<T>,
        INull<T>
    {
        T Actual { get; }

        public Null(T actual)
        {
            Actual = actual;
        }

        public INullOr<T> Or => Factory.Create<T, NullOr<T>>(Actual, this);
    }
}