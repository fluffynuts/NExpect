namespace NExpect.Interfaces
{
    public interface ITo<T> : ICanAddMatcher<T>
    {
        INotAfterTo<T> Not { get; }
        IBe<T> Be { get; }
    }
}