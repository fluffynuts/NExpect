using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class GreaterThan<T>
        : ExpectationContext<T>,
            IGreaterThan<T>
    {
        public T Actual { get; }
        public IGreaterThanAnd<T> And =>
            Factory.Create<T, GreaterThanAnd<T>>(Actual, this);

        public GreaterThan(T actual)
        {
            Actual = actual;
        }
    }
}