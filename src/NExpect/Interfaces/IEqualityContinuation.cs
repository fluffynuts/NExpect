namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the extension point for .Equal() or any similar
    /// extension in your domain.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEqualityContinuation<T>: ICanAddMatcher<T>
    {
    }
}