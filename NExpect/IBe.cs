namespace NExpect
{
    public interface IBe<T>: IExpectationContinuation<T>
    {
        INegatedBe<T> Not { get; }
    }
}