namespace NExpect.Interfaces
{
    public interface INot<T>: ICanAddMatcher<T>
    {
        IToAfterNot<T> To { get; }
    }
}