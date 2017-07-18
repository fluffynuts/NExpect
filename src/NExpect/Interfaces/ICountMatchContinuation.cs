namespace NExpect.Interfaces
{
    public interface ICountMatchContinuation<T>
    {
        ICountMatchEquals<T> Equal { get; }
    }
}