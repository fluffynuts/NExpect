using System.Collections.Generic;

namespace NExpect.Interfaces
{
    public interface ICollectionBe<T> : ICanAddMatcher<IEnumerable<T>>
    {
        ICollectionEquivalent<T> Equivalent { get; }
    }
}