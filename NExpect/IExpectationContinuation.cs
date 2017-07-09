namespace NExpect
{
    public interface IExpectationContinuation<T>
    {
        T Actual { get; }
    }
}