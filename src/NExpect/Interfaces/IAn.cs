// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation to provide the ".An" grammar
    /// </summary>
    /// <typeparam name="T">Type of the continuation</typeparam>
    public interface IAn<T> : ICanAddMatcher<T>
    {
        /// <summary>
        /// Prepares to check the Type of Actual
        /// </summary>
        IInstanceContinuation Instance { get; }
    }
}