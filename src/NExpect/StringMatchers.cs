using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using Imported.PeanutButter.Utils;
using NExpect.Implementations.Strings;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.Assertions;

// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect;

/// <summary>
/// Provides in-built string-testing matchers
/// </summary>
public static class StringMatchers
{
    /// <summary>
    /// Tests if the value under test contains a given string. May be continued
    /// with ".And"
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="search">String value to search for</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore Contain(
        this ICanAddMatcher<string> continuation,
        string search
    )
    {
        return continuation.Contain(
            search,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests if the value under test contains a given string. May be continued
    /// with ".And"
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="search">String value to search for</param>
    /// <param name="customMessage">Custom message to include in failure messages</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore Contain(
        this ICanAddMatcher<string> continuation,
        string search,
        string customMessage
    )
    {
        return continuation.Contain(
            search,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if the value under test contains a given string. May be continued
    /// with ".And"
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="search">String value to search for</param>
    /// <param name="comparison"></param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore Contain(
        this ICanAddMatcher<string> continuation,
        string search,
        StringComparison comparison
    )
    {
        return continuation.Contain(
            search,
            comparison,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests if the value under test contains a given string. May be continued
    /// with ".And"
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="search">String value to search for</param>
    /// <param name="comparison"></param>
    /// <param name="customMessage">Custom message to include in failure messages</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore Contain(
        this ICanAddMatcher<string> continuation,
        string search,
        StringComparison comparison,
        string customMessage
    )
    {
        return continuation.Contain(
            search,
            comparison,
            () => customMessage
        );
    }


    /// <summary>
    /// Tests if the value under test contains a given string. May be continued
    /// with ".And"
    /// </summary>
    /// <param name="continuation">Continuation to act on</param>
    /// <param name="search">String value to search for</param>
    /// <param name="customMessageGenerator">Custom message to include in failure messages</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore Contain(
        this ICanAddMatcher<string> continuation,
        string search,
        Func<string> customMessageGenerator
    )
    {
        return continuation.Contain(
            search,
            DefaultStringComparison,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Tests if the value under test contains a given string. May be continued
    /// with ".And"
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <param name="comparison"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IStringMore Contain(
        this ICanAddMatcher<string> continuation,
        string search,
        StringComparison comparison,
        Func<string> customMessageGenerator
    )
    {
        var result = new StringContainContinuation(continuation);
        AddContainsMatcherTo(
            continuation,
            search,
            comparison,
            customMessageGenerator,
            result
        );
        return result;
    }

    /// <summary>
    /// Continue testing a string for another substring
    /// </summary>
    /// <param name="continuation">Existing continuation from a Contain()</param>
    /// <param name="search">string to search for</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore And(
        this IStringContainContinuation continuation,
        string search
    )
    {
        return continuation.And(
            search,
            DefaultStringComparison
        );
    }

    /// <summary>
    /// Continue testing a string for another substring
    /// </summary>
    /// <param name="continuation">Existing continuation from a Contain()</param>
    /// <param name="comparison"></param>
    /// <param name="search">string to search for</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore And(
        this IStringContainContinuation continuation,
        StringComparison comparison,
        string search
    )
    {
        return continuation.And(
            search,
            comparison,
            NULL_STRING
        );
    }

    /// <summary>
    /// Continue testing a string for another substring
    /// </summary>
    /// <param name="continuation">Existing continuation from a Contain()</param>
    /// <param name="search">string to search for</param>
    /// <param name="customMessage">Custom message to include in failure messages</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore And(
        this IStringContainContinuation continuation,
        string search,
        string customMessage
    )
    {
        return continuation.And(
            search,
            DefaultStringComparison,
            customMessage
        );
    }

    /// <summary>
    /// Continue testing a string for another substring
    /// </summary>
    /// <param name="continuation">Existing continuation from a Contain()</param>
    /// <param name="search">string to search for</param>
    /// <param name="comparison"></param>
    /// <param name="customMessage">Custom message to include in failure messages</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore And(
        this IStringContainContinuation continuation,
        string search,
        StringComparison comparison,
        string customMessage
    )
    {
        return continuation.And(
            search,
            comparison,
            () => customMessage
        );
    }

