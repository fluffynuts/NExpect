using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.GreaterAndLessMatchersCommon;

namespace NExpect;

/// <summary>
/// Provides the .At.Least matchers
/// (alternative syntax for Greater.Than.Or.Equal.To)
/// </summary>
public static class AtLeastMatchers
{
    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<DateTime?> Least(
        this IAt<DateTime?> continuation,
        DateTime expected
    )
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<DateTime?> Least(
        this IAt<DateTime?> continuation,
        DateTime expected,
        string customMessage
    )
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<DateTime?> Least(
        this IAt<DateTime?> continuation,
        DateTime expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a >= e,
            customMessageGenerator);
        return continuation.More();
    }


    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long?> Least(
        this IAt<long?> continuation,
        double expected
    )
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add Least failure messages</param>
    public static IMore<long?> Least(
        this IAt<long?> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long?> Least(
        this IAt<long?> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a >= e,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long> Least(
        this IAt<long> continuation,
        double expected
    )
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long> Least(
        this IAt<long> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long> Least(
        this IAt<long> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a >= e,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long?> Least(
        this IAt<long?> continuation,
        long expected
    )
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long?> Least(
        this IAt<long?> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long?> Least(
        this IAt<long?> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a >= e,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long> Least(
        this IAt<long> continuation,
        long expected
    )
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long> Least(
        this IAt<long> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long> Least(
        this IAt<long> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a >= e,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<T1> Least<T1, T2>(
        this IAt<T1> continuation,
        T2 expected
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<T1> Least<T1, T2>(
        this IAt<T1> continuation,
        T2 expected,
        string customMessage
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<T1> Least<T1, T2>(
        this IAt<T1> continuation,
        T2 expected,
        Func<string> customMessageGenerator
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => TryCompare(a, e) > -1,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<T1?> Least<T1, T2>(
        this IAt<T1?> continuation,
        T2 expected
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<T1?> Least<T1, T2>(
        this IAt<T1?> continuation,
        T2 expected,
        string customMessage
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<T1?> Least<T1, T2>(
        this IAt<T1?> continuation,
        T2 expected,
        Func<string> customMessageGenerator
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => TryCompare(a, e) > -1,
            customMessageGenerator);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<decimal?> Least(
        this IAt<decimal?> continuation,
        decimal expected
    )
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<decimal?> Least(
        this IAt<decimal?> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<decimal?> Least(
        this IAt<decimal?> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a >= e,
            customMessageGenerator);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<decimal> Least(
        this IAt<decimal> continuation,
        decimal expected
    )
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<decimal> Least(
        this IAt<decimal> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<decimal> Least(
        this IAt<decimal> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a >= e,
            customMessageGenerator);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long?> Least(
        this IAt<long?> continuation,
        decimal expected
    )
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long?> Least(
        this IAt<long?> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long?> Least(
        this IAt<long?> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a >= e,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long> Least(
        this IAt<long> continuation,
        decimal expected
    )
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long> Least(
        this IAt<long> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long> Least(
        this IAt<long> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a >= e,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<double?> Least(
        this IAt<double?> continuation,
        decimal expected
    )
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<double?> Least(
        this IAt<double?> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<double?> Least(
        this IAt<double?> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && new Decimal(a.Value) >= e,
            customMessageGenerator);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<double?> Least(
        this IAt<double?> continuation,
        double expected
    )
    {
        return continuation.Least(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<double?> Least(
        this IAt<double?> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Least(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<double?> Least(
        this IAt<double?> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a >= e,
            customMessageGenerator
        );
    }
}