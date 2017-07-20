namespace NExpect.Interfaces
{
    /// <summary>
    /// Base interface for grammar continuations
    /// </summary>
    /// <typeparam name="T">Underlying type of the continuation</typeparam>
    public interface IGrammarContinuation<T>
    {
        /// <summary>
        /// Actual value in the continuation
        /// </summary>
        T Actual { get; }
    }
}