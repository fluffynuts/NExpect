using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class WrappingContinuation<TFrom, TTo> : 
        ExpectationBase<TTo>, 
        ICanAddMatcher<TTo>,
        IExpectationContext<TTo>
    {
        public TTo Actual => _unwrap(_wrapped);
        public void RunMatcher(Func<TTo, IMatcherResult> matcher)
        {
            RunMatcher(Actual, IsNegated, matcher);
        }

        private readonly IHasActual<TFrom> _wrapped;
        private readonly Func<IHasActual<TFrom>, TTo> _unwrap;

        internal WrappingContinuation(
            IHasActual<TFrom> toWrap, 
            Func<IHasActual<TFrom>, TTo> unwrap
        )
        {
            _wrapped = toWrap;
            _unwrap = unwrap;
        }

        public IExpectationContext<TTo> Parent { get; set; }
    }
}