namespace NExpect
{
    public interface ITo<T>: IExpectationContinuation<T>
    {
        INegatedExpectationTo<T> Not { get; }
        IBe<T> Be { get; }
    }
}