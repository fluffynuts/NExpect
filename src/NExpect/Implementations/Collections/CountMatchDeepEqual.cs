using NExpect.Interfaces;

namespace NExpect.Implementations.Collections
{
    internal class CountMatchDeepEqual<T>
        : ExpectationContext<T>,
            ICountMatchDeepEqual<T>, ICountMatchEqual<T>
    {
        public ICanAddMatcher<T> Continuation { get; }
        public CountMatchMethods Method { get; }
        public int ExpectedCount { get; }

        public CountMatchDeepEqual(
            ICanAddMatcher<T> continuation,
            CountMatchMethods method,
            int compare)
        {
            Continuation = continuation;
            Method = method;
            ExpectedCount = compare;
        }
    }
}