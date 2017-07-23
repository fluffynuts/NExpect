using System.Collections.Generic;

namespace NExpect.Interfaces
{
    public interface ICollectionToAfterNot<T> : ICanAddMatcher<IEnumerable<T>>
    {
        IContain<IEnumerable<T>> Contain { get; }
    }
}