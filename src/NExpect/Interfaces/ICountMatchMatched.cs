namespace NExpect.Interfaces
{
    public interface ICountMatchMatched<T>
    {
        IContinuation<T> Continuation { get; }
        CountMatchMethods Method { get; }
        int Compare { get; }
    }
}