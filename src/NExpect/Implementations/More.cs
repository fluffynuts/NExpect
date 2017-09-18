using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class More<T>
        : ExpectationContext<T>,
            IMore<T>
    {
        public T Actual { get; }

        public IAnd<T> And =>
            Factory.Create<T, And<T>>(Actual, this);

        public More(T actual)
        {
            Actual = actual;
        }
    }
}