namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .With dangling grammar type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWith<T>: ICanAddMatcher<T>
    {
    }

    /// <summary>
    /// Provides the .Of dangling grammar type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOf<T> : ICanAddMatcher<T>
    {
    }
}