using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CountMatchContinuation<T>
        : ExpectationContext<T>,
            ICountMatchContinuation<T>
    {
        public int ExpectedCount => _expectedCount;
        public CountMatchMethods CountMatchMethod => _countMatchMethod;

        private readonly int _expectedCount;
        private readonly CountMatchMethods _countMatchMethod;
        private readonly ICanAddMatcher<T> _wrapped;

        public ICountMatchEqual<T> Equal =>
            new CountMatchEqual<T>(
                _wrapped,
                _countMatchMethod,
                _expectedCount
            );

        public ICountMatchMatched<T> Matched =>
            new CountMatchMatched<T>(
                _wrapped,
                _countMatchMethod,
                _expectedCount
            );

        public CountMatchContinuation(
            ICanAddMatcher<T> wrapped,
            CountMatchMethods countMatchMethod,
            int expectedCount
        )
        {
            _wrapped = wrapped;
            _countMatchMethod = countMatchMethod;
            _expectedCount = expectedCount;
            SetParent(wrapped as IExpectationContext<T>);
        }
    }
}