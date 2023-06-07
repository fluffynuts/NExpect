using System;
using NExpect.Helpers;
using NExpect.Interfaces;

namespace NExpect.Implementations;

/// <summary>
/// Provides an expectation context to inherit from where
/// the context has a lazy retriever for the Actual value
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ExpectationContextWithLazyActual<T>
    : ExpectationContext<T>, IHasActualFetcher<T>
{
    /// <summary>
    /// 
    /// </summary>
    public virtual bool? IsNegated
    {
        get => _isNegated;
        set 
        {
            _isNegated = value;
            var parentPropInfo = Parent?.GetType()?.GetProperty(nameof(IsNegated));
            parentPropInfo?.SetValue(Parent, value);
        }
    }

    private bool? _isNegated;

    /// <summary>
    /// Shortcut to call into ContinuationFactory.Create
    /// </summary>
    /// <typeparam name="TContinuation"></typeparam>
    /// <returns></returns>
    protected TContinuation Next<TContinuation>()
        where TContinuation : IExpectationContext<T>
    {
        ExpectationTracker.Forget(this);
        return ContinuationFactory.Create<T, TContinuation>(ActualFetcher, this);
    }

    /// <summary>
    /// Shortcut to call into ContinuationFactory.Create and negate
    /// </summary>
    /// <typeparam name="TContinuation"></typeparam>
    /// <returns></returns>
    protected TContinuation NextNegated<TContinuation>()
        where TContinuation : IExpectationContext<T>
    {
        ExpectationTracker.Forget(this);
        return Next<TContinuation>(Negate);
    }

    private void Negate<TContinuation>(TContinuation obj)
        where TContinuation : IExpectationContext<T>
    {
        var propInfo = obj.GetType().GetProperty(nameof(ExpectationBase.IsNegated));
        var propType = propInfo?.PropertyType;
        if (propType == typeof(bool) || propType == typeof(bool?))
        {
            propInfo.SetValue(obj, true);
        }
    }

    /// <summary>
    /// Shortcut to call into ContinuationFactory.Create
    /// and run some code on the result after it's been set up
    /// </summary>
    /// <param name="afterConstruction"></param>
    /// <typeparam name="TContinuation"></typeparam>
    /// <returns></returns>
    protected TContinuation Next<TContinuation>(
        Action<TContinuation> afterConstruction
    ) where TContinuation : IExpectationContext<T>
    {
        return ContinuationFactory.Create<T, TContinuation>(
            ActualFetcher, this, afterConstruction
        );
    }

    /// <summary>
    /// Retrieves the value from the actual fetcher
    /// (memoized, so this only ever happens once)
    /// </summary>
    public T Actual
    {
        get =>
            _fetchedActual
                ? _actual
                : _actual = _actualFetcher();
        set
        {
            _actual = value;
            _fetchedActual = true;
        }
    }

    private T _actual;
    private bool _fetchedActual = false;
    private readonly Func<T> _actualFetcher;

    /// <summary>
    /// The func to use to fetch the value on-demand
    /// </summary>
    public Func<T> ActualFetcher => _actualFetcher;

    /// <summary>
    /// Constructs a new Expectation context with a lazy fetcher
    /// </summary>
    /// <param name="actualFetcher"></param>
    protected ExpectationContextWithLazyActual(
        Func<T> actualFetcher
    ) : this(actualFetcher, false)
    {
    }

    /// <summary>
    /// Constructs a new Expectation context with a lazy fetcher
    /// </summary>
    /// <param name="actualFetcher"></param>
    /// <param name="negate"></param>
    protected ExpectationContextWithLazyActual(
        Func<T> actualFetcher,
        bool negate
    )
    {
        if (negate)
        {
            Negate();
        }

        _actualFetcher = FuncFactory.Memoize(actualFetcher);
    }
}