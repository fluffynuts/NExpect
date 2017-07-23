using System.Collections.Generic;

namespace NExpect.Interfaces
{
    public interface ICollectionToAfterNot<T> : IContinuation<IEnumerable<T>>
    {
        IContain<IEnumerable<T>> Contain { get; }
        ICollectionHave<T> Have { get; }
    }
}