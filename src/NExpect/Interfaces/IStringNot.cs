namespace NExpect.Interfaces
{
    /// <summary>
    /// Starts a negated expectation for a string Actual
    /// </summary>
    public interface IStringNot: INot<string>
    {
        /// <summary>
        /// Starts a string-specific .To
        /// </summary>
        new IStringToAfterNot To { get; }

        /// <summary>
        /// Starts shorter grammar negated start test
        /// </summary>
        IStringStart Start { get; }

        /// <summary>
        /// Starts shorter grammar negated end test
        /// </summary>
        IStringEnd End { get; }
    }
}