using NExpect.Interfaces;

namespace NExpect.Implementations
{
    public class Contain<T> : ExpectationContext<T>, IContain<T>
    {
        public T Actual { get; }

        public IContain<T> At => this;

        public Contain(T actual)
        {
            Actual = actual;
        }
    }
}