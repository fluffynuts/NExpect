using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
namespace NExpect.Implementations.Fluency
{
    internal class Have<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        IHave<T>
    {
        public IA<T> A => ContinuationFactory.Create<T, A<T>>(ActualFetcher, this);
        public IAn<T> An => ContinuationFactory.Create<T, An<T>>(ActualFetcher, this);
        public IBeen<T> Been => ContinuationFactory.Create<T, Been<T>>(ActualFetcher, this);

        public Have(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}