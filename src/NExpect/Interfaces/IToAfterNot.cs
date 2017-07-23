namespace NExpect.Interfaces
{
    public interface IToAfterNot<T>: ICanAddMatcher<T>
    {
        IBe<T> Be { get; }
        IContain<T> Contain { get; }
    }
}