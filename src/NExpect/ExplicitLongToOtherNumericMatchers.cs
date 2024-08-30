using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect;

/// <summary>
/// After introducing easier int-to-enum comparisons,
/// the implicit cast that used to work now doesn't,
/// so now we need to explicitly widen
/// </summary>
public static class ExplicitLongToOtherNumericMatchers
{
    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this ITo<long> to,
        decimal expected
    )
    {
        return to.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this ITo<long> to,
        decimal expected,
        string customMessage
    )
    {
        return to.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this ITo<long> to,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
            actual =>
            {
                var passed = actual == expected;
                return new MatcherResult(
                    passed,
                    () => $"Expected {actual} {passed.AsNot()}to equal {expected}",
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this ITo<long> to,
        double expected
    )
    {
        return to.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this ITo<long> to,
        double expected,
        string customMessage
    )
    {
        return to.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this ITo<long> to,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
            actual =>
            {
                var passed = Math.Abs(actual - expected) < double.Epsilon;
                return new MatcherResult(
                    passed,
                    () => $"Expected {actual} {passed.AsNot()}to equal {expected}",
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this INotAfterTo<long> to,
        decimal expected
    )
    {
        return to.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this INotAfterTo<long> to,
        decimal expected,
        string customMessage
    )
    {
        return to.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this INotAfterTo<long> to,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
            actual =>
            {
                var passed = actual == expected;
                return new MatcherResult(
                    passed,
                    () => $"Expected {actual} {passed.AsNot()}to equal {expected}",
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this INotAfterTo<long> to,
        double expected
    )
    {
        return to.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this INotAfterTo<long> to,
        double expected,
        string customMessage
    )
    {
        return to.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this INotAfterTo<long> to,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
            actual =>
            {
                var passed = Math.Abs(actual - expected) < double.Epsilon;
                return new MatcherResult(
                    passed,
                    () => $"Expected {actual} {passed.AsNot()}to equal {expected}",
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this IToAfterNot<long> to,
        decimal expected
    )
    {
        return to.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this IToAfterNot<long> to,
        decimal expected,
        string customMessage
    )
    {
        return to.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this IToAfterNot<long> to,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
            actual =>
            {
                var passed = actual == expected;
                return new MatcherResult(
                    passed,
                    () => $"Expected {actual} {passed.AsNot()}to equal {expected}",
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this IToAfterNot<long> to,
        double expected
    )
    {
        return to.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this IToAfterNot<long> to,
        double expected,
        string customMessage
    )
    {
        return to.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Verifies that the value is equal to
    /// the expected value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<long> Equal(
        this IToAfterNot<long> to,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
            actual =>
            {
                var passed = Math.Abs(actual - expected) < double.Epsilon;
                return new MatcherResult(
                    passed,
                    () => $"Expected {actual} {passed.AsNot()}to equal {expected}",
                    customMessageGenerator
                );
            }
        );
    }
}