using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.GreaterAndLessMatchersCommon;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides the .Less.Than matchers
/// </summary>
public static class LessThanMatchers
{
    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<T1?> Than<T1, T2>(
        this ILessContinuation<T1?> continuation,
        T2? expected
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<T1?> Than<T1, T2>(
        this ILessContinuation<T1?> continuation,
        T2? expected,
        string customMessage
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<T1?> Than<T1, T2>(
        this ILessContinuation<T1?> continuation,
        T2? expected,
        Func<string> customMessageGenerator
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue &&
                e.HasValue &&
                TryCompare(a, e) < 0,
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<T1> Than<T1, T2>(
        this ILessContinuation<T1> continuation,
        T2? expected
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<T1> Than<T1, T2>(
        this ILessContinuation<T1> continuation,
        T2? expected,
        string customMessage
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<T1> Than<T1, T2>(
        this ILessContinuation<T1> continuation,
        T2? expected,
        Func<string> customMessageGenerator
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) =>
            {
                var compareResult = TryCompare(a, e);
                return compareResult == null || compareResult < 0;
            },
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<T1?> Than<T1, T2>(
        this ILessContinuation<T1?> continuation,
        T2 expected
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<T1?> Than<T1, T2>(
        this ILessContinuation<T1?> continuation,
        T2 expected,
        string customMessage
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<T1?> Than<T1, T2>(
        this ILessContinuation<T1?> continuation,
        T2 expected,
        Func<string> customMessageGenerator
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue &&
                TryCompare(a.Value, e) < 0,
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<T1> Than<T1, T2>(
        this ILessContinuation<T1> continuation,
        T2 expected
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<T1> Than<T1, T2>(
        this ILessContinuation<T1> continuation,
        T2 expected,
        string customMessage
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<T1> Than<T1, T2>(
        this ILessContinuation<T1> continuation,
        T2 expected,
        Func<string> customMessageGenerator
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) =>
            {
                var compareResult = TryCompare<T1, T2>(a, e);
                return compareResult == null || compareResult < 0;
            },
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static ILessThanContinuation<decimal?> Than(
        this ILessContinuation<decimal?> continuation,
        double expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static ILessThanContinuation<decimal?> Than(
        this ILessContinuation<decimal?> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static ILessThanContinuation<decimal?> Than(
        this ILessContinuation<decimal?> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a < new Decimal(e),
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static ILessThanContinuation<decimal> Than(
        this ILessContinuation<decimal> continuation,
        double expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static ILessThanContinuation<decimal> Than(
        this ILessContinuation<decimal> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static ILessThanContinuation<decimal> Than(
        this ILessContinuation<decimal> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a < new Decimal(e),
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static ILessThanContinuation<double?> Than(
        this ILessContinuation<double?> continuation,
        decimal expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static ILessThanContinuation<double?> Than(
        this ILessContinuation<double?> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static ILessThanContinuation<double?> Than(
        this ILessContinuation<double?> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && new Decimal(a.Value) < e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static ILessThanContinuation<double> Than(
        this ILessContinuation<double> continuation,
        decimal expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static ILessThanContinuation<double> Than(
        this ILessContinuation<double> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static ILessThanContinuation<double> Than(
        this ILessContinuation<double> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => new Decimal(a) < e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<double?> Than(
        this ILessContinuation<double?> continuation,
        long expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<double?> Than(
        this ILessContinuation<double?> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<double?> Than(
        this ILessContinuation<double?> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && new Decimal(a.Value) < e,
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<double> Than(
        this ILessContinuation<double> continuation,
        long expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<double> Than(
        this ILessContinuation<double> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<double> Than(
        this ILessContinuation<double> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => new Decimal(a) < e,
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long?> Than(
        this ILessContinuation<long?> continuation,
        long? expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long?> Than(
        this ILessContinuation<long?> continuation,
        long? expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long?> Than(
        this ILessContinuation<long?> continuation,
        long? expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a < e,
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long> Than(
        this ILessContinuation<long> continuation,
        long expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long> Than(
        this ILessContinuation<long> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long> Than(
        this ILessContinuation<long> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a < e,
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<long?> Than(
        this IGreaterContinuation<long?> continuation,
        long expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long?> Than(
        this IGreaterContinuation<long?> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long?> Than(
        this ILessContinuation<long?> continuation,
        decimal expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long?> Than(
        this ILessContinuation<long?> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long?> Than(
        this ILessContinuation<long?> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a < e,
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long> Than(
        this ILessContinuation<long> continuation,
        decimal expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long> Than(
        this ILessContinuation<long> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long> Than(
        this ILessContinuation<long> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a < e,
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long?> Than(
        this ILessContinuation<long?> continuation,
        double expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long?> Than(
        this ILessContinuation<long?> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long?> Than(
        this ILessContinuation<long?> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a < e,
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long> Than(
        this ILessContinuation<long> continuation,
        double expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long> Than(
        this ILessContinuation<long> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long> Than(
        this ILessContinuation<long> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a < e,
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<DateTime?> Than(
        this ILessContinuation<DateTime?> continuation,
        DateTime expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<DateTime?> Than(
        this ILessContinuation<DateTime?> continuation,
        DateTime expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<DateTime?> Than(
        this ILessContinuation<DateTime?> continuation,
        DateTime expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a < e,
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<DateTime> Than(
        this ILessContinuation<DateTime> continuation,
        DateTime expected
    )
    {
        return continuation.Than(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<DateTime> Than(
        this ILessContinuation<DateTime> continuation,
        DateTime expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<DateTime> Than(
        this ILessContinuation<DateTime> continuation,
        DateTime expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a < e,
            customMessageGenerator);
        return continuation.More();
    }
}