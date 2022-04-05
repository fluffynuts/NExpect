namespace NExpect.Interfaces;

/// <summary>
/// Provides a string-specific expectation with a few string-specific features
/// </summary>
public interface IStringExpectation: IExpectation<string>
{
    /// <summary>
    /// Starts a string-specific .To
    /// </summary>
    new IStringTo To { get; }

    /// <summary>
    /// Starts a string-specific .Not
    /// </summary>
    new IStringNot Not { get; }
}