using NExpect.Interfaces;
using NExpect.MatcherLogic;

// ReSharper disable InconsistentNaming
namespace NExpect.Implementations.Collections;

internal class CountMatchContinuation<T>
    : ExpectationContext<T>,
      IHasActual<T>,
      ICountMatchContinuation<T>
{
    public int ExpectedCount => _expectedCount;
    public CountMatchMethods Method => _method;

    protected readonly int _expectedCount;
    protected readonly CountMatchMethods _method;
    protected readonly ICanAddMatcher<T> _wrapped;

    public CountMatchContinuation(
        ICanAddMatcher<T> wrapped,
        CountMatchMethods method,
        int expectedCount
    )
    {
        ExpectationTracker.Forget(wrapped);
        _wrapped = wrapped;
        _method = method;
        _expectedCount = expectedCount;
        SetParent(wrapped as IExpectationContext<T>);
    }

    public ICountMatchEqual<T> Equal =>
        ForgetSelfAndReturn(
            new CountMatchEqual<T>(
                _wrapped,
                _method,
                _expectedCount
            )
        );

    public ICountMatchMatched<T> Matched =>
        ForgetSelfAndReturn(
            new CountMatchMatched<T>(
                _wrapped,
                _method,
                _expectedCount
            )
        );

    private TNext ForgetSelfAndReturn<TNext>(TNext next)
    {
        ExpectationTracker.Forget(this);
        return next;
    }

    public ICountMatchDeep<T> Deep =>
        CreateCountMatchDeep();

    public ICountMatchIntersection<T> Intersection =>
        CreateCountMatchIntersection();

    public ICountMatchOf<T> Of =>
        CreateCountMatchOf();

    private ICountMatchIntersection<T> CreateCountMatchIntersection()
    {
        ExpectationTracker.Forget(this);
        var result = new CountMatchIntersection<T>(
            _wrapped,
            _method,
            _expectedCount
        );
        result.SetParent(this);
        return result;
    }

    private CountMatchDeep<T> CreateCountMatchDeep()
    {
        ExpectationTracker.Forget(this);
        var result = new CountMatchDeep<T>(
            _wrapped,
            _method,
            _expectedCount
        );
        result.SetParent(this);
        return result;
    }

    private CountMatchOf<T> CreateCountMatchOf()
    {
        ExpectationTracker.Forget(this);
        var result = new CountMatchOf<T>(
            _wrapped,
            _method,
            _expectedCount
        );
        result.SetParent(this);
        return result;
    }

    public T Actual => _wrapped.GetActual();
}