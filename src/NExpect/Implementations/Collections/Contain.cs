using System;
using System.Collections.Generic;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Collections
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Contain<T> :
        ExpectationContextWithLazyActual<T>, 
        IHasActual<T>,
        IContain<T>
    {
        public IContainAt<T> At =>
            ContinuationFactory.Create<T, ContainAt<T>>(ActualFetcher, this);
        
        public ICountMatchContinuation<T> No
            => new CountMatchContinuation<T>(
                this,
                CountMatchMethods.Only,
                0);
        
        public ICountMatchContinuation<T> None
            => new CountMatchContinuation<T>(
                this,
                CountMatchMethods.Exactly,
                0);
        
        public ICountMatchContinuation<T> All
            => new CountMatchContinuation<T>(
                this,
                CountMatchMethods.All,
                0);
        
        public ICountMatchContinuation<T> Any
            => new CountMatchContinuation<T>(
                this,
                CountMatchMethods.Any,
                0);

        public Contain(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}