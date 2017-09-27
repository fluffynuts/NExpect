using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CountMatchIntersection<T>: 
        ExpectationContext<T>,
        ICountMatchIntersection<T>, 
        ICountMatchEqual<T>
    {
        public ICanAddMatcher<T> Continuation { get; }

        public CountMatchMethods Method { get; }
        public int ExpectedCount { get; }

        public CountMatchIntersection(
            ICanAddMatcher<T> continuation,
            CountMatchMethods method,
            int compare)
        {
            Continuation = continuation;
            Method = method;
            ExpectedCount = compare;
        }

        public ICountMatchIntersectionEqual<T> Equal
            => CreateCountMatchIntersectionEqual();

        private CountMatchIntersectionEqual<T> CreateCountMatchIntersectionEqual()
        {
            var result = new CountMatchIntersectionEqual<T>(
                Continuation,
                Method,
                ExpectedCount
            );
            result.SetParent(this);
            return result;
        }
    }
}