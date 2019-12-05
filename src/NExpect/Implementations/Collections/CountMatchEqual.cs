using System.Collections.Generic;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations.Collections
{
    internal class CountMatchEqual<T>
        : ExpectationContext<IEnumerable<T>>,
           ICountMatchEqual<T>
    {
        public ICanAddMatcher<T> Continuation { get; }
        public T Actual => Continuation.GetActual();
        public CountMatchMethods Method { get; }
        public int ExpectedCount { get; }

        public CountMatchEqual(
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