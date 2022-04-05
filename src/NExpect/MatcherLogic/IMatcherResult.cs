using System;

namespace NExpect.MatcherLogic;

/// <summary>
/// Result returned from a matcher
/// </summary>
public interface IMatcherResult
{
    /// <summary>
    /// Whether or not the matcher passed
    /// </summary>
    bool Passed { get; }

    /// <summary>
    /// Message to display. In the case when your matcher
    /// passes, put in the message you would display if your
    /// result were negated, for example, if testing equality 
    /// to 1, when Passed is false, one would return:
    /// "expected value to equal 1"
    /// and one would then say, when Passed is true:
    /// "expected value not to equal 1"
    /// </summary>
    string Message { get; }

    /// <summary>
    /// Exception from the area where the matcher result occurred, if any.
    /// Used by .Throw();{}{}{{{}
    /// </summary>
    Exception LocalException { get; }
}