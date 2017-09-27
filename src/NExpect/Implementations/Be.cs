using NExpect.Interfaces;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global

namespace NExpect.Implementations
{
    internal class Be<T> : 
        ExpectationContext<T>, 
        IHasActual<T>,
        IBe<T>
    {
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
        public IFor<T> For => Factory.Create<T, For<T>>(Actual, this);

        public Be(T actual)
        {
            Actual = actual;
        }
    }

    internal class For<T>:
        ExpectationContext<T>,
        IFor<T>
    {
        public T Actual { get; }
        public For(T actual)
        {
            Actual = actual;
        }
    }
}