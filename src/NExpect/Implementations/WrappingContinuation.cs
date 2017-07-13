using System;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class WrappingContinuation<TFrom, To> : 
        ExpectationContext<To>, IContinuation<To>
    {
        public To Actual => _unwrap(_wrapped);

        private readonly IContinuation<TFrom> _wrapped;
        private readonly Func<IContinuation<TFrom>, To> _unwrap;

        internal WrappingContinuation(
            IContinuation<TFrom> toWrap, 
            Func<IContinuation<TFrom>, To> unwrap
        )
        {
            _wrapped = toWrap;
            _unwrap = unwrap;
        }
    }
}