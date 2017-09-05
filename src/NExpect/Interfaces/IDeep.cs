namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .Deep grammar for objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDeep<T>: ICanAddMatcher<T>
    {
    }
}