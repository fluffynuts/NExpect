using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CountMatchContinuation<T>
        : ExpectationContext<IEnumerable<T>>, ICountMatchContinuation<T>
    {
        private readonly int _compare;
        private readonly CountMatchMethods _method;
        private readonly IContinuation<T> _wrapped;

        public ICountMatchEquals<T> Equal =>
            new CountMatchEquals<T>(
                _wrapped,
                _method,
                _compare
            );


        public CountMatchContinuation(
            IContinuation<T> wrapped,
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