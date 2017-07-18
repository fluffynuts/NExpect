namespace NExpect.Interfaces
{
    public interface IExceptionMessageContinuation
    {
        IEqualityContinuation<string> Equal { get; }
    }

    public interface IEqualityContinuation<T>
    {
    }
}