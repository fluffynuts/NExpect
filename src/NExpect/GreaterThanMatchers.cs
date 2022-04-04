using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.GreaterAndLessMatchersCommon;

namespace NExpect;

/// <summary>
/// Provides the .Greater.Than matchers
/// </summary>
public static class GreaterThanMatchers
{
    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<T1?> Than<T1, T2>(
        this IGreaterContinuation<T1?> continuation,
        T2? expected
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1?> Than<T1, T2>(
        this IGreaterContinuation<T1?> continuation,
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
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1?> Than<T1, T2>(
        this IGreaterContinuation<T1?> continuation,
        T2? expected,
        Func<string> customMessageGenerator
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue &&
                e.HasValue &&
                a.Value.CompareTo(e.Value) > 0,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<T1> Than<T1, T2>(
        this IGreaterContinuation<T1> continuation,
        T2? expected
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1> Than<T1, T2>(
        this IGreaterContinuation<T1> continuation,
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
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1> Than<T1, T2>(
        this IGreaterContinuation<T1> continuation,
        T2? expected,
        Func<string> customMessageGenerator
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) =>
            {
                var compareResult = TryCompare(a, e);
                return compareResult == null || compareResult > 0;
            },
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<T1?> Than<T1, T2>(
        this IGreaterContinuation<T1?> continuation,
        T2 expected
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1?> Than<T1, T2>(
        this IGreaterContinuation<T1?> continuation,
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
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1?> Than<T1, T2>(
        this IGreaterContinuation<T1?> continuation,
        T2 expected,
        Func<string> customMessageGenerator
    )
        where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) =>
            {
                var compareResult = TryCompare(a, e);
                return compareResult == null || compareResult > 0;
            },
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<T1> Than<T1, T2>(
        this IGreaterContinuation<T1> continuation,
        T2 expected
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1> Than<T1, T2>(
        this IGreaterContinuation<T1> continuation,
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
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<T1> Than<T1, T2>(
        this IGreaterContinuation<T1> continuation,
        T2 expected,
        Func<string> customMessageGenerator
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => GreaterAndLessMatchersCommon.TryCompare<T1, T2>(a, e) > 0,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<decimal?> Than(
        this IGreaterContinuation<decimal?> continuation,
        double expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal?> Than(
        this IGreaterContinuation<decimal?> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal?> Than(
        this IGreaterContinuation<decimal?> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a > new Decimal(e),
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<decimal> Than(
        this IGreaterContinuation<decimal> continuation,
        double expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal> Than(
        this IGreaterContinuation<decimal> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal> Than(
        this IGreaterContinuation<decimal> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a > new Decimal(e),
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    public static ILessThanContinuation<decimal?> Than(
        this ILessContinuation<decimal?> continuation,
        long expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static ILessThanContinuation<decimal?> Than(
        this ILessContinuation<decimal?> continuation,
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
    public static ILessThanContinuation<decimal?> Than(
        this ILessContinuation<decimal?> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
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
        long expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static ILessThanContinuation<decimal> Than(
        this ILessContinuation<decimal> continuation,
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
    public static ILessThanContinuation<decimal> Than(
        this ILessContinuation<decimal> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a < new Decimal(e),
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<decimal?> Than(
        this IGreaterContinuation<decimal?> continuation,
        long expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal?> Than(
        this IGreaterContinuation<decimal?> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal?> Than(
        this IGreaterContinuation<decimal?> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a > new Decimal(e),
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<decimal> Than(
        this IGreaterContinuation<decimal> continuation,
        long expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal> Than(
        this IGreaterContinuation<decimal> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<decimal> Than(
        this IGreaterContinuation<decimal> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a > new Decimal(e),
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
        double expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static ILessThanContinuation<double?> Than(
        this ILessContinuation<double?> continuation,
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
    /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
    public static ILessThanContinuation<double?> Than(
        this ILessContinuation<double?> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a < e,
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
        double expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Less</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static ILessThanContinuation<double> Than(
        this ILessContinuation<double> continuation,
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
    /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
    public static ILessThanContinuation<double> Than(
        this ILessContinuation<double> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a < e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<double?> Than(
        this IGreaterContinuation<double?> continuation,
        double expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double?> Than(
        this IGreaterContinuation<double?> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double?> Than(
        this IGreaterContinuation<double?> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a > e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<double> Than(
        this IGreaterContinuation<double> continuation,
        double expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double> Than(
        this IGreaterContinuation<double> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double> Than(
        this IGreaterContinuation<double> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a > e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<double?> Than(
        this IGreaterContinuation<double?> continuation,
        decimal expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double?> Than(
        this IGreaterContinuation<double?> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double?> Than(
        this IGreaterContinuation<double?> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(continuation,
            expected,
            (a, e) => a.HasValue && new Decimal(a.Value) > e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<double> Than(
        this IGreaterContinuation<double> continuation,
        decimal expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double> Than(
        this IGreaterContinuation<double> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double> Than(
        this IGreaterContinuation<double> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(continuation,
            expected,
            (a, e) => new Decimal(a) > e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<double?> Than(
        this IGreaterContinuation<double?> continuation,
        long expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double?> Than(
        this IGreaterContinuation<double?> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double?> Than(
        this IGreaterContinuation<double?> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && new Decimal(a.Value) > e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<double> Than(
        this IGreaterContinuation<double> continuation,
        long expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double> Than(
        this IGreaterContinuation<double> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<double> Than(
        this IGreaterContinuation<double> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => new Decimal(a) > e,
            customMessageGenerator);
        return continuation.Continue();
    }
        
    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long?> Than(
        this IGreaterContinuation<long?> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a > e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<long> Than(
        this IGreaterContinuation<long> continuation,
        long expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long> Than(
        this IGreaterContinuation<long> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long> Than(
        this IGreaterContinuation<long> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a > e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<long?> Than(
        this IGreaterContinuation<long?> continuation,
        decimal expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long?> Than(
        this IGreaterContinuation<long?> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long?> Than(
        this IGreaterContinuation<long?> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a > e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<long> Than(
        this IGreaterContinuation<long> continuation,
        decimal expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long> Than(
        this IGreaterContinuation<long> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long> Than(
        this IGreaterContinuation<long> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a > e,
            customMessageGenerator);
        return continuation.Continue();
    }
    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<long?> Than(
        this IGreaterContinuation<long?> continuation,
        double expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long?> Than(
        this IGreaterContinuation<long?> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long?> Than(
        this IGreaterContinuation<long?> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
            continuation,
            expected,
            (a, e) => a.HasValue && a > e,
            customMessageGenerator);
        return continuation.Continue();
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    public static IGreaterThanContinuation<long> Than(
        this IGreaterContinuation<long> continuation,
        double expected
    )
    {
        return continuation.Than(expected, MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long> Than(
        this IGreaterContinuation<long> continuation,
        double expected,
        string customMessage
    )
    {
        return continuation.Than(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.Greater</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IGreaterThanContinuation<long> Than(
        this IGreaterContinuation<long> continuation,
        double expected,
        Func<string> customMessageGenerator
    )
    {
        GreaterAndLessMatchersCommon.AddMatcher(
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
        public static IGreaterThanContinuation<DateTime?> Than(
            this IGreaterContinuation<DateTime?> continuation,
            DateTime expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<DateTime?> Than(
            this IGreaterContinuation<DateTime?> continuation,
            DateTime expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<DateTime?> Than(
            this IGreaterContinuation<DateTime?> continuation,
            DateTime expected,
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
        public static IGreaterThanContinuation<DateTime> Than(
            this IGreaterContinuation<DateTime> continuation,
            DateTime expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<DateTime> Than(
            this IGreaterContinuation<DateTime> continuation,
            DateTime expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<DateTime> Than(
            this IGreaterContinuation<DateTime> continuation,
            DateTime expected,
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

}