    /// <summary>
    /// Continue testing a string for another substring
    /// </summary>
    /// <param name="continuation">Existing continuation from a Contain()</param>
    /// <param name="search">string to search for</param>
    /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore And(
        this IStringContainContinuation continuation,
        string search,
        Func<string> customMessageGenerator
    )
    {
        return continuation.And(
            search,
            DefaultStringComparison,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Continue testing a string for another substring
    /// </summary>
    /// <param name="continuation">Existing continuation from a Contain()</param>
    /// <param name="search">string to search for</param>
    /// <param name="comparison"></param>
    /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore And(
        this IStringContainContinuation continuation,
        string search,
        StringComparison comparison,
        Func<string> customMessageGenerator
    )
    {
        var result = new StringContainContinuation(continuation);
        continuation.SetMetadata(
            SEARCH_OFFSET,
            0
        ); // And will reset the offset -- it's not ordered
        AddContainsMatcherTo(
            continuation,
            search,
            comparison,
            customMessageGenerator,
            result
        );
        return result;
    }

    /// <summary>
    /// Continue testing a string for another substring
    /// </summary>
    /// <param name="continuation">Existing continuation from a Contain()</param>
    /// <param name="search">string to search for</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore And(
        this IStringMore continuation,
        string search
    )
    {
        return continuation.And(
            search,
            DefaultStringComparison
        );
    }

    /// <summary>
    /// Continue testing a string for another substring
    /// </summary>
    /// <param name="continuation">Existing continuation from a Contain()</param>
    /// <param name="search">string to search for</param>
    /// <param name="comparison"></param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore And(
        this IStringMore continuation,
        string search,
        StringComparison comparison
    )
    {
        return continuation.And(
            search,
            comparison,
            NULL_STRING
        );
    }

