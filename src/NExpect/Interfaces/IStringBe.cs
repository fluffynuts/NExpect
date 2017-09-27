namespace NExpect.Interfaces
{
    /// <summary>
    /// Facilitates the string-specific .Be syntax
    /// </summary>
    public interface IStringBe: IBe<string>
    {
        /// <summary>
        /// Starts an expectation to match the Actual string with
        /// a regular expression
        /// </summary>
        IStringMatched Matched { get; }
    }
}