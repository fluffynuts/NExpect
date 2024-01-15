using System;
using System.Collections.Generic;
using Imported.PeanutButter.Utils;
using NExpect.EqualityComparers;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect;

/// <summary>
/// Adds approximate equality matchers for decimal values
/// </summary>
// ReSharper disable once UnusedMember.Global
public static class ApproximateEqualityDecimalMatchers
{
    /// <summary>
    /// Tests if the actual value is approximately equal, to two decimal
    /// places
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected
    )
    {
        return approx.Equal(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Tests if the actual value is approximately equal, to two decimal
    /// places
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        string customMessage
    )
    {
        return approx.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Tests if the actual value is approximately equal, to two decimal
    /// places
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        return approx.Equal(
            expected,
            new DecimalsEqualToDecimalPlacesRounded(2),
            customMessageGenerator
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        decimal within
    )
    {
        return approx.Equal(
            expected,
            within,
            NULL_STRING
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        decimal within,
        string customMessage
    )
    {
        return approx.Equal(
            expected,
            within,
            () => customMessage
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        decimal within,
        Func<string> customMessageGenerator
    )
    {
        return approx.Equal(
            expected,
            new MaxDeltaComparer(within),
            customMessageGenerator
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        double within
    )
    {
        return approx.Equal(
            expected,
            within,
            NULL_STRING
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        double within,
        string customMessage
    )
    {
        return approx.Equal(
            expected,
            within,
            () => customMessage
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        double within,
        Func<string> customMessageGenerator
    )
    {
        return approx.Equal(
            expected,
            new MaxDeltaComparer((decimal)within),
            customMessageGenerator
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        long within
    )
    {
        return approx.Equal(
            expected,
            within,
            NULL_STRING
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        long within,
        string customMessage
    )
    {
        return approx.Equal(
            expected,
            within,
            () => customMessage
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        long within,
        Func<string> customMessageGenerator
    )
    {
        return approx.Equal(
            expected,
            new MaxDeltaComparer(within),
            customMessageGenerator
        );
    }

    /// <summary>
    /// Tests if the actual value is approximately equal, using the
    /// provided comparer
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        IEqualityComparer<decimal> comparer
    )
    {
        return approx.Equal(expected, comparer, NULL_STRING);
    }

    /// <summary>
    /// Tests if the actual value is approximately equal, using the
    /// provided comparer
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="comparer"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        IEqualityComparer<decimal> comparer,
        string customMessage
    )
    {
        return approx.Equal(expected, comparer, () => customMessage);
    }

    /// <summary>
    /// Tests if the actual value is approximately equal, to two decimal?
    /// places
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected
    )
    {
        return approx.Equal(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Tests if the actual value is approximately equal, to two decimal?
    /// places
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        string customMessage
    )
    {
        return approx.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Tests if the actual value is approximately equal, to two decimal?
    /// places
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        Func<string> customMessageGenerator
    )
    {
        return approx.Equal(
            expected,
            new DecimalsEqualToDecimalPlacesRounded(2),
            customMessageGenerator
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        decimal? within
    )
    {
        return approx.Equal(
            expected,
            within,
            NULL_STRING
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        decimal? within,
        string customMessage
    )
    {
        return approx.Equal(
            expected,
            within,
            () => customMessage
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        decimal? within,
        Func<string> customMessageGenerator
    )
    {
        return approx.Equal(
            expected,
            new MaxNullableDeltaComparer(within),
            customMessageGenerator
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        double within
    )
    {
        return approx.Equal(
            expected,
            within,
            NULL_STRING
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        double within,
        string customMessage
    )
    {
        return approx.Equal(
            expected,
            within,
            () => customMessage
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        double within,
        Func<string> customMessageGenerator
    )
    {
        return approx.Equal(
            expected,
            new MaxNullableDeltaComparer((decimal?)within),
            customMessageGenerator
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        long within
    )
    {
        return approx.Equal(
            expected,
            within,
            NULL_STRING
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        long within,
        string customMessage
    )
    {
        return approx.Equal(
            expected,
            within,
            () => customMessage
        );
    }

    /// <summary>
    /// Test for approximate equality within a defined range
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        long within,
        Func<string> customMessageGenerator
    )
    {
        return approx.Equal(
            expected,
            new MaxNullableDeltaComparer(within),
            customMessageGenerator
        );
    }

    /// <summary>
    /// Tests if the actual value is approximately equal, using the
    /// provided comparer
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        IEqualityComparer<decimal?> comparer
    )
    {
        return approx.Equal(expected, comparer, NULL_STRING);
    }

    /// <summary>
    /// Tests if the actual value is approximately equal, using the
    /// provided comparer
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="comparer"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        IEqualityComparer<decimal?> comparer,
        string customMessage
    )
    {
        return approx.Equal(
            expected,
            comparer,
            () => customMessage
        );
    }


    /// <summary>
    /// Tests if the actual value is approximately equal, using the
    /// provided comparer
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="comparer"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal expected,
        IEqualityComparer<decimal> comparer,
        Func<string> customMessageGenerator
    )
    {
        approx.AddMatcher(
            actual =>
            {
                var passed = comparer.Equals(actual, expected);
                return new MatcherResult(
                    passed,
                    () => FinalMessageFor(
                        () => $@"Expected {
                            actual.Stringify()
                        } to approximately equal {
                            expected.Stringify()
                        }",
                        customMessageGenerator
                    )
                );
            }
        );
        return approx.More();
    }


    /// <summary>
    /// Tests if the actual value is approximately equal, using the
    /// provided comparer
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="comparer"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal expected,
        IEqualityComparer<decimal> comparer,
        Func<string> customMessageGenerator
    )
    {
        return approx.AddMatcher(
            actual => CreateApproximatelyEqualMatcherResultFor(
                actual,
                expected,
                comparer,
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Tests if the actual value is approximately equal, using the
    /// provided comparer
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="comparer"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal expected,
        IEqualityComparer<decimal?> comparer,
        Func<string> customMessageGenerator
    )
    {
        approx.AddMatcher(
            actual => CreateApproximatelyEqualMatcherResultFor(
                actual,
                expected,
                comparer,
                customMessageGenerator
            )
        );
        return approx.More();
    }

    /// <summary>
    /// Tests if the actual value is approximately equal, using the
    /// provided comparer
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="comparer"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal?> Equal(
        this IApproximately<decimal?> approx,
        decimal? expected,
        IEqualityComparer<decimal?> comparer,
        Func<string> customMessageGenerator
    )
    {
        approx.AddMatcher(
            actual =>
            {
                if (actual is null)
                {
                    return new MatcherResult(
                        false,
                        FinalMessageFor(
                            () => $"Expected {false.AsNot()}to receive value approximately ${expected} (received null)",
                            customMessageGenerator
                        )
                    );
                }

                return CreateApproximatelyEqualMatcherResultFor(
                    actual,
                    expected,
                    comparer,
                    customMessageGenerator
                );
            }
        );
        return approx.More();
    }

    /// <summary>
    /// Verify that the two values are approximately equal, within
    /// the provided allowed difference
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal? expected,
        double within
    )
    {
        return approx.Equal(
            expected,
            within,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verify that the two values are approximately equal, within
    /// the provided allowed difference
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal? expected,
        double within,
        string customMessage
    )
    {
        return approx.Equal(expected, within, () => customMessage);
    }

    /// <summary>
    /// Verify that the two values are approximately equal, within
    /// the provided allowed difference
    /// </summary>
    /// <param name="approx"></param>
    /// <param name="expected"></param>
    /// <param name="within"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<decimal> Equal(
        this IApproximately<decimal> approx,
        decimal? expected,
        double within,
        Func<string> customMessageGenerator
    )
    {
        return approx.AddMatcher(
            actual => CreateApproximatelyEqualMatcherResultFor(
                actual,
                expected,
                new MaxNullableDeltaComparer((decimal?)within),
                customMessageGenerator
            )
        );
    }

    private static IMatcherResult CreateApproximatelyEqualMatcherResultFor(
        decimal? actual,
        decimal? expected,
        IEqualityComparer<decimal?> comparer,
        Func<string> customMessageGenerator
    )
    {
        if (FoundNullAnswerFor(
                actual,
                expected,
                customMessageGenerator,
                out var result
            ))
        {
            return result;
        }

        var passed = comparer.Equals(actual, expected);
        return CreateFinalResultFor(
            passed,
            actual,
            expected,
            customMessageGenerator
        );
    }

    private static IMatcherResult CreateApproximatelyEqualMatcherResultFor(
        decimal? actual,
        decimal? expected,
        IEqualityComparer<decimal> comparer,
        Func<string> customMessageGenerator
    )
    {
        if (FoundNullAnswerFor(
                actual,
                expected,
                customMessageGenerator,
                out var result
            ))
        {
            return result;
        }

        var passed = comparer.Equals(actual!.Value, expected!.Value);
        return CreateFinalResultFor(passed, actual, expected, customMessageGenerator);
    }

    private static IMatcherResult CreateFinalResultFor(
        bool passed,
        decimal? actual,
        decimal? expected,
        Func<string> customMessageGenerator
    )
    {
        return new MatcherResult(
            passed,
            () => FinalMessageFor(
                () => $@"Expected {
                    actual.Stringify()
                } to approximately equal {
                    expected.Stringify()
                }",
                customMessageGenerator
            )
        );
    }

    private static bool FoundNullAnswerFor(
        decimal? actual,
        decimal? expected,
        Func<string> customMessageGenerator,
        out IMatcherResult result
    )
    {
        result = default;
        if (actual is null && expected is null)
        {
            result = new MatcherResult(
                true,
                FinalMessageFor(
                    () => $"Expected {true.AsNot()}to receive two approximately equal values (both are null)",
                    customMessageGenerator
                )
            );
            return true;
        }

        if (actual is null)
        {
            result = new MatcherResult(
                false,
                FinalMessageFor(
                    () =>
                        $"Expected {false.AsNot()}to receive value approximately equal to ${expected} (received null)",
                    customMessageGenerator
                )
            );
            return true;
        }

        if (expected is null)
        {
            result = new MatcherResult(
                false,
                FinalMessageFor(
                    () => "",
                    customMessageGenerator
                )
            );
            return true;
        }

        return false;
    }
}