using System;
using Microsoft.AspNetCore.Mvc;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// 
/// </summary>
public static class PartialViewResultNameMatchers
{
    /// <summary>
    /// Asserts that the action result is a partial view result
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> PartialView(
        this IA<ActionResult> a
    )
    {
        PartialViewResult partialViewResult = null;
        a.AddMatcher(actual =>
        {
            partialViewResult = actual as PartialViewResult;
            var passed = partialViewResult is not null;
            return new MatcherResult(
                passed,
                () =>
                    actual is null
                        ? $"Expected a partial view result, but received NULL"
                        : $"Expected action result of type {actual.GetType()} to be a PartialViewResult"
            );
        });
        return new More<PartialViewResult>(() => partialViewResult);
    }

    /// <summary>
    /// Asserts that the partial view result has the expected view name
    /// </summary>
    /// <param name="with"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Name(
        this IWith<PartialViewResult> with,
        string expected
    )
    {
        return with.Name(expected, NULL_STRING);
    }

    /// <summary>
    /// Asserts that the partial view result has the expected view name
    /// </summary>
    /// <param name="with"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Name(
        this IWith<PartialViewResult> with,
        string expected,
        string customMessage
    )
    {
        return with.Name(expected, () => customMessage);
    }

    /// <summary>
    /// Asserts that the partial view result has the expected view name
    /// </summary>
    /// <param name="with"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Name(
        this IWith<PartialViewResult> with,
        string expected,
        Func<string> customMessageGenerator
    )
    {
        return with.AddMatcher(actual =>
        {
            if (expected is null)
            {
                return new EnforcedMatcherResult(false, "Expected view name cannot be null");
            }

            var passed = expected.Equals(actual?.ViewName, StringComparison.OrdinalIgnoreCase);
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () => $"Expected view name '{expected}' but received '{actual?.ViewName ?? "(no view result)"}",
                    customMessageGenerator
                )
            );
        });
    }
}