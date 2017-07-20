using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CountMatchMatched<T>
        : ICountMatchMatched<T>
    {
        public IContinuation<T> Continuation { get; }
        public CountMatchMethods Method { get; }
        public int Compare { get; }

        public CountMatchMatched(
            IContinuation<T> continuation,
            CountMatchMethods method,
            int compare)
        {
            Continuation = continuation;
            Method = method;
            Compare = compare;
        }
    }
}