using NExpect.Interfaces;

namespace NExpect.Implementations.Collections;

internal class CountMatchMatched<T>
    : ICountMatchMatched<T>
{
    public ICanAddMatcher<T> Continuation { get; }
    public CountMatchMethods Method { get; }
    public int Compare { get; }

    public CountMatchMatched(
        ICanAddMatcher<T> continuation,
        CountMatchMethods method,
        int compare)
    {
        Continuation = continuation;
        Method = method;
        Compare = compare;
    }
}