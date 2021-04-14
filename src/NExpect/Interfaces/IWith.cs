namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .With dangling grammar type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWith<T>: ICanAddMatcher<T>
    {
    }
}