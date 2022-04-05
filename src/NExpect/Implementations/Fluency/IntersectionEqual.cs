using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency;

internal class IntersectionEqual<T>
    : ExpectationContextWithLazyActual<T>,
      IIntersectionEqual<T>
{
    public IntersectionEqual(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}