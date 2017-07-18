using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CountMatchEquals<T>
        : ICountMatchEquals<T>
    {
        public IContinuation<T> Continuation { get; }
        public CountMatchMethods Method { get; }
        public int Compare { get; }

        public CountMatchEquals(
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