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
        IContain<IEnumerable<T>> Contain { get; }
        ICollectionNotAfterTo<T> Not { get; }
    }

    public interface ICollectionNot<T> : IContinuation<IEnumerable<T>>
    {
        ICollectionToAfterNot<T> To { get; }
    }

    public interface ICollectionToAfterNot<T> : IContinuation<IEnumerable<T>>
    {
        IContain<IEnumerable<T>> Contain { get; }
    }

    public interface ICollectionNotAfterTo<T> : IContinuation<IEnumerable<T>>
    {
        IContain<IEnumerable<T>> Contain { get; }
    }
}