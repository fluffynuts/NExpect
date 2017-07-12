namespace NExpect.Interfaces
{
    public interface IExpectation<T>
    {
        T Actual { get; }

        ITo<T> To { get; }
        INot<T> Not { get; }
    }

}