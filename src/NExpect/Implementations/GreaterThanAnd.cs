using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class GreaterThanAnd<T>: 
        ExpectationContext<T>,
        IGreaterThanAnd<T>
    {
        public T Actual { get; }
        public ILessContinuation<T> Less =>
            Factory.Create<T, LessContinuation<T>>(Actual, this);
        public GreaterThanAnd(T actual)
        {
            Actual = actual;
        }
    }
}