using System;
using System.Collections.Generic;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides matchers for super / subset matching
/// </summary>
public static class SuperSubSetMatchers
{
    /// <summary>
    /// Asserts that the initial collection is a subset of the provided one
    /// </summary>
    /// <param name="subset"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Of<T>(
        this ISubset<T> subset,
        IEnumerable<T> expected
    )
    {
        return subset.Of(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Asserts that the initial collection is a subset of the provided one
    /// </summary>
    /// <param name="subset"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Of<T>(
        this ISubset<T> subset,
        IEnumerable<T> expected,
        string customMessage
    )
    {
        return subset.Of(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Asserts that the initial collection is a subset of the provided one
    /// </summary>
    /// <param name="subset"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Of<T>(
        this ISubset<T> subset,
        IEnumerable<T> expected,
        Func<string> customMessageGenerator
    )
    {
        return subset.AddMatcher(actual =>
        {
            if (actual is null)
            {
                return new EnforcedMatcherResult(
                    false,
                    FinalMessageFor(
                        () => "Cannot assert actual is a subset: it is null",
                        customMessageGenerator
                    )
                );
            }

            if (expected is null)
            {
                return new EnforcedMatcherResult(
                    false,
                    FinalMessageFor(
                        () => "Cannot assert actual is a subset of null",
                        customMessageGenerator
                    )
                );
            }

            var passed = SetContainsAllOf(expected, actual, out var superWasEmpty);
            if (superWasEmpty)
            {
                return new EnforcedMatcherResult(
                    false,
                    FinalMessageFor(
                        () => "Cannot assert actual is a subset of an empty collection",
                        customMessageGenerator
                    )
                );
            }

            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () => $"Expected\n{actual.Stringify()}\nto be a subset of\n{expected.Stringify()}",
                    customMessageGenerator
                )
            );
        });
    }

    /// <summary>
    /// Asserts that the initial collection is a superset of the provided one
    /// </summary>
    /// <param name="superset"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Of<T>(
        this ISuperset<T> superset,
        IEnumerable<T> expected
    )
    {
        return superset.Of(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Asserts that the initial collection is a superset of the provided one
    /// </summary>
    /// <param name="superset"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Of<T>(
        this ISuperset<T> superset,
        IEnumerable<T> expected,
        string customMessage
    )
    {
        return superset.Of(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Asserts that the initial collection is a superset of the provided one
    /// </summary>
    /// <param name="superset"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Of<T>(
        this ISuperset<T> superset,
        IEnumerable<T> expected,
        Func<string> customMessageGenerator
    )
    {
        return superset.AddMatcher(actual =>
        {
            if (actual is null)
            {
                return new EnforcedMatcherResult(
                    false,
                    FinalMessageFor(
                        () => "Cannot assert actual is a superset: it is null",
                        customMessageGenerator
                    )
                );
            }

            if (expected is null)
            {
                return new EnforcedMatcherResult(
                    false,
                    FinalMessageFor(
                        () => "Cannot assert actual is a superset of null",
                        customMessageGenerator
                    )
                );
            }

            var passed = SetContainsAllOf(actual, expected, out var subWasEmpty);
            if (subWasEmpty)
            {
                return new EnforcedMatcherResult(
                    false,
                    FinalMessageFor(
                        () => "Cannot assert actual is a superset of an empty collection",
                        customMessageGenerator
                    )
                );
            }

            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () => $"Expected\n{actual.Stringify()}\nto be a superset of\n{expected.Stringify()}",
                    customMessageGenerator
                )
            );
        });
    }

    private static bool SetContainsAllOf<T>(
        IEnumerable<T> superset,
        IEnumerable<T> subset,
        out bool subWasEmpty
    )
    {
        // TODO: optimise for larger collections
        var super = new HashSet<T>(superset);
        subWasEmpty = true;
        foreach (var sub in subset)
        {
            subWasEmpty = false;
            if (!super.Contains(sub))
            {
                return false;
            }
        }

        return true;
    }
}