namespace NExpect
{
    public interface ITo<T>: IContinuation<T>
    {
        INotAfterTo<T> Not { get; }
        IBe<T> Be { get; }
    }
}