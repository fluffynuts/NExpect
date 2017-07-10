namespace NExpect
{
    public interface IBe<T>: IContinuation<T>
    {
        INotAfterBe<T> Not { get; }
    }
}