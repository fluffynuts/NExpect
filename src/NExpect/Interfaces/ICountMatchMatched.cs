namespace NExpect.Interfaces
{
    public interface ICountMatchMatched<T>
    {
        ICanAddMatcher<T> Continuation { get; }
        CountMatchMethods Method { get; }
        int Compare { get; }
    }
}