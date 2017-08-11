using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class Be<T> : ExpectationContext<T>, IBe<T>
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public T Actual { get; }
        public INotAfterBe<T> Not => Factory.Create<T, NotAfterBe<T>>(Actual, this);

        public IEqualityContinuation<T> Equal =>
            Factory.Create<T, EqualityContinuation<T>>(Actual, this);

        public IGreaterContinuation<T> Greater =>
            Factory.Create<T, GreaterContinuation<T>>(
                Actual,
                this);

        public ILessContinuation<T> Less =>
            Factory.Create<T, LessContinuation<T>>(
                Actual,
                this);

        public IA<T> A => Factory.Create<T, A<T>>(Actual, this);
        public IAn<T> An => Factory.Create<T, An<T>>(Actual, this);

        public Be(T actual)
        {
            Actual = actual;
        }
    }
}