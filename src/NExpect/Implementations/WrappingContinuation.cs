using System;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class WrappingContinuation<TFrom, To> : 
        ExpectationContext<To>, IContinuation<To>
    {
        public To Actual => _unwrap(_wrapped);

        private readonly IHasActual<TFrom> _wrapped;
        private readonly Func<IHasActual<TFrom>, To> _unwrap;

        internal WrappingContinuation(
            IHasActual<TFrom> toWrap, 
            Func<IHasActual<TFrom>, To> unwrap
        )
        {
            _wrapped = toWrap;
            _unwrap = unwrap;
        }
    }
}