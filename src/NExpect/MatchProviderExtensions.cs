using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace NExpect;

/// <summary>
/// Provides Match() continuations which allow providing a simple
/// lambda to do your matching, for when writing an entire extension method
/// seems like an overkill.
/// </summary>
public static class MatchProviderExtensions
{
    /// <summary>
    /// Match the value under test with a simple Func which takes in your value
    /// and returns true if the test should pass.
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="test">Func to test the original value with</param>
    /// <typeparam name="T"></typeparam>
    public static IMore<T> Match<T>(
        this ITo<T> continuation,
        Func<T, bool> test
    )
    {
        return continuation.Match(test, NULL_STRING);
    }

    /// <summary>
    /// Match the value under test with a simple Func which takes in your value
    /// and returns true if the test should pass.
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="test">Func to test the original value with</param>
    /// <param name="customMessage">Message to include in the result upon failure</param>
    /// <typeparam name="T"></typeparam>
    public static IMore<T> Match<T>(
        this ITo<T> continuation,
        Func<T, bool> test,
        string customMessage
    )
    {
        return continuation.Match(test, () => customMessage);
    }

    /// <summary>
    /// Match the value under test with a simple Func which takes in your value
    /// and returns true if the test should pass.
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="test">Func to test the original value with</param>
    /// <param name="customMessageGenerator">Generates a custom message to include in the result upon failure</param>
    /// <typeparam name="T"></typeparam>
    public static IMore<T> Match<T>(
        this ITo<T> continuation,
        Func<T, bool> test,
        Func<string> customMessageGenerator
    )
    {
        return continuation.AddMatcher(MatchMatcherFor(test, customMessageGenerator));
    }

    /// <summary>
    /// Match the value under test with a simple Func which takes in your value
    /// and returns true if the test should pass.
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="test">Func to test the original value with</param>
    /// <typeparam name="T"></typeparam>
    public static IMore<T> Match<T>(
        this IToAfterNot<T> continuation,
        Func<T, bool> test
    )
    {
        return continuation.Match(test, NULL_STRING);
    }

    /// <summary>
    /// Match the value under test with a simple Func which takes in your value
    /// and returns true if the test should pass.
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="test">Func to test the original value with</param>
    /// <param name="customMessage">Message to include in the result upon failure</param>
    /// <typeparam name="T"></typeparam>
    public static IMore<T> Match<T>(
        this IToAfterNot<T> continuation,
        Func<T, bool> test,
        string customMessage
    )
    {
        return continuation.Match(test, () => customMessage);
    }

    /// <summary>
    /// Match the value under test with a simple Func which takes in your value
    /// and returns true if the test should pass.
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="test">Func to test the original value with</param>
    /// <param name="customMessageGenerator">Generates a custom message to include in the result upon failure</param>
    /// <typeparam name="T"></typeparam>
    public static IMore<T> Match<T>(
        this IToAfterNot<T> continuation,
        Func<T, bool> test,
        Func<string> customMessageGenerator
    )
    {
        return continuation.AddMatcher(MatchMatcherFor(test, customMessageGenerator));
    }

    /// <summary>
    /// Match the value under test with a simple Func which takes in your value
    /// and returns true if the test should pass.
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="test">Func to test the original value with</param>
    /// <typeparam name="T"></typeparam>
    public static IMore<T> Match<T>(
        this INotAfterTo<T> continuation,
        Func<T, bool> test
    )
    {
        return continuation.Match(test, NULL_STRING);
    }

    /// <summary>
    /// Match the value under test with a simple Func which takes in your value
    /// and returns true if the test should pass.
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="test">Func to test the original value with</param>
    /// <param name="customMessage">Message to include in the result upon failure</param>
    /// <typeparam name="T"></typeparam>
    public static IMore<T> Match<T>(
        this INotAfterTo<T> continuation,
        Func<T, bool> test,
        string customMessage
    )
    {
        return continuation.Match(test, () => customMessage);
    }

    /// <summary>
    /// Match the value under test with a simple Func which takes in your value
    /// and returns true if the test should pass.
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="test">Func to test the original value with</param>
    /// <param name="customMessageGenerator">Generates a custom message to include in the result upon failure</param>
    /// <typeparam name="T"></typeparam>
    public static IMore<T> Match<T>(
        this INotAfterTo<T> continuation,
        Func<T, bool> test,
        Func<string> customMessageGenerator
    )
    {
        return continuation.AddMatcher(MatchMatcherFor(test, customMessageGenerator));
    }

    private static Func<T, IMatcherResult> MatchMatcherFor<T>(
        Func<T, bool> test,
        Func<string> customMessageGenerator
    )
    {
        return actual =>
        {
            var passed = test(actual);
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () => passed
                        ? new[] {"Expected", actual.Stringify(), "not to be matched"}
                        : new[] {"Expected", actual.Stringify(), "to be matched"},
                    customMessageGenerator)
            );
        };
    }
}