using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides matchers dangling on IMore&lt;T&gt;
/// </summary>
public static class MoreMatchers
{
    /// <summary>
    /// Provides the .With extension on .And allowing
    /// for easy validation of the actual value.
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <returns></returns>
    public static IMore<T> With<T>(
        this IAnd<T> and,
        Func<T, bool> matcher
    )
    {
        return and.With(matcher, NULL_STRING);
    }

    /// <summary>
    /// Provides the .With extension on .And allowing
    /// for easy validation of the actual value.
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<T> With<T>(
        this IAnd<T> and,
        Func<T, bool> matcher,
        string customMessage
    )
    {
        return and.With(matcher, () => customMessage);
    }

    /// <summary>
    /// Provides the .With extension on .And allowing
    /// for easy validation of the actual value.
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<T> With<T>(
        this IAnd<T> and,
        Func<T, bool> matcher,
        Func<string> customMessageGenerator
    )
    {
        var canAddMatcher = and as ICanAddMatcher<T>;
        return RunMatcherOn(
            "IAnd<T>",
            canAddMatcher,
            matcher,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Provides the .With extension on .More allowing
    /// for easy validation of the actual value.
    /// </summary>
    /// <param name="more"></param>
    /// <param name="matcher"></param>
    /// <returns></returns>
    public static IMore<T> With<T>(
        this IMore<T> more,
        Func<T, bool> matcher
    )
    {
        return more.With(matcher, NULL_STRING);
    }

    /// <summary>
    /// Provides the .With extension on .More allowing
    /// for easy validation of the actual value.
    /// </summary>
    /// <param name="more"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<T> With<T>(
        this IMore<T> more,
        Func<T, bool> matcher,
        string customMessage
    )
    {
        return more.With(matcher, () => customMessage);
    }

    /// <summary>
    /// Provides the .With extension on .More allowing
    /// for easy validation of the actual value.
    /// </summary>
    /// <param name="more"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<T> With<T>(
        this IMore<T> more,
        Func<T, bool> matcher,
        Func<string> customMessageGenerator
    )
    {
        var canAddMatcher = more as ICanAddMatcher<T>;
        return RunMatcherOn("IMore<T>", canAddMatcher, matcher, customMessageGenerator);
    }

    /// <summary>
    /// Provides the .With extension on .And allowing
    /// for easy validation of the actual value.
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <returns></returns>
    public static IMore<T> With<T>(
        this IAnd<T> and,
        Func<T, IMatcherResult> matcher
    )
    {
        return and.With(matcher, NULL_STRING);
    }

    /// <summary>
    /// Provides the .With extension on .And allowing
    /// for easy validation of the actual value.
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<T> With<T>(
        this IAnd<T> and,
        Func<T, IMatcherResult> matcher,
        string customMessage
    )
    {
        return and.With(matcher, () => customMessage);
    }

    /// <summary>
    /// Provides the .With extension on .And allowing
    /// for easy validation of the actual value.
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<T> With<T>(
        this IAnd<T> and,
        Func<T, IMatcherResult> matcher,
        Func<string> customMessageGenerator
    )
    {
        var canAddMatcher = and as ICanAddMatcher<T>;
        return RunMatcherOn(
            "IAnd<T>",
            canAddMatcher,
            matcher,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Provides the .With extension on .More allowing
    /// for easy validation of the actual value.
    /// </summary>
    /// <param name="more"></param>
    /// <param name="matcher"></param>
    /// <returns></returns>
    public static IMore<T> With<T>(
        this IMore<T> more,
        Func<T, IMatcherResult> matcher
    )
    {
        return more.With(matcher, NULL_STRING);
    }

    /// <summary>
    /// Provides the .With extension on .More allowing
    /// for easy validation of the actual value.
    /// </summary>
    /// <param name="more"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<T> With<T>(
        this IMore<T> more,
        Func<T, IMatcherResult> matcher,
        string customMessage
    )
    {
        return more.With(matcher, () => customMessage);
    }

    /// <summary>
    /// Provides the .With extension on .More allowing
    /// for easy validation of the actual value.
    /// </summary>
    /// <param name="more"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<T> With<T>(
        this IMore<T> more,
        Func<T, IMatcherResult> matcher,
        Func<string> customMessageGenerator
    )
    {
        var canAddMatcher = more as ICanAddMatcher<T>;
        return RunMatcherOn("IMore<T>", canAddMatcher, matcher, customMessageGenerator);
    }

    private static IMore<T> RunMatcherOn<T>(
        string sourceTypeName,
        ICanAddMatcher<T> canAddMatcher,
        Func<T, bool> matcher,
        Func<string> customMessageGenerator
    )
    {
        return RunMatcherOn(
            sourceTypeName,
            canAddMatcher,
            actual =>
            {
                var passed = matcher(actual);
                return new MatcherResult(
                    passed,
                    () => $"Expected custom matcher {passed.AsNot()}to pass"
                );
            },
            customMessageGenerator
        );
    }

    private static IMore<T> RunMatcherOn<T>(
        string sourceTypeName,
        ICanAddMatcher<T> canAddMatcher,
        Func<T, IMatcherResult> matcher,
        Func<string> customMessageGenerator
    )
    {
        if (canAddMatcher is null)
        {
            throw new NotSupportedException(
                $"Implementations of {sourceTypeName} must also implement ICanAddMatcher<T> to use the .With extension method"
            );
        }

        return canAddMatcher.AddMatcher(actual =>
        {
            try
            {
                var result = matcher(actual);
                return new MatcherResult(
                    result.Passed,
                    FinalMessageFor(
                        () => result.Message,
                        customMessageGenerator
                    )
                );
            }
            catch (Exception ex)
            {
                return new EnforcedMatcherResult(
                    false,
                    FinalMessageFor(
                        () => $"Failed to run matcher: ${ex.Message}",
                        customMessageGenerator
                    ),
                    ex
                );
            }
        });
    }
}