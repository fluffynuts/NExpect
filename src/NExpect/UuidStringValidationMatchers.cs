using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Some matchers to validate that strings are GUID/UUID values
/// </summary>
public static class UuidStringValidationMatchers
{
    /// <summary>
    /// Verifies whether or not the string under test is a valid Guid
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static IMore<string> Guid(
        this IA<string> a
    )
    {
        return a.Guid(NULL_STRING);
    }

    /// <summary>
    /// Verifies whether or not the string under test is a valid Guid
    /// </summary>
    /// <param name="a"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<string> Guid(
        this IA<string> a,
        string customMessage
    )
    {
        return a.Guid(() => customMessage);
    }

    /// <summary>
    /// Verifies whether or not the string under test is a valid Guid
    /// </summary>
    /// <param name="a"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<string> Guid(
        this IA<string> a,
        Func<string> customMessageGenerator
    )
    {
        return a.AddMatcher(
            actual => ValidateIsUUID(actual, customMessageGenerator)
        );
    }

    /// <summary>
    /// Verifies whether or not the string under test is a valid Uuid
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static IMore<string> Uuid(
        this IAn<string> a
    )
    {
        return a.Uuid(NULL_STRING);
    }

    /// <summary>
    /// Verifies whether or not the string under test is a valid Uuid
    /// </summary>
    /// <param name="a"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<string> Uuid(
        this IAn<string> a,
        string customMessage
    )
    {
        return a.Uuid(() => customMessage);
    }

    /// <summary>
    /// Verifies whether or not the string under test is a valid Uuid
    /// </summary>
    /// <param name="a"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<string> Uuid(
        this IAn<string> a,
        Func<string> customMessageGenerator
    )
    {
        return a.AddMatcher(
            actual => ValidateIsUUID(actual, customMessageGenerator)
        );
    }

    private static IMatcherResult ValidateIsUUID(
        string str,
        Func<string> customMessageGenerator,
        [CallerMemberName] string caller = null)
    {
        var passed = UuidMatcher.IsMatch(str);
        return new MatcherResult(
            passed,
            FinalMessageFor(
                () => $"Expected '{str}' {passed.AsNot()}to be a {caller}",
                customMessageGenerator
            )
        );
    }


    private static readonly Regex UuidMatcher = new Regex(
        "^[0-9a-fA-F]{8}\\b-[0-9a-fA-F]{4}\\b-[0-9a-fA-F]{4}\\b-[0-9a-fA-F]{4}\\b-[0-9a-fA-F]{12}$"
    );
}