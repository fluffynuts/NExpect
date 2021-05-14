using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency
{
    internal class PropertyNot<TValue>
        : ExpectationContextWithLazyActual<TValue>, IPropertyNot<TValue>
    {
        public IToAfterNot<TValue> To => Next<ToAfterNot<TValue>>();

        public PropertyNot(Func<TValue> actualFetcher) : base(actualFetcher)
        {
        }
    }
}