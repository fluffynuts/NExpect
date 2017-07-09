namespace NExpect
{
    public interface INonNegatingTo<T>: IExpectationContinuation<T>, INegated
    {
        IBe<T> Be { get; }
    }
}