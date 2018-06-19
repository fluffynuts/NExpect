namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the longer .To.Be.Equal.To syntax for longer intersection equals
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIntersectionEqual<T> : ICanAddMatcher<T>
    {
    }
}