using System.Collections.Generic;

namespace NExpect.Interfaces
{
    public interface ICollectionTo<T> : ICanAddMatcher<IEnumerable<T>>
    {
        IContain<IEnumerable<T>> Contain { get; }
        ICollectionNotAfterTo<T> Not { get; }
        ICollectionBe<T> Be { get; }
    }
}