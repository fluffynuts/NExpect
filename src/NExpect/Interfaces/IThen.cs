namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .Then grammar extension
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IThen<T> : ICanAddMatcher<T>
    {
    }
}