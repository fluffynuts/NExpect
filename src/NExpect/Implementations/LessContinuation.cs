using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class LessContinuation<T> :
        ExpectationContext<T>,
        ILessContinuation<T>
    {
        public T Actual { get; }

        public LessContinuation(T actual)
        {
            Actual = actual;
        }
    }
}