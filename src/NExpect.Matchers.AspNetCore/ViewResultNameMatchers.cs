using System;
using Microsoft.AspNetCore.Mvc;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides matchers for fluently asserting view names on action results which are
/// in fact view results
/// </summary>
public static class ViewResultNameMatchers
{
    /// <summary>
    /// Asserts that the provided action result is a view result
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static IMore<ViewResult> View<TResult>(
        this IA<TResult> a
    ) where TResult: IActionResult
    {
        return a.View(NULL_STRING);
    }

    /// <summary>
    /// Asserts that the provided action result is a view result
    /// </summary>
    /// <param name="a"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<ViewResult> View<TResult>(
        this IA<TResult> a,
        string customMessage
    ) where TResult: IActionResult
    {
        return a.View(() => customMessage);
    }

    /// <summary>
    /// Asserts that the provided action result is a view result
    /// </summary>
    /// <param name="a"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<ViewResult> View<TResult>(
        this IA<TResult> a,
        Func<string> customMessageGenerator
    ) where TResult: IActionResult
    {
        ViewResult viewResult = null;
        a.AddMatcher(actual =>
        {
            viewResult = actual as ViewResult;
            var passed = viewResult is not null;
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () =>
                        actual is null
                            ? $"Expected a view result, but received NULL"
                            : $"Expected action result of type {actual.GetType()} to be a ViewResult",
                    customMessageGenerator
                )
            );
        });
        return new More<ViewResult>(() => viewResult);
    }

    /// <summary>
    /// Asserts that the name on the view result is the expected name
    /// </summary>
    /// <param name="with"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Name(
        this IWith<ViewResult> with,
        string expected
    )
    {
        return AddNameMatcher(with, expected);
    }

    /// <summary>
    /// Asserts that the name on the view result is the expected name
    /// </summary>
    /// <param name="with"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Name(
        this IAnd<ViewResult> with,
        string expected
    )
    {
        return AddNameMatcher(with, expected);
    }

    private static IMore<ViewResult> AddNameMatcher(
        ICanAddMatcher<ViewResult> addMatcher,
        string expected
    )
    {
        return addMatcher.AddMatcher(actual =>
        {
            if (expected is null)
            {
                return new EnforcedMatcherResult(false, "Expected view name cannot be null");
            }

            var passed = expected.Equals(actual?.ViewName, StringComparison.OrdinalIgnoreCase);
            return new MatcherResult(
                passed,
                () => $"Expected view name '{expected}' but received '{actual?.ViewName ?? "(no view result)"}"
            );
        });
    }
}