// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .Less grammar continuation
    /// </summary>
    /// <typeparam name="T">Type of the continuation</typeparam>
    public interface ILessContinuation<T> : IGreaterOrLessContinuation<T>
    {
    }
}