using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations.Collections;

internal class CountMatchDeep<T>: 
    ExpectationContext<T>,
    ICountMatchDeep<T>, 
    ICountMatchEqual<T>
{
    public ICanAddMatcher<T> Continuation { get; }
    public T Actual => Continuation.GetActual();

    public CountMatchMethods Method { get; }
    public int ExpectedCount { get; }

    public CountMatchDeep(
        ICanAddMatcher<T> continuation,
        CountMatchMethods method,
        int compare)
    {
        Continuation = continuation;
        Method = method;
        ExpectedCount = compare;
    }

    public ICountMatchDeepEqual<T> Equal
        => CreateCountMatchDeepEqual();

    private CountMatchDeepEqual<T> CreateCountMatchDeepEqual()
    {
        var result = new CountMatchDeepEqual<T>(
            Continuation,
            Method,
            ExpectedCount
        );
        result.SetParent(this);
        return result;
    }
}