using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Interfaces;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable UsePatternMatching
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect.MatcherLogic;

/// <summary>
/// Provides extension methods to add matchers to continuations which support it
/// </summary>
public static class AddMatcherExtensions
{
    /// <summary>
    /// Simplifies adding matchers which also include a context switch
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="matcher"></param>
    /// <typeparam name="TCurrent"></typeparam>
    /// <typeparam name="TNext"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IMore<TNext> AddMatcher<TCurrent, TNext>(
        this ICanAddMatcher<TCurrent> continuation,
        Func<TCurrent, IMatcherResultWithNext<TNext>> matcher
    )
    {
        var result = AddMatcherPrivate(continuation, matcher)
            as IMatcherResultWithNext<TNext>;
        if (result is null)
        {
            throw new InvalidOperationException(
                "Expected to get a matcher result with NextFetcher, but got null"
            );
        }

        return new Next<TNext>(
            result.NextFetcher,
            continuation as IExpectationContext
        );
    }

    /// <summary>
    /// Most general matcher add - onto ICanAddMatcher&lt;T&gt;
    /// </summary>
    /// <param name="continuation">Continuation to add matcher to</param>
    /// <param name="matcher">Matcher to run</param>
    /// <typeparam name="T">Type of the object under test</typeparam>
    public static IMore<T> AddMatcher<T>(
        this ICanAddMatcher<T> continuation,
        Func<T, IMatcherResult> matcher)
    {
        AddMatcherPrivate(continuation, matcher);
        return continuation.More();
    }

    /// <summary>
    /// Add a matcher onto an Exception property continuation
    /// </summary>
    /// <param name="continuation">Continuation to add matcher to</param>
    /// <param name="matcher">Matcher to run</param>
    /// <typeparam name="T">Type of the object under test</typeparam>
    public static void AddMatcher<T>(
        this IExceptionPropertyContinuation<T> continuation,
        Func<string, IMatcherResult> matcher
    )
    {
        AddMatcherPrivate(continuation, matcher);
    }

    /// <summary>
    /// Add a matcher onto a Collection continuation
    /// </summary>
    /// <param name="continuation">Continuation to add matcher to</param>
    /// <param name="matcher">Matcher to run</param>
    /// <typeparam name="T">Type of the object under test</typeparam>
    public static IMore<IEnumerable<T>> AddMatcher<T>(
        this ICanAddMatcher<IEnumerable<T>> continuation,
        Func<IEnumerable<T>, IMatcherResult> matcher
    )
    {
        AddMatcherPrivate(continuation, matcher);
        return continuation.More();
    }
    

    /// <summary>
    /// Use to compose expectations into one matcher
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expectationsRunner">Runs your composed expectations</param>
    /// <param name="callingMethod"></param>
    /// <typeparam name="T"></typeparam>
    public static IMore<T> Compose<T>(
        this ICanAddMatcher<T> continuation,
        Action<T> expectationsRunner,
        [CallerMemberName] string callingMethod = null
    )
    {
        return continuation.Compose(expectationsRunner,
            (a, b) => $"Expectation \"{callingMethod}\" should {(!b).AsNot()}have failed.");
    }

    /// <summary>
    /// Use to compose expectations into one matcher
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expectationsRunner">Runs your composed expectations</param>
    /// <param name="messageGenerator">Generates the final message, passing in the actual instance being tested as well as a boolean for passed/failed</param>
    /// <typeparam name="T"></typeparam>
    public static IMore<T> Compose<T>(
        this ICanAddMatcher<T> continuation,
        Action<T> expectationsRunner,
        Func<T, bool, string> messageGenerator
    )
    {
        continuation.AddMatcher(actual =>
        {
            try
            {
                // if we're using custom assertions, we won't get an UnmetExpectationException
                // on a failure
                // worse, if those custom assertions are from NUnit, we can't stop the failure
                // -> it's part of the design of NUnit
                using var _ = Assertions.TemporarilyUseDefaultAssertionsFactoryForThisThread();
                expectationsRunner(actual);
                return new MatcherResult(true, () => messageGenerator(actual, true));
            }
            catch (UnmetExpectationException e)
            {
                return new MatcherResult(false,
                    () => FinalMessageFor(
                        new[] { "Specifically:", e.Message },
                        messageGenerator?.Invoke(actual, false)
                    )
                );
            }
        });
        return continuation.More();
    }

    /// <summary>
    /// Use to compose expectations into one matcher
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expectationsRunner">Runs your composed expectations</param>
    /// <param name="callingMethod"></param>
    /// <typeparam name="T"></typeparam>
    public static IMore<IEnumerable<T>> Compose<T>(
        this ICanAddMatcher<IEnumerable<T>> continuation,
        Action<IEnumerable<T>> expectationsRunner,
        [CallerMemberName] string callingMethod = null
    )
    {
        return continuation.Compose(expectationsRunner,
            (a, b) => $"{callingMethod} should {b.AsNot()}have passed.");
    }

    /// <summary>
    /// Use to compose expectations into one matcher
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expectationsRunner">Runs your composed expectations</param>
    /// <param name="messageGenerator">Generates the final message, passing in the actual instance being tested as well as a boolean for passed/failed</param>
    /// <typeparam name="T"></typeparam>
    public static IMore<IEnumerable<T>> Compose<T>(
        this ICanAddMatcher<IEnumerable<T>> continuation,
        Action<IEnumerable<T>> expectationsRunner,
        Func<IEnumerable<T>, bool, string> messageGenerator
    )
    {
        continuation.AddMatcher(actual =>
        {
            try
            {
                expectationsRunner(actual);
                return new MatcherResult(true, () => messageGenerator(actual, true));
            }
            catch (UnmetExpectationException e)
            {
                return new MatcherResult(false,
                    () => FinalMessageFor(
                        new[] { "Specifically:", e.Message },
                        messageGenerator?.Invoke(actual, false)
                    )
                );
            }
        });
        return continuation.More();
    }

    private static IMatcherResult AddMatcherPrivate<T>(
        object continuation,
        Func<T, IMatcherResult> matcher)
    {
        Assertions.Forget(continuation);
        Debug.WriteLine($"Adding matcher for type {typeof(T)}");
        var asContext = continuation as IExpectationContext<T>;
        if (asContext == null)
        {
            throw new InvalidOperationException($"{continuation} does not implement IExpectationContext<T>");
        }

        return asContext.RunMatcher(matcher) ?? throw new InvalidOperationException(
            "Unable to run matcher - null result returned"
        );
    }
}