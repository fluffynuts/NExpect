using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect;

/// <summary>
/// Adds approximate equality matchers for DateTime values
/// </summary>
public static class ApproximateEqualityNullableDateTimeMatchers
{
    /// <summary>
    /// Tests if the actual DateTime is approximately equal to the
    /// expected value within 1 second
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <returns></returns>
    public static IMore<DateTime?> Equal(
        this IApproximately<DateTime?> continuation,
        DateTime? expected
    )
    {
        return continuation.Equal(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Tests if the actual DateTime is approximately equal to the
    /// expected value within the allowed drift timespan
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="allowedDrift">How much the actual value may drift from the expected
    /// value</param>
    /// <returns></returns>
    public static IMore<DateTime?> Equal(
        this IApproximately<DateTime?> continuation,
        DateTime? expected,
        TimeSpan allowedDrift
    )
    {
        return continuation.Equal(
            expected,
            allowedDrift,
            MessageHelpers.NULL_STRING
        );
    }

    /// <summary>
    /// Tests if the actual DateTime is approximately equal to the
    /// expected value within 1 second
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to include when
    /// this expectation fails</param>
    /// <returns></returns>
    public static IMore<DateTime?> Equal(
        this IApproximately<DateTime?> continuation,
        DateTime? expected,
        string customMessage
    )
    {
        return continuation.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Tests if the actual DateTime is approximately equal to the
    /// expected value within 1 second
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Generates a custom message to include when
    /// this expectation fails</param>
    /// <returns></returns>
    public static IMore<DateTime?> Equal(
        this IApproximately<DateTime?> continuation,
        DateTime? expected,
        Func<string> customMessageGenerator
    )
    {
        return continuation.Equal(
            expected,
            TimeSpan.FromSeconds(1),
            customMessageGenerator
        );
    }

    /// <summary>
    /// Tests if the actual DateTime is approximately equal to the
    /// expected value within the provided allowed drift value
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="allowedDrift">How much the actual value may drift from the expected
    /// value</param>
    /// <param name="customMessage">Custom message to include when
    /// this expectation fails</param>
    /// <returns></returns>
    public static IMore<DateTime?> Equal(
        this IApproximately<DateTime?> continuation,
        DateTime? expected,
        TimeSpan allowedDrift,
        string customMessage
    )
    {
        return continuation.Equal(
            expected,
            allowedDrift,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if the actual DateTime is approximately equal to the
    /// expected value within the provided allowed drift value
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="allowedDrift">How much the actual value may drift from the expected
    /// value</param>
    /// <param name="customMessageGenerator">Generates a custom message to include when
    /// this expectation fails</param>
    /// <returns></returns>
    public static IMore<DateTime?> Equal(
        this IApproximately<DateTime?> continuation,
        DateTime? expected,
        TimeSpan allowedDrift,
        Func<string> customMessageGenerator
    )
    {
        continuation.AddMatcher(
            actual =>
            {
                bool passed;
                if (actual is null && expected is null)
                {
                    passed = true;
                }
                else if (actual is null || expected is null)
                {
                    passed = false;
                }
                else
                {
                    var delta = actual == null
                        ? allowedDrift.TotalMilliseconds + 1
                        : Math.Abs(
                            (actual.Value - expected.Value)
                            .TotalMilliseconds
                        );
                    var allowed = Math.Abs(allowedDrift.TotalMilliseconds);
                    passed = delta <= allowed;
                }

                return new MatcherResult(
                    passed,
                    () => MessageHelpers.FinalMessageFor(
                        ()
                            => $@"Expected {
                                actual.Stringify()
                            } to approximately equal {
                                expected.Stringify()
                            } within a timespan of {
                                allowedDrift.Stringify()
                            }",
                        customMessageGenerator
                    )
                );
            }
        );
        return continuation.More();
    }
}