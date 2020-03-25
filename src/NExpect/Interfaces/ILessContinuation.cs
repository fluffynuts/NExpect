namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .Less grammar continuation
    /// </summary>
    /// <typeparam name="T">Type of the continuation</typeparam>
    public interface ILessContinuation<T> : ICanAddMatcher<T>
    {
        /// <summary>
        /// Starts the .Less.Than.Or.Equal.To
        /// </summary>
        ILessThan<T> Than { get; }
    }
}