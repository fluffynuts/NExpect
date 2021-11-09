namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the interface for the .Having continuation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHaving<T> : ICanAddMatcher<T>
    {
    }
}