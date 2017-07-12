namespace NExpect.Interfaces
{
    public interface IToAfterNot<T>: IContinuation<T>
    {
        IBe<T> Be { get; }
    }
}