namespace NExpect.Interfaces
{
    public interface IExceptionMessageContinuation
    {
        IEqualityContinuation<string> Equal { get; }
        INot<string> Not { get; }
    }

    public interface IEqualityContinuation<T>
    {
    }
}