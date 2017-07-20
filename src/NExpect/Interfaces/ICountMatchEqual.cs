namespace NExpect.Interfaces
{
    public interface ICountMatchEqual<T>
    {
        IContinuation<T> Continuation { get; }
        CountMatchMethods Method { get; }
        int Compare { get; }
    }
}