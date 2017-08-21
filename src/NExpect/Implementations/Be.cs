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
        public INull<T> Null => Factory.Create<T, Null<T>>(Actual, this);

        public Be(T actual)
        {
            Actual = actual;
        }
    }

    internal class Null<T> :
        ExpectationContext<T>,
        INull<T>
    {
        T Actual { get; }

        public Null(T actual)
        {
            Actual = actual;
        }

        public INullOr<T> Or => Factory.Create<T, NullOr<T>>(Actual, this);
    }

    internal class NullOr<T> :
        ExpectationContext<T>,
        INullOr<T>
    {
        public T Actual { get; }

        public NullOr(T actual)
        {
            Actual = actual;
        }
    }
}