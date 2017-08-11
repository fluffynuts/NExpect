using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CountMatchContinuation<T>
        : ExpectationContext<T>,
            ICountMatchContinuation<T>
    {
        private readonly int _compare;
        private readonly CountMatchMethods _method;
        private readonly ICanAddMatcher<T> _wrapped;

        public ICountMatchEqual<T> Equal =>
            new CountMatchEqual<T>(
                _wrapped,
                _method,
                _compare
            );

        public ICountMatchMatched<T> Matched =>
            new CountMatchMatched<T>(
                _wrapped,
                _method,
                _compare
            );

        public CountMatchContinuation(
            ICanAddMatcher<T> wrapped,
            CountMatchMethods method,
            int compare
        )
        {
            _wrapped = wrapped;
            _method = method;
            _compare = compare;
        }
    }
}