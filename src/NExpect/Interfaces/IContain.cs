namespace NExpect.Interfaces
{
    public interface IContain<T> : IContinuation<T>
    {
        IContain<T> At { get; }
    }

}