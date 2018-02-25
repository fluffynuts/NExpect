using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class CountMatchContinuationOfStringCollectionVerb
        : CountMatchContinuationOfStringCollection,
            ICountMatchContinuationOfStringCollectionEnding,
            ICountMatchContinuationOfStringCollectionStarting
    {
        public CountMatchContinuationOfStringCollectionVerb(
            ICountMatchContinuation<IEnumerable<string>> wrapped,
            CountMatchMethods method,
            int expectedCount
        ) : base(wrapped, method, expectedCount)
        {
        }

        public ICountMatchContinuationOfStringCollection Wrapped =>
            _wrapped as ICountMatchContinuationOfStringCollection;
    }
}