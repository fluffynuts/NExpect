namespace NExpect.Interfaces
{
    public interface ICollectionHaveAll<T>
    {
        ICollectionHaveAllEqual<T> Equal { get; }
    }
}