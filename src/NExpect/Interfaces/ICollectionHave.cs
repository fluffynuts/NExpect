namespace NExpect.Interfaces
{
    public interface ICollectionHave<T>
    {
        ICollectionHaveAll<T> All { get; }

        ICollectionHaveAny<T> Any { get; }
    }
}