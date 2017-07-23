using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CountMatchEqual<T>
        : ICountMatchEqual<T>
    {
        public ICanAddMatcher<T> Continuation { get; }
        public CountMatchMethods Method { get; }
        public int Compare { get; }

        public CountMatchEqual(
            ICanAddMatcher<T> continuation,
            CountMatchMethods method,
            int compare)
        {
            Continuation = continuation;
            Method = method;
            Compare = compare;
        }
    }
}