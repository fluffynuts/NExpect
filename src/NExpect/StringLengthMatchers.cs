using System;
using Imported.PeanutButter.Utils;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides matchers for strings to
/// neatly assert that one string is
/// longer than or shorter than another
/// </summary>
public static class StringLengthMatchers
{
    /// <summary>
    /// Asserts that the provided string
    /// is shorter than the other one
    /// </summary>
    /// <param name="shorter"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static IMore<string> Than(
        this IStringShorter shorter,
        string other
    )
    {
        return shorter.Than(
            other,
            NULL_STRING
        );
    }

    /// <summary>
    /// Asserts that the provided string
    /// is shorter than the other one
    /// </summary>
    /// <param name="shorter"></param>
    /// <param name="other"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<string> Than(
        this IStringShorter shorter,
        string other,
        string customMessage
    )
    {
        return shorter.Than(
            other,
            () => customMessage
        );
    }

    /// <summary>
    /// Asserts that the provided string
    /// is shorter than the other one
    /// </summary>
    /// <param name="shorter"></param>
    /// <param name="other"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<string> Than(
        this IStringShorter shorter,
        string other,
        Func<string> customMessageGenerator
    )
    {
        return shorter.AddMatcher(
            actual =>
            {
                if (actual is null || other is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        FinalMessageFor(
                            () => "Cannot compare string lengths when one or more are null",
                            customMessageGenerator
                        )
                    );
                }

                var passed = actual.Length < other.Length;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected '{actual}' {passed.AsNot()}to be shorter than '{other}'",
                        customMessageGenerator
                    )
                );
            }
        );
    }
    
    
    /// <summary>
    /// Asserts that the provided string
    /// is longer than the other one
    /// </summary>
    /// <param name="longer"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static IMore<string> Than(
        this IStringLonger longer,
        string other
    )
    {
        return longer.Than(
            other,
            NULL_STRING
        );
    }

    /// <summary>
    /// Asserts that the provided string
    /// is longer than the other one
    /// </summary>
    /// <param name="longer"></param>
    /// <param name="other"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<string> Than(
        this IStringLonger longer,
        string other,
        string customMessage
    )
    {
        return longer.Than(
            other,
            () => customMessage
        );
    }

    /// <summary>
    /// Asserts that the provided string
    /// is longer than the other one
    /// </summary>
    /// <param name="longer"></param>
    /// <param name="other"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<string> Than(
        this IStringLonger longer,
        string other,
        Func<string> customMessageGenerator
    )
    {
        return longer.AddMatcher(
            actual =>
            {
                if (actual is null || other is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        FinalMessageFor(
                            () => "Cannot compare string lengths when one or more are null",
                            customMessageGenerator
                        )
                    );
                }

                var passed = actual.Length > other.Length;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected '{actual}' {passed.AsNot()}to be longer than '{other}'",
                        customMessageGenerator
                    )
                );
            }
        );
    }
}