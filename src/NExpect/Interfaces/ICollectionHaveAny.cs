namespace NExpect.Interfaces
{
    public interface ICollectionHaveAny<T>
    {
        ICollectionHaveAnyEqual<T> Equal { get; }
    }
}