namespace NExpect.Interfaces
{
    public interface ITo<T>: IContinuation<T>
    {
        INotAfterTo<T> Not { get; }
        IBe<T> Be { get; }
    }
}