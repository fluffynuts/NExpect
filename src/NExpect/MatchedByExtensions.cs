using System;
using System.Linq.Expressions;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides the .Matched.By grammar
/// </summary>
public static class MatchedByExtensions
{
    /// <summary>
    /// Tests if the value T matches expectations outlined in a func
    /// </summary>
    /// <param name="matched"></param>
    /// <param name="matcher"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> By<T>(
        this IMatched<T> matched,
        Func<T, bool> matcher
    )
    {
        return matched.By(
            matcher,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests if the value T matches expectations outlined in a func
    /// </summary>
    /// <param name="matched"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> By<T>(
        this IMatched<T> matched,
        Func<T, bool> matcher,
        string customMessage
    )
    {
        return matched.By(
            matcher,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if the value T matches expectations outlined in a func
    /// </summary>
    /// <param name="matched"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> By<T>(
        this IMatched<T> matched,
        Func<T, bool> matcher,
        Func<string> customMessageGenerator
    )
    {
        return matched.AddMatcher(actual =>
        {
            var passed = matcher(actual);
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () => $"Expected to match\n{actual.Stringify()}\nwith\n{matcher}",
                    customMessageGenerator
                )
            );
        });
    }
}