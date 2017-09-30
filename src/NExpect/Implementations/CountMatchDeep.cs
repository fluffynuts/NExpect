using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CountMatchDeep<T>: 
        ExpectationContext<T>,
        ICountMatchDeep<T>, 
        ICountMatchEqual<T>
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
}