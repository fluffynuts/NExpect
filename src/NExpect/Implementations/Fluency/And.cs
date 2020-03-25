using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable MemberCanBeProtected.Global

namespace NExpect.Implementations.Fluency
{
    internal class And<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        IAnd<T>
    {
        public IA<T> A => ContinuationFactory.Create<T, A<T>>(ActualFetcher, this);
        public IAn<T> An => ContinuationFactory.Create<T, An<T>>(ActualFetcher, this);
        public IHave<T> Have => ContinuationFactory.Create<T, Have<T>>(ActualFetcher, this);
        public IPropertyNot<T> Not => ContinuationFactory.Create<T, Not<T>>(ActualFetcher, this);
        public ITo<T> To => ContinuationFactory.Create<T, To<T>>(ActualFetcher, this);

        public And(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}