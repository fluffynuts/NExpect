using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class Contain<T> : ExpectationContext<T>, IContain<T>
    {
        public T Actual { get; }

        public IContainAt<T> At =>
            Factory.Create<T, ContainAt<T>>(Actual, this);

        public Contain(T actual)
        {
            Actual = actual;
        }
    }
}