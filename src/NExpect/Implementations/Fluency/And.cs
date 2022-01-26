using System;
using NExpect.Interfaces;

// ReSharper disable MemberCanBeProtected.Global

namespace NExpect.Implementations.Fluency
{
    internal class And<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        IAnd<T>
    {
        public IA<T> A => Next<A<T>>();
        public IAn<T> An => Next<An<T>>();
        public IHave<T> Have => Next<Have<T>>();
        public IPropertyNot<T> Not => Next<Not<T>>();
        public ITo<T> To => Next<To<T>>();
        public IIs<T> Is => Next<Is<T>>();
        public IHas<T> Has => Next<Has<T>>();
        public IIn<T> In => Next<In<T>>();
        public IOn<T> On => Next<On<T>>();
        public IWith<T> With => Next<With<T>>();
        public IWithout<T> Without => Next<Without<T>>();

        public And(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}