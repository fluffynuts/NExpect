namespace NExpect
{
    public interface INot<T>: IContinuation<T>
    {
        IToAfterNot<T> To { get; }
    }
}