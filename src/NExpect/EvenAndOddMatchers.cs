using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides convenience matchers to assert that
/// numbers are odd or even
/// </summary>
public static class EvenAndOddMatchers
{
    /// <summary>
    /// Verifies that the value is even
    /// </summary>
    /// <param name="be"></param>
    /// <returns></returns>
    public static IMore<long> Even(
        this IBe<long> be
    )
    {
        return be.Even(NULL_STRING);
    }

    /// <summary>
    /// Verifies that the value is even
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<long> Even(
        this IBe<long> be,
        string customMessage
    )
    {
        return be.Even(() => customMessage);
    }

    /// <summary>
    /// Verifies that the value is even
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<long> Even(
        this IBe<long> be,
        Func<string> customMessageGenerator
    )
    {
        return be.AddMatcher(
            actual =>
            {
                var passed = actual % 2 == 0;
                return new MatcherResult(
                    passed,
                    () => $"Expected {actual} {passed.AsNot()}to be even",
                    customMessageGenerator
                );
            }
        );
    }
    /// <summary>
    /// Verifies that the value is even
    /// </summary>
    /// <param name="be"></param>
    /// <returns></returns>
    public static IMore<long> Odd(
        this IBe<long> be
    )
    {
        return be.Odd(NULL_STRING);
    }

    /// <summary>
    /// Verifies that the value is even
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<long> Odd(
        this IBe<long> be,
        string customMessage
    )
    {
        return be.Odd(() => customMessage);
    }

    /// <summary>
    /// Verifies that the value is even
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<long> Odd(
        this IBe<long> be,
        Func<string> customMessageGenerator
    )
    {
        return be.AddMatcher(
            actual =>
            {
                var passed = actual % 2 != 0;
                return new MatcherResult(
                    passed,
                    () => $"Expected {actual} {passed.AsNot()}to be even",
                    customMessageGenerator
                );
            }
        );
    }
}