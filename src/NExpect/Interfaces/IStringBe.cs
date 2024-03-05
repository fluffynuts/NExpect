namespace NExpect.Interfaces;

/// <summary>
/// Facilitates the string-specific .Be syntax
/// </summary>
public interface IStringBe: IBe<string>
{
    /// <summary>
    /// Starts an expectation to match the Actual string with
    /// a regular expression
    /// </summary>
    new IStringMatched Matched { get; }

    /// <summary>
    /// Starts an expectation to assert that
    /// the primary string is shorter than
    /// another string
    /// </summary>
    IStringShorter Shorter { get; }

    /// <summary>
    /// Starts an expectation to assert that
    /// the primary string is shorter than
    /// another string
    /// </summary>
    IStringLonger Longer { get; }
}