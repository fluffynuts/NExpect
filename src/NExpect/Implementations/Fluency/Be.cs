using NExpect.Implementations.Collections;
using NExpect.Implementations.Numerics;
using NExpect.Interfaces;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global

namespace NExpect.Implementations.Fluency
{
    internal class Be<T> : 
        ExpectationContext<T>, 
        IHasActual<T>,
        IBe<T>
    {
        public T Actual { get; }

        public INotAfterBe<T> Not => ContinuationFactory.Create<T, NotAfterBe<T>>(Actual, this);

        public IEqualityContinuation<T> Equal =>
            ContinuationFactory.Create<T, EqualityContinuation<T>>(Actual, this);

        public IGreaterContinuation<T> Greater =>
            ContinuationFactory.Create<T, GreaterContinuation<T>>(
                Actual,
                this);

        public ILessContinuation<T> Less =>
            ContinuationFactory.Create<T, LessContinuation<T>>(
                Actual,
                this);

        public IA<T> A => ContinuationFactory.Create<T, A<T>>(Actual, this);
        public IAn<T> An => ContinuationFactory.Create<T, An<T>>(Actual, this);
        public INull<T> Null => ContinuationFactory.Create<T, Null<T>>(Actual, this);
        public IFor<T> For => ContinuationFactory.Create<T, For<T>>(Actual, this);
        
        public IDeep<T> Deep =>
            ContinuationFactory.Create<T, Deep<T>>(Actual, this);

        public IIntersection<T> Intersection =>
            ContinuationFactory.Create<T, Intersection<T>>(Actual, this);


        public Be(T actual)
        {
            Actual = actual;
        }
    }
}