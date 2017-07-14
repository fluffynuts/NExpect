namespace NExpect.Interfaces
{
    public interface INot<T>: IGrammarContinuation<T>
    {
        IToAfterNot<T> To { get; }
    }
}