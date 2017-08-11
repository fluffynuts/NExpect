namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the extension point for testing count-matches within collections.
    /// Essentially, the original continuation is frankensquished into this.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICountMatchEqual<T>
    {
        /// <summary>
        /// Original continuation
        /// </summary>
        ICanAddMatcher<T> Continuation { get; }
        /// <summary>
        /// Method to use when comparing the count, eg:
        /// - at least
        /// - at most
        /// - exactly
        /// - any
        /// - all
        /// </summary>
        CountMatchMethods Method { get; }
        /// <summary>
        /// Comparison count, where appropriate (at least, at most, exactly)
        /// </summary>
        int Compare { get; }
    }
}