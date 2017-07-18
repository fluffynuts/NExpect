using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    internal class WrappingContinuation<TFrom, To> : 
        ExpectationBase<To>, 
        IContinuation<To>,
        IExpectationContext<To>
    {
        public To Actual => _unwrap(_wrapped);
        public void RunMatcher(Func<To, IMatcherResult> matcher)
        {
            RunMatcher(Actual, IsNegated, matcher);
        }

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

        public IExpectationContext<To> Parent { get; set; }
    }
}