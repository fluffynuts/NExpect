using NExpect.Interfaces;

namespace NExpect.Implementations
{
    public class Contain<T> : ExpectationContext<T>, IContain<T>
    {
        public T Actual { get; }

        public IContainAt<T> At =>
            Factory.Create<T, ContainAt<T>>(Actual, this);

        public Contain(T actual)
        {
            Actual = actual;
        }
    }

    public class ContainAt<T>
        : ExpectationContext<T>, IContainAt<T>
    {
        T Actual { get; }
        public ContainAt(T actual)
        {
            Actual = actual;
        }
    }
}