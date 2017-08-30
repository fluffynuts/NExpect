using NExpect.Interfaces;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class NullOr<T> :
        ExpectationContext<T>,
        INullOr<T>
    {
        public T Actual { get; }

        public NullOr(T actual)
        {
            Actual = actual;
        }
    }
}