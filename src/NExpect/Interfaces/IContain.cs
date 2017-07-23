namespace NExpect.Interfaces
{
    public interface IContain<T> : ICanAddMatcher<T>
    {
        IContainAt<T> At { get; }
    }
}