    /// <summary>
    /// Continue testing a string for another substring
    /// </summary>
    /// <param name="continuation">Existing continuation from a Contain()</param>
    /// <param name="search">string to search for</param>
    /// <param name="customMessage">Custom message to include in failure messages</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore And(
        this IStringMore continuation,
        string search,
        string customMessage
    )
    {
        return continuation.And(
            search,
            DefaultStringComparison,
            customMessage
        );
    }

    /// <summary>
    /// Continue testing a string for another substring
    /// </summary>
    /// <param name="continuation">Existing continuation from a Contain()</param>
    /// <param name="search">string to search for</param>
    /// <param name="comparison"></param>
    /// <param name="customMessage">Custom message to include in failure messages</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore And(
        this IStringMore continuation,
        string search,
        StringComparison comparison,
        string customMessage
    )
    {
        return continuation.And(
            search,
            comparison,
            () => customMessage
        );
    }

    /// <summary>
    /// Continue testing a string for another substring
    /// </summary>
    /// <param name="continuation">Existing continuation from a Contain()</param>
    /// <param name="search">string to search for</param>
    /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore And(
        this IStringMore continuation,
        string search,
        Func<string> customMessageGenerator
    )
    {
        return continuation.And(
            search,
            DefaultStringComparison,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Continue testing a string for another substring
    /// </summary>
    /// <param name="continuation">Existing continuation from a Contain()</param>
    /// <param name="search">string to search for</param>
    /// <param name="comparison"></param>
    /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
    /// <returns>IStringContainContinuation onto which you can chain .And</returns>
    public static IStringMore And(
        this IStringMore continuation,
        string search,
        StringComparison comparison,
        Func<string> customMessageGenerator
    )
    {
        var result = new StringContainContinuation(continuation);
        continuation.SetMetadata(
            SEARCH_OFFSET,
            0
        ); // And will reset the offset -- it's not ordered
        AddContainsMatcherTo(
            continuation,
            search,
            comparison,
            customMessageGenerator,
            result
        );
        return result;
    }

    /// <summary>
    /// Continue testing a string for another substring from beyond the end of the last match
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="search">String to search for</param>
    /// <returns></returns>
    public static IStringMore Then(
        this IStringContainContinuation continuation,
        string search
    )
    {
        return continuation.Then(
            search,
            DefaultStringComparison
        );
    }

    /// <summary>
    /// Continue testing a string for another substring from beyond the end of the last match
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="search">String to search for</param>
    /// <param name="comparison"></param>
    /// <returns></returns>
    public static IStringMore Then(
        this IStringContainContinuation continuation,
        string search,
        StringComparison comparison
    )
    {
        return continuation.Then(
            search,
            comparison,
            NULL_STRING
        );
    }

    /// <summary>
    /// Continue testing a string for another substring from beyond the end of the last match
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="search">String to search for</param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IStringMore Then(
        this IStringContainContinuation continuation,
        string search,
        string customMessage
    )
    {
        return continuation.Then(
            search,
            DefaultStringComparison,
            customMessage
        );
    }

    /// <summary>
    /// Continue testing a string for another substring from beyond the end of the last match
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="search">String to search for</param>
    /// <param name="comparison"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IStringMore Then(
        this IStringContainContinuation continuation,
        string search,
        StringComparison comparison,
        string customMessage
    )
    {
        return continuation.Then(
            search,
            comparison,
            () => customMessage
        );
    }

    /// <summary>
    /// Continue testing a string for another substring from beyond the end of the last match
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="search">String to search for</param>
    /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
    /// <returns></returns>
    public static IStringMore Then(
        this IStringContainContinuation continuation,
        string search,
        Func<string> customMessageGenerator
    )
    {
        return continuation.Then(
            search,
            DefaultStringComparison,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Continue testing a string for another substring from beyond the end of the last match
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="search">String to search for</param>
    /// <param name="comparison"></param>
    /// <param name="customMessageGenerator">Generates a custom message to include in failure messages</param>
    /// <returns></returns>
    public static IStringMore Then(
        this IStringContainContinuation continuation,
        string search,
        StringComparison comparison,
        Func<string> customMessageGenerator
    )
    {
        var result = new StringContainContinuation(continuation);
        AddContainsMatcherTo(
            continuation,
            search,
            comparison,
            customMessageGenerator,
            result
        );
        return result;
    }

    /// <summary>
    /// Provides the .Then(...) extension on already extended string
    /// continuations.
    /// </summary>
    /// <param name="more">Continuation to operate on</param>
    /// <param name="search">String to search for</param>
    /// <returns></returns>
    public static IStringMore Then(
        this IStringMore more,
        string search
    )
    {
        return more.Then(
            search,
            DefaultStringComparison
        );
    }

    /// <summary>
    /// Provides the .Then(...) extension on already extended string
    /// continuations.
    /// </summary>
    /// <param name="more">Continuation to operate on</param>
    /// <param name="search">String to search for</param>
    /// <param name="comparison"></param>
    /// <returns></returns>
    public static IStringMore Then(
        this IStringMore more,
        string search,
        StringComparison comparison
    )
    {
        return more.Then(
            search,
            comparison,
            NULL_STRING
        );
    }


    /// <summary>
    /// Provides the .Then(...) extension on already extended string
    /// continuations.
    /// </summary>
    /// <param name="more">Continuation to operate on</param>
    /// <param name="search">String to search for</param>
    /// <param name="customMessage">Generates a custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Then(
        this IStringMore more,
        string search,
        string customMessage
    )
    {
        return more.Then(
            search,
            DefaultStringComparison,
            () => customMessage
        );
    }

    /// <summary>
    /// Provides the .Then(...) extension on already extended string
    /// continuations.
    /// </summary>
    /// <param name="more">Continuation to operate on</param>
    /// <param name="search">String to search for</param>
    /// <param name="comparison"></param>
    /// <param name="customMessage">Generates a custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Then(
        this IStringMore more,
        string search,
        StringComparison comparison,
        string customMessage
    )
    {
        return more.Then(
            search,
            comparison,
            () => customMessage
        );
    }

    /// <summary>
    /// Provides the .Then(...) extension on already extended string
    /// continuations.
    /// </summary>
    /// <param name="more">Continuation to operate on</param>
    /// <param name="search">String to search for</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Then(
        this IStringMore more,
        string search,
        Func<string> customMessageGenerator
    )
    {
        return more.Then(
            search,
            DefaultStringComparison,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Provides the .Then(...) extension on already extended string
    /// continuations.
    /// </summary>
    /// <param name="more">Continuation to operate on</param>
    /// <param name="search">String to search for</param>
    /// <param name="comparison"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Then(
        this IStringMore more,
        string search,
        StringComparison comparison,
        Func<string> customMessageGenerator
    )
    {
        var canAddMatcher = more as ICanAddMatcher<string>;
        var result = new StringContainContinuation(canAddMatcher);
        AddContainsMatcherTo(
            canAddMatcher,
            search,
            comparison,
            customMessageGenerator,
            result
        );
        return result;
    }

    /// <summary>
    /// Tests if a string starts with an expected value
    /// </summary>
    /// <param name="start">Continuation to operate on</param>
    /// <param name="expected">String that is expected at the start of the Actual</param>
    public static IStringMore With(
        this IStringStart start,
        string expected
    )
    {
        return start.With(
            expected,
            DefaultStringComparison
        );
    }

    /// <summary>
    /// Tests if a string starts with an expected value
    /// </summary>
    /// <param name="start">Continuation to operate on</param>
    /// <param name="comparison"></param>
    /// <param name="expected">String that is expected at the start of the Actual</param>
    public static IStringMore With(
        this IStringStart start,
        string expected,
        StringComparison comparison
    )
    {
        return start.With(
            expected,
            comparison,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests if a string starts with an expected value
    /// </summary>
    /// <param name="start">Continuation to operate on</param>
    /// <param name="expected">String that is expected at the start of the Actual</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IStringMore With(
        this IStringStart start,
        string expected,
        string customMessage
    )
    {
        return start.With(
            expected,
            DefaultStringComparison,
            customMessage
        );
    }

    /// <summary>
    /// Tests if a string starts with an expected value
    /// </summary>
    /// <param name="start">Continuation to operate on</param>
    /// <param name="expected">String that is expected at the start of the Actual</param>
    /// <param name="comparison"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IStringMore With(
        this IStringStart start,
        string expected,
        StringComparison comparison,
        string customMessage
    )
    {
        return start.With(
            expected,
            comparison,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if a string starts with an expected value
    /// </summary>
    /// <param name="start">Continuation to operate on</param>
    /// <param name="expected">String that is expected at the start of the Actual</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IStringMore With(
        this IStringStart start,
        string expected,
        Func<string> customMessageGenerator
    )
    {
        return start.With(
            expected,
            DefaultStringComparison,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Tests if a string starts with an expected value
    /// </summary>
    /// <param name="start">Continuation to operate on</param>
    /// <param name="expected">String that is expected at the start of the Actual</param>
    /// <param name="comparison"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    public static IStringMore With(
        this IStringStart start,
        string expected,
        StringComparison comparison,
        Func<string> customMessageGenerator
    )
    {
        start.AddMatcher(
            actual =>
            {
                var passed = actual?.StartsWith(
                    expected,
                    comparison
                ) ?? false;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => new[]
                        {
                            "Expected",
                            actual.Stringify(),
                            $"{passed.AsNot()}to start with",
                            expected.Stringify()
                        },
                        customMessageGenerator
                    )
                );
            }
        );
        return start.More();
    }

    /// <summary>
    /// Tests if a string ends with an expected value
    /// </summary>
    /// <param name="end">Continuation to operate on</param>
    /// <param name="expected">String that is expected at the end of the Actual</param>
    public static IStringMore With(
        this IStringEnd end,
        string expected
    )
    {
        return end.With(
            expected,
            DefaultStringComparison
        );
    }

    /// <summary>
    /// Tests if a string ends with an expected value
    /// </summary>
    /// <param name="end">Continuation to operate on</param>
    /// <param name="expected">String that is expected at the end of the Actual</param>
    /// <param name="comparison"></param>
    public static IStringMore With(
        this IStringEnd end,
        string expected,
        StringComparison comparison
    )
    {
        return end.With(
            expected,
            comparison,
            NULL_STRING
        );
    }


    /// <summary>
    /// Tests if a string ends with an expected value
    /// </summary>
    /// <param name="end">Continuation to operate on</param>
    /// <param name="expected">String that is expected at the start of the Actual</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IStringMore With(
        this IStringEnd end,
        string expected,
        string customMessage
    )
    {
        return end.With(
            expected,
            DefaultStringComparison,
            customMessage
        );
    }

    /// <summary>
    /// Tests if a string ends with an expected value
    /// </summary>
    /// <param name="end">Continuation to operate on</param>
    /// <param name="expected">String that is expected at the start of the Actual</param>
    /// <param name="comparison"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IStringMore With(
        this IStringEnd end,
        string expected,
        StringComparison comparison,
        string customMessage
    )
    {
        return end.With(
            expected,
            comparison,
            () => customMessage
        );
    }


    /// <summary>
    /// Tests if a string ends with an expected value
    /// </summary>
    /// <param name="end">Continuation to operate on</param>
    /// <param name="expected">String that is expected at the start of the Actual</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IStringMore With(
        this IStringEnd end,
        string expected,
        Func<string> customMessage
    )
    {
        return end.With(
            expected,
            DefaultStringComparison,
            customMessage
        );
    }

    /// <summary>
    /// Tests if a string ends with an expected value
    /// </summary>
    /// <param name="end">Continuation to operate on</param>
    /// <param name="expected">String that is expected at the start of the Actual</param>
    /// <param name="comparison"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    public static IStringMore With(
        this IStringEnd end,
        string expected,
        StringComparison comparison,
        Func<string> customMessage
    )
    {
        end.AddMatcher(
            actual =>
            {
                var passed = actual?.EndsWith(
                    expected,
                    comparison
                ) ?? false;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => new[]
                        {
                            "Expected",
                            actual.Stringify(),
                            $"{passed.AsNot()}to end with",
                            expected.Stringify()
                        },
                        customMessage
                    )
                );
            }
        );
        return end.More();
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="regex">Regex instance to match with</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore By(
        this IStringMatched matched,
        Regex regex
    )
    {
        return matched.By(
            regex,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="regex">Regex instance to match with</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this ITo<string> matched,
        Regex regex
    )
    {
        return matched.Match(
            regex,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="regex">Regex instance to match with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this ITo<string> matched,
        Regex regex,
        string customMessage
    )
    {
        return matched.Match(
            regex,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="regex">Regex instance to match with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this ITo<string> matched,
        Regex regex,
        Func<string> customMessageGenerator
    )
    {
        AddRegexMatcher(
            matched,
            regex,
            customMessageGenerator
        );
        return matched.More();
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="regex">Regex string to match with</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this ITo<string> matched,
        string regex
    )
    {
        return matched.Match(
            regex,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="regex">Regex string to match with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this ITo<string> matched,
        string regex,
        string customMessage
    )
    {
        return matched.Match(
            regex,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="regex">Regex string to match with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this ITo<string> matched,
        string regex,
        Func<string> customMessageGenerator
    )
    {
        AddRegexMatcher(
            matched,
            regex,
            customMessageGenerator
        );
        return matched.More();
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matcher">Continuation to operate on</param>
    /// <param name="regex">Regex instance to match with</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this IToAfterNot<string> matcher,
        Regex regex
    )
    {
        return matcher.Match(
            regex,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matcher">Continuation to operate on</param>
    /// <param name="regex">Regex instance to match with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this IToAfterNot<string> matcher,
        Regex regex,
        string customMessage
    )
    {
        return matcher.Match(
            regex,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matcher">Continuation to operate on</param>
    /// <param name="regex">Regex instance to match with</param>
    /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this IToAfterNot<string> matcher,
        Regex regex,
        Func<string> customMessageGenerator
    )
    {
        AddRegexMatcher(
            matcher,
            regex,
            customMessageGenerator
        );
        return matcher.More();
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matcher">Continuation to operate on</param>
    /// <param name="regex">Regex string to match with</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this IToAfterNot<string> matcher,
        string regex
    )
    {
        return matcher.Match(
            regex,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matcher">Continuation to operate on</param>
    /// <param name="regex">Regex string to match with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this IToAfterNot<string> matcher,
        string regex,
        string customMessage
    )
    {
        return matcher.Match(
            regex,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matcher">Continuation to operate on</param>
    /// <param name="regex">Regex string to match with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this IToAfterNot<string> matcher,
        string regex,
        Func<string> customMessageGenerator
    )
    {
        AddRegexMatcher(
            matcher,
            regex,
            customMessageGenerator
        );
        return matcher.More();
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matcher">Continuation to operate on</param>
    /// <param name="regex">Regex instance to match with</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this INotAfterTo<string> matcher,
        Regex regex
    )
    {
        return matcher.Match(
            regex,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matcher">Continuation to operate on</param>
    /// <param name="regex">Regex instance to match with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this INotAfterTo<string> matcher,
        Regex regex,
        string customMessage
    )
    {
        return matcher.Match(
            regex,
            () => customMessage
        );
    }


    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matcher">Continuation to operate on</param>
    /// <param name="regex">Regex instance to match with</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this INotAfterTo<string> matcher,
        Regex regex,
        Func<string> customMessageGenerator
    )
    {
        AddRegexMatcher(
            matcher,
            regex,
            customMessageGenerator
        );
        return matcher.More();
    }


    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matcher">Continuation to operate on</param>
    /// <param name="regex">Regex string to match with</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this INotAfterTo<string> matcher,
        string regex
    )
    {
        return matcher.Match(
            regex,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matcher">Continuation to operate on</param>
    /// <param name="regex">Regex string to match with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this INotAfterTo<string> matcher,
        string regex,
        string customMessage
    )
    {
        return matcher.Match(
            regex,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matcher">Continuation to operate on</param>
    /// <param name="regex">Regex string to match with</param>
    /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore Match(
        this INotAfterTo<string> matcher,
        string regex,
        Func<string> customMessageGenerator
    )
    {
        AddRegexMatcher(
            matcher,
            regex,
            customMessageGenerator
        );
        return matcher.More();
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="regex">Regex instance to match with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore By(
        this IStringMatched matched,
        Regex regex,
        string customMessage
    )
    {
        return matched.By(
            regex,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="regex">Regex instance to match with</param>
    /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore By(
        this IStringMatched matched,
        Regex regex,
        Func<string> customMessageGenerator
    )
    {
        AddRegexMatcher(
            matched,
            regex,
            customMessageGenerator
        );
        return matched.More();
    }

    private static void AddRegexMatcher(
        ICanAddMatcher<string> matcher,
        string regex,
        Func<string> customMessage
    )
    {
        AddRegexMatcher(
            matcher,
            CompileRegexFor(regex),
            customMessage
        );
    }

    private static void AddRegexMatcher(
        ICanAddMatcher<string> matcher,
        Regex regex,
        Func<string> customMessageGenerator
    )
    {
        matcher.AddMatcher(
            actual =>
            {
                var passed = regex.IsMatch(actual);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => new[]
                        {
                            "Expected",
                            actual.Stringify(),
                            $"{passed.AsNot()}to match regex",
                            $"\"{regex}\""
                        },
                        customMessageGenerator
                    )
                );
            }
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="regex">Regular expression which will be compiled into a Regex instance to match with</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore By(
        this IStringMatched matched,
        string regex
    )
    {
        return matched.By(
            regex,
            null
        );
    }

    /// <summary>
    /// Tests whether the Actual string is matched by the given Regex
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="regex">Regular expression which will be compiled into a Regex instance to match with</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>More continuation for Actual string</returns>
    public static IStringMore By(
        this IStringMatched matched,
        string regex,
        string customMessage
    )
    {
        return matched.By(
            CompileRegexFor(regex),
            customMessage
        );
    }

    private static Regex CompileRegexFor(
        string regex
    )
    {
        try
        {
            return new Regex(regex);
        }
        catch (Exception e)
        {
            throw new UnmetExpectationException(
                new[]
                {
                    $"Unable to compile {regex.Stringify()} as a Regex",
                    "Specifically:",
                    e.Message
                }.JoinWith("\n")
            );
        }
    }

    /// <summary>
    /// Checks for fragments in order in the string being expected upon
    /// </summary>
    /// <param name="stringIn">Continuation to operate on</param>
    /// <param name="firstFragment">First fragment to look for</param>
    /// <param name="fragments">Subsequent fragments to look for</param>
    /// <returns></returns>
    public static IStringMore Order(
        this IStringIn stringIn,
        string firstFragment,
        params string[] fragments
    )
    {
        return stringIn.Order(
            new[] { firstFragment }.Concat(fragments),
            null
        );
    }

    /// <summary>
    /// Checks for fragments in order in the string being expected upon
    /// </summary>
    /// <param name="stringIn">Continuation to operate on</param>
    /// <param name="fragments">Fragments to look for</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Order(
        this IStringIn stringIn,
        IEnumerable<string> fragments,
        string customMessage
    )
    {
        if (!fragments.Any())
        {
            throw new InvalidOperationException(".In.Order(...) requires at least one fragment");
        }

        var first = fragments.First();
        var canAddMatcher = stringIn as ICanAddMatcher<string>;
        fragments.Skip(1)
            .Aggregate(
                canAddMatcher.Contain(
                    first,
                    customMessage
                ),
                (
                    acc,
                    cur
                ) => acc.Then(
                    cur,
                    customMessage
                )
            );
        return canAddMatcher.More();
    }


    private const string SEARCH_OFFSET = "SearchOffset";

    private static void AddContainsMatcherTo(
        ICanAddMatcher<string> continuation,
        string search,
        StringComparison comparison,
        Func<string> customMessage,
        StringContainContinuation next
    )
    {
        ExpectationTracker.Forget(next);
        continuation.AddMatcher(
            s =>
            {
                var priorOffset = continuation.GetMetadata<int>(SEARCH_OFFSET);
                var nextOffset = GetNextOffsetOf(
                    search,
                    s,
                    priorOffset,
                    comparison
                );

                next.SetMetadata(
                    SEARCH_OFFSET,
                    nextOffset
                );

                var passed = nextOffset > -1;
                return new MatcherResult(
                    passed,
                    () =>
                    {
                        var offsetMessage = priorOffset > 0
                            ? $" after index {priorOffset}"
                            : "";
                        return FinalMessageFor(
                            () => new[]
                            {
                                "Expected",
                                s.Stringify(),
                                $"{passed.AsNot()}to contain",
                                search.Stringify(),
                                offsetMessage
                            },
                            customMessage
                        );
                    }
                );
            }
        );
    }

    private static int GetNextOffsetOf(
        string needle,
        string haystack,
        int priorOffset,
        StringComparison comparison
    )
    {
        if (priorOffset >= (haystack?.Length ?? 0) - 1)
        {
            return -1;
        }

        if (priorOffset < 0)
        {
            priorOffset = 0;
        }

        var nextOffset = haystack?.IndexOf(
            needle,
            priorOffset,
            comparison
        ) ?? -1;
        if (nextOffset > -1)
        {
            nextOffset += needle?.Length ?? 0;
        }

        return nextOffset;
    }

    /// <summary>
    /// Tests the string's length against the expected value
    /// </summary>
    /// <param name="have">Continuation to operate on</param>
    /// <param name="expected">Expected string length</param>
    /// <param name="customMessageGenerator">Custom message generator</param>
    /// <returns>More continuation -- continue with more assertions!</returns>
    public static IStringMore Length(
        this IHave<string> have,
        int expected,
        Func<string> customMessageGenerator
    )
    {
        have.AddMatcher(
            actual =>
            {
                var passed = actual != null && actual.Length == expected;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected {actual.Stringify()} {passed.AsNot()}to have length {expected}",
                        customMessageGenerator
                    )
                );
            }
        );
        return have.More();
    }

    /// <summary>
    /// Tests the string's length against the expected value
    /// </summary>
    /// <param name="have">Continuation to operate on</param>
    /// <param name="expected">Expected string length</param>
    /// <param name="customMessage">Custom message generator</param>
    /// <returns>More continuation -- continue with more assertions!</returns>
    public static IStringMore Length(
        this IHave<string> have,
        int expected,
        string customMessage
    )
    {
        return have.Length(
            expected,
            () => customMessage
        );
    }


    /// <summary>
    /// Tests the string's length against the expected value
    /// </summary>
    /// <param name="have">Continuation to operate on</param>
    /// <param name="expected">Expected string length</param>
    /// <returns>More continuation -- continue with more assertions!</returns>
    public static IStringMore Length(
        this IHave<string> have,
        int expected
    )
    {
        return have.Length(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Asserts that a string is alpha-numeric
    /// </summary>
    /// <param name="be"></param>
    /// <returns></returns>
    public static IStringMore Alphanumeric(
        this IBe<string> be
    )
    {
        return be.Alphanumeric(NULL_STRING);
    }

    /// <summary>
    /// Asserts that a string is alpha-numeric
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IStringMore Alphanumeric(
        this IBe<string> be,
        string customMessage
    )
    {
        return be.Alphanumeric(() => customMessage);
    }

    /// <summary>
    /// Asserts that a string is alpha-numeric
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IStringMore Alphanumeric(
        this IBe<string> be,
        Func<string> customMessageGenerator
    )
    {
        be.AddMatcher(
            actual =>
            {
                var passed = actual != null &&
                    actual.All(c => c.IsNumeric() || c.IsAlpha());
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected {actual.Stringify()} {passed.AsNot()}to be alpha-numeric",
                        customMessageGenerator
                    )
                );
            }
        );
        return be.More();
    }

    /// <summary>
    /// Asserts that a string is numeric
    /// </summary>
    /// <param name="be"></param>
    /// <returns></returns>
    public static IStringMore Numeric(
        this IBe<string> be
    )
    {
        return be.Numeric(NULL_STRING);
    }

    /// <summary>
    /// Asserts that a string is numeric
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IStringMore Numeric(
        this IBe<string> be,
        string customMessage
    )
    {
        return be.Numeric(() => customMessage);
    }

    /// <summary>
    /// Asserts that a string is numeric
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IStringMore Numeric(
        this IBe<string> be,
        Func<string> customMessageGenerator
    )
    {
        be.AddMatcher(
            actual =>
            {
                var passed = actual != null &&
                    actual.All(c => c.IsNumeric());
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected {actual.Stringify()} {passed.AsNot()}to be numeric",
                        customMessageGenerator
                    )
                );
            }
        );
        return be.More();
    }

    /// <summary>
    /// Asserts that a string is alpha
    /// </summary>
    /// <param name="be"></param>
    /// <returns></returns>
    public static IStringMore Alpha(
        this IBe<string> be
    )
    {
        return be.Alpha(NULL_STRING);
    }

    /// <summary>
    /// Asserts that a string is alpha
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IStringMore Alpha(
        this IBe<string> be,
        string customMessage
    )
    {
        return be.Alpha(() => customMessage);
    }

    /// <summary>
    /// Asserts that a string is alpha
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IStringMore Alpha(
        this IBe<string> be,
        Func<string> customMessageGenerator
    )
    {
        be.AddMatcher(
            actual =>
            {
                var passed = actual != null &&
                    actual.All(c => c.IsAlpha());
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected {actual.Stringify()} {passed.AsNot()}to be alpha",
                        customMessageGenerator
                    )
                );
            }
        );
        return be.More();
    }
}