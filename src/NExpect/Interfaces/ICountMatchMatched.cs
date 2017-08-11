namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the matched count match. Match!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICountMatchMatched<T>
    {
        /// <summary>
        /// The original continuation
        /// </summary>
        ICanAddMatcher<T> Continuation { get; }
        /// <summary>
        /// The count match method
        /// </summary>
        CountMatchMethods Method { get; }
        /// <summary>
        /// The count match expected value, if appropriate
        /// </summary>
        int Compare { get; }
    }
}