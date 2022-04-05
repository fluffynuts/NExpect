using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations.Collections;

internal class CountMatchContinuationOfStringCollection
    : CountMatchContinuation<IEnumerable<string>>,
      ICountMatchContinuationOfStringCollection
{
    public CountMatchContinuationOfStringCollection(
        ICanAddMatcher<IEnumerable<string>> wrapped, CountMatchMethods method, int expectedCount
    ) : base(wrapped, method, expectedCount)
    {
    }

    public ICountMatchContinuationOfStringCollectionEnding Ending =>
        new CountMatchContinuationOfStringCollectionVerb(
            this,
            _method,
            _expectedCount
        );

    public ICountMatchContinuationOfStringCollectionStarting Starting =>
        new CountMatchContinuationOfStringCollectionVerb(
            this,
            _method,
            _expectedCount
        );
}