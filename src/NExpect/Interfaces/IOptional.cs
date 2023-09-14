namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the type for the .With.Optional grammar
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOptional<T> : ICanAddMatcher<T>
    {
    }
}