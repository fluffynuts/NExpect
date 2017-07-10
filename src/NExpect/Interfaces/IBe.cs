namespace NExpect
{
    public interface IBe<T>: IContinuation<T>
    {
        IBeAfterNot<T> Not { get; }
    }
}