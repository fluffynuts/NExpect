using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations;

internal interface IWrappingContinuation
{
    object Wrapped { get; }
}

internal class WrappingContinuation<TFrom, TTo> : 
    ExpectationBase<TTo>, 
    ICanAddMatcher<TTo>,
    IExpectationContext<TTo>,
    IWrappingContinuation
{
    public IExpectationContext<TTo> TypedParent { get; set; }
    public IExpectationContext Parent => TypedParent;

    public TTo Actual => _unwrap(_wrapped);

    public object Wrapped => _wrapped;
    private readonly IHasActual<TFrom> _wrapped;
    private readonly Func<IHasActual<TFrom>, TTo> _unwrap;

    public IMatcherResult RunMatcher(Func<TTo, IMatcherResult> matcher)
    {
        return RunMatcher(Actual, IsNegated, matcher, false);
    }


    internal WrappingContinuation(
        IHasActual<TFrom> toWrap, 
        Func<IHasActual<TFrom>, TTo> unwrap
    )
    {
        _wrapped = toWrap;
        _unwrap = unwrap;
    }
}