using NExpect.Interfaces;

namespace NExpect.Implementations
{
    public class Not<T>: ExpectationContext<T>, INot<T>
    {
        public T Actual { get; }
        public IToAfterNot<T> To => Factory.Create<T, ToAfterNot<T>>(Actual, this);

        public Not(T actual)
        {
            Actual = actual;
            Negate();
        }
    }
}