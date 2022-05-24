using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable HeapView.BoxingAllocation
// ReSharper disable UnusedMember.Global

namespace NExpect;

/// <summary>
/// Provides matchers to test True / False
/// </summary>
public static class TruthMatchers
{
    /// <summary>
    /// Tests if a boolean value is True
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    public static IMore<bool> True(
        this IBe<bool> continuation)
    {
        return continuation.True(NULL_STRING);
    }

    /// <summary>
    /// Tests if a boolean value is True
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="message">Custom message to include on failure</param>
    public static IMore<bool> True(
        this IBe<bool> continuation,
        string message)
    {
        return continuation.True(() => message);
    }

    /// <summary>
    /// Tests if a boolean value is True
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="customMessageGenerator">Generates a custom message to include on failure</param>
    public static IMore<bool> True(
        this IBe<bool> continuation,
        Func<string> customMessageGenerator
    )
    {
        return TestBoolean(continuation, true, customMessageGenerator);
    }

    /// <summary>
    /// Tests if a boolean value is True
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    public static IMore<bool?> True(this IBe<bool?> continuation)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        return continuation.True(null as string);
    }

    /// <summary>
    /// Tests if a boolean value is True
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="message">Custom message to include on failure</param>
    public static IMore<bool?> True(this IBe<bool?> continuation, string message)
    {
        return continuation.True(() => message);
    }

    /// <summary>
    /// Tests if a boolean value is True
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="customMessageGenerator">Generates a custom message to include on failure</param>
    public static IMore<bool?> True(
        this IBe<bool?> continuation,
        Func<string> customMessageGenerator
    )
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        return continuation.AddMatcher(TruthTestFor(true as bool?, customMessageGenerator));
    }

    /// <summary>
    /// Tests if a boolean value is False
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    public static IMore<bool> False(this IBe<bool> continuation)
    {
        return continuation.AddMatcher(TruthTestFor(false, null));
    }

    /// <summary>
    /// Tests if a boolean value is False
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="message">Custom message to include on failure</param>
    public static IMore<bool> False(
        this IBe<bool> continuation,
        string message)
    {
        return continuation.False(() => message);
    }

    /// <summary>
    /// Tests if a boolean value is False
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="customMessageGenerator">Generats a custom message to include on failure</param>
    public static IMore<bool> False(
        this IBe<bool> continuation,
        Func<string> customMessageGenerator)
    {
        return continuation.AddMatcher(TruthTestFor(false, customMessageGenerator));
    }

    /// <summary>
    /// Tests if a boolean value is False
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    public static IMore<bool?> False(this IBe<bool?> continuation)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        return continuation.False(null as string);
    }

    /// <summary>
    /// Tests if a boolean value is False
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="message">Custom message to include on failure</param>
    public static IMore<bool?> False(this IBe<bool?> continuation, string message)
    {
        return continuation.False(() => message);
    }

    /// <summary>
    /// Tests if a boolean value is False
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="customMessageGenerator">Generates a custom message to include on failure</param>
    public static IMore<bool?> False(
        this IBe<bool?> continuation,
        Func<string> customMessageGenerator
    )
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        return continuation.AddMatcher(
            TruthTestFor(false as bool?, customMessageGenerator)
        );
    }

    private static IMore<bool> TestBoolean(
        IBe<bool> expectation,
        bool expected,
        Func<string> message
    )
    {
        return expectation.AddMatcher(TruthTestFor(expected, message));
    }

    private static Func<T, MatcherResult> TruthTestFor<T>(
        T expected,
        Func<string> message
    )
    {
        return actual =>
        {
            if (actual.Equals(expected))
                return new MatcherResult(true, () => $"Did not expect {true}");
            return new MatcherResult(
                false,
                FinalMessageFor(
                    () => new[]
                    {
                        "Expected",
                        expected.Stringify(),
                        "but got",
                        actual.Stringify()
                    },
                    message
                ));
        };
    }
}