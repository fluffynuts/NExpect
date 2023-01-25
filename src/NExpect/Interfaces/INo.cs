namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .No dangling grammar type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INo<T> : ICanAddMatcher<T>
    {
    }
}