using System;
using NExpect.Interfaces;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.GreaterAndLessMatchersCommon;

namespace NExpect;

/// <summary>
/// Provides the .At.Least matchers
/// (alternative syntax for .Less.Than.Or.Equal.To)
/// </summary>
public static class AtMostMatchers
{
    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">At.Most</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<decimal> Most(
        this IAt<decimal> continuation,
        decimal expected
    )
    {
        return continuation.Most(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">At.Most</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<decimal> Most(
        this IAt<decimal> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Most(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">At.Most</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<decimal> Most(
        this IAt<decimal> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a <= e,
            customMessageGenerator);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Most</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<double> Most(
        this IAt<double> continuation,
        decimal expected
    )
    {
        return continuation.Most(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Most</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<double> Most(
        this IAt<double> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Most(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Most</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<double> Most(
        this IAt<double> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => new Decimal(a) <= e,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Most</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<T1> Most<T1, T2>(
        this IAt<T1> continuation,
        T2 expected
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        return continuation.Most(expected, NULL_STRING);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Most</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<T1> Most<T1, T2>(
        this IAt<T1> continuation,
        T2 expected,
        string customMessage
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        return continuation.Most(expected, () => customMessage);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">.At.Most</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<T1> Most<T1, T2>(
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
            (a, e) => TryCompare(a, e) < 1,
            customMessageGenerator);
    }

    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">At.Most</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long> Most(
        this IAt<long> continuation,
        long expected
    )
    {
        return continuation.Most(expected, NULL_STRING);
    }
    
    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">At.Most</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long> Most(
        this IAt<long> continuation,
        long expected,
        string customMessage
    )
    {
        return continuation.Most(expected, () => customMessage);
    }
    
    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">At.Most</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long> Most(
        this IAt<long> continuation,
        long expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a <= e,
            customMessageGenerator);
    }
    
    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">At.Most</param>
    /// <param name="expected">value to compare with</param>
    public static IMore<long> Most(
        this IAt<long> continuation,
        decimal expected
    )
    {
        return continuation.Most(expected, NULL_STRING);
    }
    
    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">At.Most</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IMore<long> Most(
        this IAt<long> continuation,
        decimal expected,
        string customMessage
    )
    {
        return continuation.Most(expected, () => customMessage);
    }
    
    /// <summary>
    /// Compares two values
    /// </summary>
    /// <param name="continuation">At.Most</param>
    /// <param name="expected">value to compare with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IMore<long> Most(
        this IAt<long> continuation,
        decimal expected,
        Func<string> customMessageGenerator
    )
    {
        return AddMatcher(
            continuation,
            expected,
            (a, e) => a <= e,
            customMessageGenerator);
    }
}