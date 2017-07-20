namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation from, eg At.Least(N)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICountMatchContinuation<T>
    {
        /// <summary>
        /// Continuation to attempt to match the collection items exactly
        /// </summary>
        ICountMatchEquals<T> Equal { get; }
    }
}