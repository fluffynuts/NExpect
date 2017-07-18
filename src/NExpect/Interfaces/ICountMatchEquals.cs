namespace NExpect.Interfaces
{
    public interface ICountMatchEquals<T>
    {
        IContinuation<T> Continuation { get; }
        CountMatchMethods Method { get; }
        int Compare { get; }
    }
}