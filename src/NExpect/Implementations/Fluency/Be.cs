using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Numerics;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global

namespace NExpect.Implementations.Fluency
{
    internal class Be<T> : 
        ExpectationContextWithLazyActual<T>, 
        IHasActual<T>,
        IBe<T>
    {
        public INotAfterBe<T> Not => ContinuationFactory.Create<T, NotAfterBe<T>>(ActualFetcher, this);

        public IEqualityContinuation<T> Equal =>
            ContinuationFactory.Create<T, EqualityContinuation<T>>(ActualFetcher, this);

        public IGreaterContinuation<T> Greater =>
            ContinuationFactory.Create<T, GreaterContinuation<T>>(
                ActualFetcher,
                this);

        public ILessContinuation<T> Less =>
            ContinuationFactory.Create<T, LessContinuation<T>>(
                ActualFetcher,
                this);

        public IA<T> A => ContinuationFactory.Create<T, A<T>>(ActualFetcher, this);
        public IAn<T> An => ContinuationFactory.Create<T, An<T>>(ActualFetcher, this);
        public INull<T> Null => ContinuationFactory.Create<T, Null<T>>(ActualFetcher, this);
        public IFor<T> For => ContinuationFactory.Create<T, For<T>>(ActualFetcher, this);
        
        public IDeep<T> Deep =>
            ContinuationFactory.Create<T, Deep<T>>(ActualFetcher, this);

        public IIntersection<T> Intersection =>
            ContinuationFactory.Create<T, Intersection<T>>(ActualFetcher, this);

        public void SetActual(T actual)
        {
            Actual = actual;
        }

        public Be(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}