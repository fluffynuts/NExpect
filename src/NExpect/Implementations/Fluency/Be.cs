using System;
using NExpect.Implementations.Numerics;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global

namespace NExpect.Implementations.Fluency
{
    internal class Be<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IBe<T>
    {
        public INotAfterBe<T> Not => Next<NotAfterBe<T>>();
        public IEqualityContinuation<T> Equal => Next<EqualityContinuation<T>>();
        public IGreaterContinuation<T> Greater => Next<Greater<T>>();
        public ILessContinuation<T> Less => Next<LessContinuation<T>>();
        public IA<T> A => Next<A<T>>();
        public IAn<T> An => Next<An<T>>();
        public INull<T> Null => Next<Null<T>>();
        public IFor<T> For => Next<For<T>>();
        public IDeep<T> Deep => Next<Deep<T>>();
        public IIntersection<T> Intersection => Next<Intersection<T>>();
        public IOn<T> On => Next<On<T>>();
        public IIn<T> In => Next<In<T>>();

        public void SetActual(T actual)
        {
            Actual = actual;
        }

        public Be(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}