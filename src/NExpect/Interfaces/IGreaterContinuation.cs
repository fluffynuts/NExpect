// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the extension point for .Greater.Than()
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGreaterContinuation<T> : IGreaterOrLessContinuation<T>
    {
    }
}