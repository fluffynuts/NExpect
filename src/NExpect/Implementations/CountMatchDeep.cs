using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CountMatchDeep<T>
        : ExpectationContext<T>,
        ICountMatchDeep<T>, ICountMatchEqual<T>
    {
        public ICanAddMatcher<T> Continuation { get; }

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
    internal class CountMatchIntersection<T>
        : ExpectationContext<T>,
        ICountMatchIntersection<T>, ICountMatchEqual<T>
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
    
    internal class CountMatchIntersectionEqual<T>
        : ExpectationContext<T>,
        ICountMatchIntersectionEqual<T>, ICountMatchEqual<T>
    {
        public ICanAddMatcher<T> Continuation { get; }
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
}