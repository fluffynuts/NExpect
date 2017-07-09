namespace NExpect
{
    public interface INot<T>: IExpectationContinuation<T>
    {
        INonNegatingTo<T> To { get; }
    }
}