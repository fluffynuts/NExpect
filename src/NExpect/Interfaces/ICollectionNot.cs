using System.Collections.Generic;

namespace NExpect.Interfaces
{
    public interface ICollectionNot<T> : IContinuation<IEnumerable<T>>
    {
        ICollectionToAfterNot<T> To { get; }
    }
}