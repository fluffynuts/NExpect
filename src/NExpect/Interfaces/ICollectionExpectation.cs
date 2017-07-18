namespace NExpect.Interfaces
{
    public interface ICollectionExpectation<T>
    {
        ICollectionTo<T> To { get; }
        ICollectionNot<T> Not { get; }
    }
}