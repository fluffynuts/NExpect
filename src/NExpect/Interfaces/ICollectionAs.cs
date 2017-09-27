namespace NExpect.Interfaces
{
    public interface ICollectionAs<T>
    {
        ICollectionExpectation<object> Objects { get; }
    }
}