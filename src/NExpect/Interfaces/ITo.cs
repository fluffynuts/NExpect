using System.Collections.Generic;

namespace NExpect.Interfaces
{
    public interface ITo<T> : IContinuation<T>
    {
        INotAfterTo<T> Not { get; }
        IBe<T> Be { get; }
    }

    public interface ICollectionTo<T> : IContinuation<IEnumerable<T>>
    {
        IContain<T> Contain { get; }
    }

    public interface ICollectionNot<T> : IContinuation<IEnumerable<T>>
    {
        IContain<T> Contain { get; }
    }
}