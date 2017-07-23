namespace NExpect.Interfaces
{
    public interface ICountMatchEqual<T>
    {
        ICanAddMatcher<T> Continuation { get; }
        CountMatchMethods Method { get; }
        int Compare { get; }
    }
}