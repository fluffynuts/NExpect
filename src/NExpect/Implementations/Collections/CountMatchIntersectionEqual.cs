using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations.Collections;

internal class CountMatchIntersectionEqual<T>
    : ExpectationContext<T>,
      ICountMatchIntersectionEqual<T>, ICountMatchEqual<T>
{
    public ICanAddMatcher<T> Continuation { get; }
    public T Actual => Continuation.GetActual();
    public CountMatchMethods Method { get; }
    public int ExpectedCount { get; }

    public CountMatchIntersectionEqual(
        ICanAddMatcher<T> continuation,
        CountMatchMethods method,
        int compare)
    {
        Continuation = continuation;
        Method = method;
        ExpectedCount = compare;
    }
}