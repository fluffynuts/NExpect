using NExpect.Interfaces;

namespace NExpect.Implementations
{
    public class Be<T> : ExpectationContext<T>, IBe<T>
    {
        public T Actual { get; }
        public INotAfterBe<T> Not => Factory.Create<T, NotAfterBe<T>>(Actual, this);

        public IEqualityContinuation<T> Equal =>
            Factory.Create<T, EqualityContinuation<T>>(Actual, this);

        public IGreaterContinuation<T> Greater =>
            Factory.Create<T, GreaterContinuation<T>>(
                Actual, this);

        public ILessContinuation<T> Less =>
            Factory.Create<T, LessContinuation<T>>(
                Actual, this);

        public Be(T actual)
        {
            Actual = actual;
        }
    }

    public class GreaterContinuation<T> :
        ExpectationContext<T>, IGreaterContinuation<T>
    {
        public T Actual { get; }

        public GreaterContinuation(T actual)
        {
            Actual = actual;
        }
    }

    public class LessContinuation<T> :
        ExpectationContext<T>, ILessContinuation<T>
    {
        public T Actual { get; }

        public LessContinuation(T actual)
        {
            Actual = actual;
        }
    }
}