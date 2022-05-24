using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.GreaterAndLessMatchersCommon;

namespace NExpect;

/// <summary>
/// Provides the .Greater.Than.Or.Equal.To matchers
/// </summary>
public static class GreaterThanOrEqualMatchers
{
    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<DateTime> To(
        this IGreaterThanOrEqual<DateTime> continuation,
        DateTime expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<DateTime> To(
        this IGreaterThanOrEqual<DateTime> continuation,
        DateTime expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<DateTime> To(
        this IGreaterThanOrEqual<DateTime> continuation,
        DateTime expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a >= e,
            customMessageGenerator);
        return continuation.More();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<DateTime?> To(
        this IGreaterThanOrEqual<DateTime?> continuation,
        DateTime expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<DateTime?> To(
        this IGreaterThanOrEqual<DateTime?> continuation,
        DateTime expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<DateTime?> To(
        this IGreaterThanOrEqual<DateTime?> continuation,
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
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<decimal?> To(
        this IGreaterContinuation<decimal?> continuation,
        decimal expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal?> To(
        this IGreaterContinuation<decimal?> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal?> To(
        this IGreaterContinuation<decimal?> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a > e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<decimal> To(
        this IGreaterContinuation<decimal> continuation,
        decimal expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal> To(
        this IGreaterContinuation<decimal> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal> To(
        this IGreaterContinuation<decimal> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a > e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long?> To(
        this IGreaterThanOrEqual<long?> continuation,
        double expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long?> To(
        this IGreaterThanOrEqual<long?> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long?> To(
        this IGreaterThanOrEqual<long?> continuation,
        double expected,
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
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<long> To(
        this IGreaterThanOrEqual<long> continuation,
        double expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long> To(
        this IGreaterThanOrEqual<long> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long> To(
        this IGreaterThanOrEqual<long> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a >= e,
            customMessageGenerator
        );
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<long?> To(
        this IGreaterThanOrEqual<long?> continuation,
        long expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long?> To(
        this IGreaterThanOrEqual<long?> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long?> To(
        this IGreaterThanOrEqual<long?> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a >= e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<long> To(
        this IGreaterThanOrEqual<long> continuation,
        long expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long> To(
        this IGreaterThanOrEqual<long> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long> To(
        this IGreaterThanOrEqual<long> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a >= e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<T1> To<T1, T2>(
        this IGreaterThanOrEqual<T1> continuation,
        T2 expected
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1> To<T1, T2>(
        this IGreaterThanOrEqual<T1> continuation,
        T2 expected,
        string customMessage
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1> To<T1, T2>(
        this IGreaterThanOrEqual<T1> continuation,
        T2 expected,
        Func<string> customMessageGenerator
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => TryCompare(a, e) > -1,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<T1?> To<T1, T2>(
        this IGreaterThanOrEqual<T1?> continuation,
        T2 expected
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1?> To<T1, T2>(
        this IGreaterThanOrEqual<T1?> continuation,
        T2 expected,
        string customMessage
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1?> To<T1, T2>(
        this IGreaterThanOrEqual<T1?> continuation,
        T2 expected,
        Func<string> customMessageGenerator
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => TryCompare(a, e) > -1,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<T1> To<T1, T2>(
        this IGreaterThanOrEqual<T1> continuation,
        T2? expected
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1> To<T1, T2>(
        this IGreaterThanOrEqual<T1> continuation,
        T2? expected,
        string customMessage
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1> To<T1, T2>(
        this IGreaterThanOrEqual<T1> continuation,
        T2? expected,
        Func<string> customMessageGenerator
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => TryCompare(a, e) > -1,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<decimal?> To(
        this IGreaterThanOrEqual<decimal?> continuation,
        decimal expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal?> To(
        this IGreaterThanOrEqual<decimal?> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal?> To(
        this IGreaterThanOrEqual<decimal?> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a >= e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<decimal> To(
        this IGreaterThanOrEqual<decimal> continuation,
        decimal expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal> To(
        this IGreaterThanOrEqual<decimal> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Least</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal> To(
        this IGreaterThanOrEqual<decimal> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a >= e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<long?> To(
        this IGreaterThanOrEqual<long?> continuation,
        decimal expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long?> To(
        this IGreaterThanOrEqual<long?> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long?> To(
        this IGreaterThanOrEqual<long?> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a >= e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<long> To(
        this IGreaterThanOrEqual<long> continuation,
        decimal expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long> To(
        this IGreaterThanOrEqual<long> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long> To(
        this IGreaterThanOrEqual<long> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a >= e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<double?> To(
        this IGreaterThanOrEqual<double?> continuation,
        long expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double?> To(
        this IGreaterThanOrEqual<double?> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double?> To(
        this IGreaterThanOrEqual<double?> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && new Decimal(a.Value) >= e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<double?> To(
        this IGreaterThanOrEqual<double?> continuation,
        decimal expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double?> To(
        this IGreaterThanOrEqual<double?> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double?> To(
        this IGreaterThanOrEqual<double?> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && new Decimal(a.Value) >= e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<double> To(
        this IGreaterThanOrEqual<double> continuation,
        decimal expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double> To(
        this IGreaterThanOrEqual<double> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double> To(
        this IGreaterThanOrEqual<double> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => new Decimal(a) >= e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<double?> To(
        this IGreaterThanOrEqual<double?> continuation,
        double expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double?> To(
        this IGreaterThanOrEqual<double?> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double?> To(
        this IGreaterThanOrEqual<double?> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a >= e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<double> To(
        this IGreaterThanOrEqual<double> continuation,
        double expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double> To(
        this IGreaterThanOrEqual<double> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double> To(
        this IGreaterThanOrEqual<double> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        AddMatcher(
            continuation,
            expected,
            (a, e) => a >= e,
            customMessageGenerator);
        return continuation.Continue();
    }
}