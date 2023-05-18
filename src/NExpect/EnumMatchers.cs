using System;
using Imported.PeanutButter.Utils;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides matchers for enum values
/// </summary>
public static class EnumMatchers
{
    /// <summary>
    /// Verifies that the [Flag]-decorated enum
    /// has the provided flag
    /// </summary>
    /// <param name="have"></param>
    /// <param name="flag">The flag being tested for</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Flag<T>(
        this IHave<T> have,
        T flag
    ) where T : struct
    {
        return have.Flag(
            flag,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verifies that the [Flag]-decorated enum
    /// has the provided flag
    /// </summary>
    /// <param name="have"></param>
    /// <param name="flag">The flag being tested for</param>
    /// <param name="customMessage">A custom error message</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Flag<T>(
        this IHave<T> have,
        T flag,
        string customMessage
    ) where T : struct
    {
        return have.Flag(
            flag,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that the [Flag]-decorated enum
    /// has the provided flag
    /// </summary>
    /// <param name="have"></param>
    /// <param name="flag">The flag being tested for</param>
    /// <param name="customMessageGenerator">Generates a custom error message</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Flag<T>(
        this IHave<T> have,
        T flag,
        Func<string> customMessageGenerator
    ) where T : struct
    {
        return have.AddMatcher(
            actual =>
            {
                if (!typeof(T).HasAttribute<FlagsAttribute>())
                {
                    return new EnforcedMatcherResult(
                        false,
                        FinalMessageFor(
                            () =>
                                $"{typeof(T)} is not decorated with [Flags]. Are you sure you should be using it like a Flags type?",
                            customMessageGenerator
                        )
                    );
                }

                var passed = actual.HasFlag(flag);

                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected ({actual}) {passed.AsNot()}to have flag ({flag})",
                        customMessageGenerator
                    )
                );
            }
        );
    }
}