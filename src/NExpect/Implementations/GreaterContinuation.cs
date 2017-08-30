using NExpect.Interfaces;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class GreaterContinuation<T> :
        ExpectationContext<T>,
        IGreaterContinuation<T>
    {
        public T Actual { get; }

        public GreaterContinuation(T actual)
        {
            Actual = actual;
        }
    }
}