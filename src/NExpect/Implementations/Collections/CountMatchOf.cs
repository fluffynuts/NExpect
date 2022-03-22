using NExpect.Interfaces;

namespace NExpect.Implementations.Collections;

internal class CountMatchOf<T>
    : ExpectationContext<T>,
      ICountMatchOf<T>
{
    public ICanAddMatcher<T> Continuation { get; }
    public CountMatchMethods Method { get; }
    public int Compare { get; }

    public CountMatchOf(
        ICanAddMatcher<T> continuation,
        CountMatchMethods method,
        int compare
    )
    {
        Continuation = continuation;
        Method = method;
        Compare = compare;
    }
}