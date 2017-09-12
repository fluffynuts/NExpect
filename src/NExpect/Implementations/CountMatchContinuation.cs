using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CountMatchContinuation<T>
        : ExpectationContext<T>,
            ICountMatchContinuation<T>
    {
        public int ExpectedCount => _expectedCount;
        public CountMatchMethods Method => _method;

        private readonly int _expectedCount;
        private readonly CountMatchMethods _method;
        private readonly ICanAddMatcher<T> _wrapped;

        public ICountMatchEqual<T> Equal =>
            new CountMatchEqual<T>(
                _wrapped,
                _method,
                _expectedCount
            );

        public ICountMatchMatched<T> Matched =>
            new CountMatchMatched<T>(
                _wrapped,
                _method,
                _expectedCount
            );

        public ICountMatchDeep<T> Deep =>
            CreateCountMatchDeep();

        public ICountMatchIntersection<T> Intersection =>
            CreateCountMatchIntersection();

        private ICountMatchIntersection<T> CreateCountMatchIntersection()
        {
            var result = new CountMatchIntersection<T>(
                _wrapped,
                _method,
                _expectedCount
            );
            result.SetParent(this);
            return result;
        }

        private CountMatchDeep<T> CreateCountMatchDeep()
        {
            var result = new CountMatchDeep<T>(
                _wrapped,
                _method,
                _expectedCount
            );
            result.SetParent(this);
            return result;
        }

        public CountMatchContinuation(
            ICanAddMatcher<T> wrapped,
            CountMatchMethods method,
            int expectedCount
        )
        {
            _wrapped = wrapped;
            _method = method;
            _expectedCount = expectedCount;
            SetParent(wrapped as IExpectationContext<T>);
        }
    }
}