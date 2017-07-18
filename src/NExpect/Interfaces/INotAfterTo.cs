namespace NExpect.Interfaces
{
    public interface INotAfterTo<T>: IContinuation<T>
    {
        IBe<T> Be { get; }
    }
}