using System.Collections.Generic;

namespace NExpect.Interfaces
{
    public interface ICollectionTo<T> : IContinuation<IEnumerable<T>>
    {
        IContain<IEnumerable<T>> Contain { get; }
        ICollectionNotAfterTo<T> Not { get; }
        ICollectionHave<T> Have { get; }
    }
}