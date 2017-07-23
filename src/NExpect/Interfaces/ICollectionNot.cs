using System.Collections.Generic;

namespace NExpect.Interfaces
{
    public interface ICollectionNot<T> : ICanAddMatcher<IEnumerable<T>>
    {
        ICollectionToAfterNot<T> To { get; }
    }
}