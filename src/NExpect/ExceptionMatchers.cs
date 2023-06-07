using System;
using System.Text.RegularExpressions;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using Imported.PeanutButter.Utils;
using NExpect.Helpers;
using NExpect.Implementations.Exceptions;
using NExpect.Implementations.Strings;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.Assertions;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMethodReturnValue.Global

namespace NExpect;

/// <summary>
/// Provides matchers for exception testing
/// </summary>
public static class ExceptionMatchers
{
    /// <summary>
    /// Expects the Action to throw any kind of exception
    /// </summary>
    /// <param name="src">Action to run</param>
    /// <returns>Continuation which can be used to test exception messages</returns>
    public static IThrowContinuation<Exception> Throw(
        this ICanAddMatcher<Action> src
    )
    {
        return src.Throw(NULL_STRING);
    }

    /// <summary>
    /// Expects the Action to throw any kind of exception
    /// </summary>
    /// <param name="src">Action to run</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>Continuation which can be used to test exception messages</returns>
    public static IThrowContinuation<Exception> Throw(
        this ICanAddMatcher<Action> src,
        string customMessage
    )
    {
        return src.Throw(() => customMessage);
    }

    /// <summary>
    /// Expects the Action to throw any kind of exception
    /// </summary>
    /// <param name="src">Action to run</param>
    /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
    /// <returns>Continuation which can be used to test exception messages</returns>
    public static IThrowContinuation<Exception> Throw(
        this ICanAddMatcher<Action> src,
        Func<string> customMessageGenerator
    )
    {
        var continuation = new ThrowContinuation<Exception>();
        src.AddMatcher(
            fn =>
            {
                MatcherResult result;
                try
                {
                    ActionRunner.Run(fn);
                    result = new MatcherResult(
                        false,
                        FinalMessageFor(
                            () => "Expected to throw an exception but none was thrown",
                            customMessageGenerator
                        )
                    );
                }
                catch (Exception ex)
                {
                    continuation.Exception = ex;
                    result = new MatcherResult(
                        true,
                        FinalMessageFor(
                            () => $"Exception thrown:\n{ex.Message}\n{ex.StackTrace}",
                            customMessageGenerator
                        ),
                        ex
                    );
                }

                return result;
            }
        );
        return continuation;
    }

    /// <summary>
    /// Shorthand for Message.Equal.To()
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IThrowAndContinuation<T> Message<T>(
        this IWithAfterThrowContinuation<T> continuation,
        string expected
    ) where T : Exception
    {
        return continuation.Message(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Shorthand for Message.Equal.To()
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IThrowAndContinuation<T> Message<T>(
        this IWithAfterThrowContinuation<T> continuation,
        string expected,
        string customMessage
    ) where T : Exception
    {
        return continuation.Message(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Shorthand for Message.Equal.To()
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IThrowAndContinuation<T> Message<T>(
        this IWithAfterThrowContinuation<T> continuation,
        string expected,
        Func<string> customMessageGenerator
    ) where T : Exception
    {
        var result = new ThrowAndContinuation<T>();
        result.SetParent(continuation as IExpectationContext<T>);
        continuation.AddMatcher(
            actual =>
            {
                if (actual is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        FinalMessageFor(
                            () => "Actual exception is null",
                            customMessageGenerator
                        )
                    );
                }

                var passed = actual.Message == expected;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected exception message '{expected}', but received '{actual.Message}'",
                        customMessageGenerator
                    )
                );
            }
        );
        return result;
    }

    /// <summary>
    /// Assert that the provided code throws an exception of the given type
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected exception type</param>
    /// <returns>Fluency extension</returns>
    public static IThrowAndContinuation<T> Type<T>(
        this IWithAfterThrowContinuation<T> continuation,
        Type expected
    ) where T : Exception
    {
        return continuation.Type(
            expected,
            null as string
        );
    }

    /// <summary>
    /// Assert that the provided code throws an exception of the given type
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected exception type</param>
    /// <param name="customMessage">Custom message to include when this expectation fails</param>
    /// <returns>Fluency extension</returns>
    public static IThrowAndContinuation<T> Type<T>(
        this IWithAfterThrowContinuation<T> continuation,
        Type expected,
        string customMessage
    ) where T : Exception
    {
        return continuation.Type(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Assert that the provided code throws an exception of the given type
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected exception type</param>
    /// <param name="customMessageGenerator">Generates a custom message to include when this expectation fails</param>
    /// <returns>Fluency extension</returns>
    public static IThrowAndContinuation<T> Type<T>(
        this IWithAfterThrowContinuation<T> continuation,
        Type expected,
        Func<string> customMessageGenerator
    ) where T : Exception
    {
        var result = new ThrowAndContinuation<T>();
        result.SetParent(continuation as IExpectationContext<T>);
        continuation.AddMatcher(
            actual =>
            {
                result.Exception = actual;
                var passed = actual.GetType() == expected;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected to throw an exception of type {expected} but received {actual}",
                        customMessageGenerator
                    )
                );
            }
        );
        return result;
    }

    /// <summary>
    /// Allows testing a specific property on the thrown exception
    /// </summary>
    /// <param name="continuation">Exception-based continuation</param>
    /// <param name="fetcher">Func to invoke to get the property you'd like to test</param>
    /// <typeparam name="T">Type of the Exception</typeparam>
    /// <typeparam name="TValue">Type of the property you're testing</typeparam>
    /// <returns>Throw continuation, used to continue with testing the property you just selected</returns>
    /// <exception cref="ArgumentException">Thrown if the continuation is not a known ThrowContinuation. Userland implementation of IThrowContinuation is not supported - yet. Make a request if you need it!</exception>
    public static IExceptionPropertyContinuation<TValue> With<T, TValue>(
        this IThrowContinuation<T> continuation,
        Func<T, TValue> fetcher
    ) where T : Exception
    {
        var throwContinuation = continuation as ThrowContinuation<T>;
        var actual = throwContinuation?.Exception ??
            continuation.TryGetPropertyValue<T>("Exception");
        if (actual == null)
        {
            // TODO: can we kinda-duck this to work on user-generated implementations of IThrowContinuation<T>? And do we care to?
            throw new ArgumentException(
                "With should operate on a ThrowContinuation<T> or at least something with an Exception property that is an Exception."
            );
        }

        var memoized = FuncFactory.Memoize(() => fetcher(actual));

        return ContinuationFactory.Create<TValue, ExceptionPropertyContinuation<TValue>>(
            memoized,
            new WrappingContinuation<T, TValue>(
                throwContinuation,
                _ => memoized()
            )
        );
    }

    /// <summary>
    /// Expects the action to throw an exception of type T
    /// </summary>
    /// <param name="src">Action to test</param>
    /// <typeparam name="T">Type of exception which is expected</typeparam>
    /// <returns>Continuation which can be used to test exception messages</returns>
    public static IThrowContinuation<T> Throw<T>(
        this ICanAddMatcher<Action> src
    ) where T : Exception
    {
        return src.Throw<T>(NULL_STRING);
    }

    /// <summary>
    /// Expects the action to throw an exception of type T
    /// </summary>
    /// <param name="src">Action to test</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <typeparam name="T">Type of exception which is expected</typeparam>
    /// <returns>Continuation which can be used to test exception messages</returns>
    public static IThrowContinuation<T> Throw<T>(
        this ICanAddMatcher<Action> src,
        string customMessage
    ) where T : Exception
    {
        return src.Throw<T>(() => customMessage);
    }

    /// <summary>
    /// Verifies that the ArgumentException or derivative was thrown with the
    /// expected ParameterName. Effectively shorthand for
    /// Expect(func).To.Throw&lt;ArgumentException&gt;.With.Property(e => e.ParamName == parameterName);
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="parameterName">Expected parameter name</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Continuation which can be used to do more verification of the exception</returns>
    public static IThrowContinuation<T> For<T>(
        this IThrowContinuation<T> continuation,
        string parameterName
    ) where T : ArgumentException
    {
        continuation.AddMatcher(
            actual =>
            {
                var passed = actual.ParamName == parameterName;
                return new MatcherResult(
                    passed,
                    () =>
                        continuation.IsNegated() // not actually sure how we'd get here if negated & throwing
                            ? $"Expected ArgumentException with ParameterName other than '{parameterName}', but found exactly that"
                            : $"Expected ArgumentException with ParameterName '{parameterName}' but found '{actual.ParamName}'"
                );
            }
        );
        return continuation;
    }

    /// <summary>
    /// Expects the action to throw an exception of type T
    /// </summary>
    /// <param name="src">Action to test</param>
    /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
    /// <typeparam name="T">Type of exception which is expected</typeparam>
    /// <returns>Continuation which can be used to test exception messages</returns>
    public static IThrowContinuation<T> Throw<T>(
        this ICanAddMatcher<Action> src,
        Func<string> customMessageGenerator
    ) where T : Exception
    {
        var continuation = new ThrowContinuation<T>();
        var expectedType = typeof(T);
        src.AddMatcher(
            fn =>
            {
                MatcherResult result;
                try
                {
                    fn();
                    result = new MatcherResult(
                        false,
                        FinalMessageFor(
                            () => $"Expected to throw an exception of type {expectedType.Name} but none was thrown",
                            customMessageGenerator
                        )
                    );
                }
                catch (Exception actual)
                {
                    var passed = actual is T;
                    result = new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () =>
                            {
                                var actualType = actual.GetType();
                                var haveSameName = expectedType.Name == actualType.Name;
                                var expectedName = haveSameName
                                    ? $"{expectedType.Namespace}.{expectedType.Name}"
                                    : expectedType.Name;
                                var actualName = haveSameName
                                    ? $"{actualType.Namespace}.{actualType.Name}"
                                    : actualType.Name;
                                return passed
                                    ? $@"Expected not to throw an exception of type {expectedName}
Stacktrace:
{actual.StackTrace}"
                                    : $@"Expected to throw an exception of type {
                                        expectedName
                                    } but {
                                        actualName
                                    } was thrown instead ({
                                        actual.Message
                                    })
Stacktrace:
{actual.StackTrace}";
                            },
                            customMessageGenerator
                        )
                    );
                    continuation.Exception = actual as T;
                }

                return result;
            }
        );
        return continuation;
    }

    /// <summary>
    /// Used to test exception messages
    /// </summary>
    /// <param name="src">Continuation carrying an exception message</param>
    /// <param name="search">String to look for in the message</param>
    /// <returns>Another continuation so you can do .And() on it</returns>
    public static IStringPropertyContinuation Containing(
        this IExceptionPropertyContinuation<string> src,
        string search
    )
    {
        return src.Containing(
            search,
            NULL_STRING
        );
    }

    /// <summary>
    /// Used to test exception messages
    /// </summary>
    /// <param name="src">Continuation carrying an exception message</param>
    /// <param name="search">String to look for in the message</param>
    /// <param name="customMessage">Custom message to add to a failure message</param>
    /// <returns>Another continuation so you can do .And() on it</returns>
    public static IStringPropertyContinuation Containing(
        this IExceptionPropertyContinuation<string> src,
        string search,
        string customMessage
    )
    {
        return src.Containing(
            search,
            () => customMessage
        );
    }

    /// <summary>
    /// Used to test exception messages
    /// </summary>
    /// <param name="src">Continuation carrying an exception message</param>
    /// <param name="search">String to look for in the message</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to a failure message</param>
    /// <returns>Another continuation so you can do .And() on it</returns>
    public static IStringPropertyContinuation Containing(
        this IExceptionPropertyContinuation<string> src,
        string search,
        Func<string> customMessageGenerator
    )
    {
        var result = ContinuationFactory.Create<string, StringPropertyContinuation>(
            () => null,
            src as IExpectationContext<string>
        );
        src.AddMatcher(
            s =>
            {
                result.Actual = s;
                var nextOffset = s?.IndexOf(search) ?? -1;
                if (nextOffset > -1)
                {
                    nextOffset += search?.Length ?? 0;
                }

                result.SetMetadata(
                    SEARCH_OFFSET,
                    nextOffset
                );

                var passed = nextOffset > -1;
                return new MatcherResult(
                    passed,
                    MessageForContainsResult(
                        passed,
                        s,
                        search,
                        customMessageGenerator
                    )
                );
            }
        );
        return result;
    }

    private const string SEARCH_OFFSET = "SearchOffset";

    /// <summary>
    /// Used to test exception messages
    /// </summary>
    /// <param name="src">Continuation containing exception message to test</param>
    /// <param name="test">Custom function to test the message -- return true if the test should pass</param>
    /// <returns>Another continuation so you can do .And()</returns>
    public static IStringPropertyContinuation Matching(
        this IExceptionPropertyContinuation<string> src,
        Func<string, bool> test
    )
    {
        return src.Matching(
            test,
            NULL_STRING
        );
    }

    /// <summary>
    /// Used to test exception messages
    /// </summary>
    /// <param name="src">Continuation containing exception message to test</param>
    /// <param name="test">Custom function to test the message -- return true if the test should pass</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>Another continuation so you can do .And()</returns>
    public static IStringPropertyContinuation Matching(
        this IExceptionPropertyContinuation<string> src,
        Func<string, bool> test,
        string customMessage
    )
    {
        return src.Matching(
            test,
            () => customMessage
        );
    }

    /// <summary>
    /// Used to test exception messages
    /// </summary>
    /// <param name="src">Continuation containing exception message to test</param>
    /// <param name="test">Custom function to test the message -- return true if the test should pass</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns>Another continuation so you can do .And()</returns>
    public static IStringPropertyContinuation Matching(
        this IExceptionPropertyContinuation<string> src,
        Func<string, bool> test,
        Func<string> customMessageGenerator
    )
    {
        var result = ContinuationFactory.Create<string, StringPropertyContinuation>(
            () => null,
            src as IExpectationContext<string>
        );
        src.AddMatcher(
            s =>
            {
                result.Actual = s;
                var passed = test(s);
                return new MatcherResult(
                    passed,
                    MessageForMatchResult(
                        passed,
                        s,
                        customMessageGenerator
                    )
                );
            }
        );
        return result;
    }

    /// <summary>
    /// Matches a string property with a regular expression
    /// </summary>
    /// <param name="src"></param>
    /// <param name="re"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation Matching(
        this IStringPropertyContinuation src,
        Regex re
    )
    {
        return src.Matching(
            re,
            NULL_STRING
        );
    }

    /// <summary>
    /// Matches a string property with a regular expression
    /// </summary>
    /// <param name="src"></param>
    /// <param name="re"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation Matching(
        this IStringPropertyContinuation src,
        Regex re,
        string customMessage
    )
    {
        return src.Matching(
            re,
            () => customMessage
        );
    }

    /// <summary>
    /// Matches a string property with a regular expression
    /// </summary>
    /// <param name="src"></param>
    /// <param name="re"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation Matching(
        this IStringPropertyContinuation src,
        Regex re,
        Func<string> customMessageGenerator
    )
    {
        var result = ContinuationFactory.Create<string, StringPropertyContinuation>(
            () => null,
            src as IExpectationContext<string>
        );
        src.AddMatcher(
            s =>
            {
                result.Actual = s;
                var passed = re.IsMatch(s);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected \"{s}\" {passed.AsNot()}to match regex {PrettyPrint(re)}",
                        customMessageGenerator
                    )
                );
            }
        );
        return result;
    }

    private static string PrettyPrint(
        Regex re
    )
    {
        return $"\"{re}\" (options: ${re.Options})";
    }

    /// <summary>
    /// Used to test exception messages
    /// </summary>
    /// <param name="src">Continuation containing exception message to test</param>
    /// <param name="test">Custom function to test the message -- return true if the test should pass</param>
    /// <returns>Another continuation so you can do .And()</returns>
    public static IStringPropertyContinuation Matching(
        this IStringPropertyContinuation src,
        Func<string, bool> test
    )
    {
        return src.Matching(
            test,
            NULL_STRING
        );
    }

    /// <summary>
    /// Used to test exception messages
    /// </summary>
    /// <param name="src">Continuation containing exception message to test</param>
    /// <param name="test">Custom function to test the message -- return true if the test should pass</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>Another continuation so you can do .And()</returns>
    public static IStringPropertyContinuation Matching(
        this IStringPropertyContinuation src,
        Func<string, bool> test,
        string customMessage
    )
    {
        return src.Matching(
            test,
            () => customMessage
        );
    }

    /// <summary>
    /// Used to test exception messages
    /// </summary>
    /// <param name="src">Continuation containing exception message to test</param>
    /// <param name="test">Custom function to test the message -- return true if the test should pass</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns>Another continuation so you can do .And()</returns>
    public static IStringPropertyContinuation Matching(
        this IStringPropertyContinuation src,
        Func<string, bool> test,
        Func<string> customMessageGenerator
    )
    {
        var result = ContinuationFactory.Create<string, StringPropertyContinuation>(
            () => null,
            src as IExpectationContext<string>
        );
        src.AddMatcher(
            s =>
            {
                result.Actual = s;
                var passed = test(s);
                return new MatcherResult(
                    passed,
                    MessageForMatchResult(
                        passed,
                        s,
                        customMessageGenerator
                    )
                );
            }
        );
        return result;
    }

    /// <summary>
    /// Used to test exception messages in the negative
    /// </summary>
    /// <param name="continuation">Continuation containing the exception message</param>
    /// <param name="search">String to search for</param>
    /// <returns>Continuation so you can perform more tests on the message</returns>
    public static IStringPropertyContinuation Containing(
        this IStringPropertyContinuation continuation,
        string search
    )
    {
        return continuation.Containing(
            search,
            NULL_STRING
        );
    }

    /// <summary>
    /// Continues to search for another string after a previous .Contains()
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation And(
        this IStringPropertyContinuation continuation,
        string other
    )
    {
        return continuation.Containing(other);
    }

    /// <summary>
    /// Continues to search for another string after a previous .Contains()
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation Then(
        this IStringPropertyContinuation continuation,
        string other
    )
    {
        return continuation.Then(
            other,
            DefaultStringComparison
        );
    }

    /// <summary>
    /// Continues to search for another string after a previous .Contains()
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="other"></param>
    /// <param name="comparison"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation Then(
        this IStringPropertyContinuation continuation,
        string other,
        StringComparison comparison
    )
    {
        return continuation.Then(
            other,
            comparison,
            null as string
        );
    }

    /// <summary>
    /// Continues to search for another string after a previous .Contains()
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="other"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation Then(
        this IStringPropertyContinuation continuation,
        string other,
        string customMessage
    )
    {
        return continuation.Then(
            other,
            DefaultStringComparison,
            customMessage
        );
    }

    /// <summary>
    /// Continues to search for another string after a previous .Contains()
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="other"></param>
    /// <param name="comparison"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation Then(
        this IStringPropertyContinuation continuation,
        string other,
        StringComparison comparison,
        string customMessage
    )
    {
        return continuation.Then(
            other,
            comparison,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if the string contains the search, using
    /// default StringComparison (InvariantCulture, if not
    /// overridden)
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="other"></param>
    /// <param name="messageGenerator"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation Then(
        this IStringPropertyContinuation continuation,
        string other,
        Func<string> messageGenerator
    )
    {
        return continuation.Then(
            other,
            DefaultStringComparison,
            messageGenerator
        );
    }

    /// <summary>
    /// Tests if the string contains the search, using
    /// default StringComparison (InvariantCulture, if not
    /// overridden)
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="other"></param>
    /// <param name="comparison"></param>
    /// <param name="messageGenerator"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation Then(
        this IStringPropertyContinuation continuation,
        string other,
        StringComparison comparison,
        Func<string> messageGenerator
    )
    {
        return RunContainFor(
            continuation,
            other,
            comparison,
            messageGenerator,
            other?.GetMetadata<int>(SEARCH_OFFSET) ?? 0
        );
    }

    /// <summary>
    /// Used to test exception messages in the negative,
    /// with the provided StringComparison
    /// </summary>
    /// <param name="continuation">Continuation containing the exception message</param>
    /// <param name="search">String to search for</param>
    /// <param name="comparison"></param>
    public static IStringPropertyContinuation Containing(
        this IStringPropertyContinuation continuation,
        string search,
        StringComparison comparison
    )
    {
        return continuation.Containing(
            search,
            comparison,
            NULL_STRING
        );
    }


    /// <summary>
    /// Used to test exception messages in the negative, using the
    /// default StringComparison (InvariantCulture, if not
    /// overridden)
    /// </summary>
    /// <param name="continuation">Continuation containing the exception message</param>
    /// <param name="search">String to search for</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>Continuation so you can perform more tests on the message</returns>
    public static IStringPropertyContinuation Containing(
        this IStringPropertyContinuation continuation,
        string search,
        string customMessage
    )
    {
        return continuation.Containing(
            search,
            DefaultStringComparison,
            () => customMessage
        );
    }

    /// <summary>
    /// Used to test exception messages in the negative,
    /// using the provided comparison
    /// </summary>
    /// <param name="continuation">Continuation containing the exception message</param>
    /// <param name="search">String to search for</param>
    /// <param name="comparison"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>Continuation so you can perform more tests on the message</returns>
    public static IStringPropertyContinuation Containing(
        this IStringPropertyContinuation continuation,
        string search,
        StringComparison comparison,
        string customMessage
    )
    {
        return continuation.Containing(
            search,
            comparison,
            () => customMessage
        );
    }

    /// <summary>
    /// Used to test exception messages in the negative,
    /// using the provided comparison
    /// </summary>
    /// <param name="continuation">Continuation containing the exception message</param>
    /// <param name="search">String to search for</param>
    /// <param name="comparison"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns>Continuation so you can perform more tests on the message</returns>
    public static IStringPropertyContinuation Containing(
        this IStringPropertyContinuation continuation,
        string search,
        StringComparison comparison,
        Func<string> customMessageGenerator
    )
    {
        return RunContainFor(
            continuation,
            search,
            comparison,
            customMessageGenerator,
            -1
        );
    }

    /// <summary>
    /// Used to test exception messages in the negative
    /// </summary>
    /// <param name="continuation">Continuation containing the exception message</param>
    /// <param name="search">String to search for</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns>Continuation so you can perform more tests on the message</returns>
    public static IStringPropertyContinuation Containing(
        this IStringPropertyContinuation continuation,
        string search,
        Func<string> customMessageGenerator
    )
    {
        return RunContainFor(
            continuation,
            search,
            DefaultStringComparison,
            customMessageGenerator,
            -1
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static IStringPropertyContinuation Containing(
        this IBe<string> continuation,
        string search
    )
    {
        return continuation.Containing(
            search,
            null as string
        );
    }

    /// <summary>
    /// Searches for a string within an Exception's string property
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation Containing(
        this IBe<string> continuation,
        string search,
        string customMessage
    )
    {
        return continuation.Containing(
            search,
            () => customMessage
        );
    }

    /// <summary>
    /// Searchs for a string within an Exception's string property
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <param name="messageGenerator"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation Containing(
        this IBe<string> continuation,
        string search,
        Func<string> messageGenerator
    )
    {
        return RunContainFor(
            continuation,
            search,
            DefaultStringComparison,
            messageGenerator,
            -1
        );
    }

    private static IStringPropertyContinuation RunContainFor(
        ICanAddMatcher<string> continuation,
        string search,
        StringComparison comparison,
        Func<string> customMessageGenerator,
        int offset
    )
    {
        var result = ContinuationFactory.Create<string, StringPropertyContinuation>(
            () => null,
            continuation as IExpectationContext<string>
        );
        ExpectationTracker.Forget(result);
        continuation.AddMatcher(
            s =>
            {
                if (search == null)
                {
                    throw new InvalidOperationException("Cannot search for NULL in a string");
                }

                result.Actual = s;
                var foundAt = s?.IndexOf(
                    search,
                    comparison
                ) ?? -1;
                var passed = offset < 0
                    ? foundAt >= 0
                    : foundAt >= offset;
                s?.SetMetadata(
                    SEARCH_OFFSET,
                    foundAt + search.Length
                );

                return new MatcherResult(
                    passed,
                    MessageForNotContainsResult(
                        passed,
                        s,
                        search,
                        customMessageGenerator
                    )
                );
            }
        );
        return result;
    }

    /// <summary>
    /// Used to test exception messages in the negative
    /// </summary>
    /// <param name="continuation">Continuation containing the exception message</param>
    /// <param name="search">String to search for</param>
    /// <returns>Continuation so you can perform more tests on the message</returns>
    public static IStringPropertyContinuation Containing(
        this IStringPropertyNot continuation,
        string search
    )
    {
        return continuation.Containing(
            search,
            NULL_STRING
        );
    }

    /// <summary>
    /// Used to test exception messages in the negative
    /// </summary>
    /// <param name="continuation">Continuation containing the exception message</param>
    /// <param name="search">String to search for</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>Continuation so you can perform more tests on the message</returns>
    public static IStringPropertyContinuation Containing(
        this IStringPropertyNot continuation,
        string search,
        string customMessage
    )
    {
        return continuation.Containing(
            search,
            () => customMessage
        );
    }

    /// <summary>
    /// Used to test exception messages in the negative
    /// </summary>
    /// <param name="continuation">Continuation containing the exception message</param>
    /// <param name="search">String to search for</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns>Continuation so you can perform more tests on the message</returns>
    public static IStringPropertyContinuation Containing(
        this IStringPropertyNot continuation,
        string search,
        Func<string> customMessageGenerator
    )
    {
        var result = ContinuationFactory.Create<string, StringPropertyContinuation>(
            () => null,
            continuation as IExpectationContext<string>
        );
        ExpectationTracker.Forget(result);
        continuation.AddMatcher(
            s =>
            {
                result.Actual = s;
                var passed = s?.Contains(search) ?? true;
                return new MatcherResult(
                    passed,
                    MessageForNotContainsResult(
                        passed,
                        s,
                        search,
                        customMessageGenerator
                    )
                );
            }
        );
        return result;
    }

    /// <summary>
    /// Used to test exception messages in the negative
    /// </summary>
    /// <param name="continuation">Continuation containing exception message to test</param>
    /// <param name="test">Custom function to test the message -- return true if the test should pass</param>
    /// <returns>Another continuation so you can do .And()</returns>
    public static IStringPropertyContinuation Matching(
        this IPropertyNot<string> continuation,
        Func<string, bool> test
    )
    {
        return continuation.Matching(
            test,
            NULL_STRING
        );
    }

    /// <summary>
    /// Used to test exception messages in the negative
    /// </summary>
    /// <param name="continuation">Continuation containing exception message to test</param>
    /// <param name="test">Custom function to test the message -- return true if the test should pass</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns>Another continuation so you can do .And()</returns>
    public static IStringPropertyContinuation Matching(
        this IPropertyNot<string> continuation,
        Func<string, bool> test,
        string customMessage
    )
    {
        return continuation.Matching(
            test,
            () => customMessage
        );
    }

    /// <summary>
    /// Used to test exception messages in the negative
    /// </summary>
    /// <param name="continuation">Continuation containing exception message to test</param>
    /// <param name="test">Custom function to test the message -- return true if the test should pass</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns>Another continuation so you can do .And()</returns>
    public static IStringPropertyContinuation Matching(
        this IPropertyNot<string> continuation,
        Func<string, bool> test,
        Func<string> customMessageGenerator
    )
    {
        continuation.AddMatcher(
            s =>
            {
                var passed = !test(s);
                return new MatcherResult(
                    passed,
                    MessageForNotMatchResult(
                        passed,
                        s,
                        customMessageGenerator
                    )
                );
            }
        );
        return continuation.More();
    }

    /// <summary>
    /// Facilitates testing an exception message to end with another substring
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation With(
        this IStringPropertyEndingContinuation continuation,
        string search
    )
    {
        return continuation.With(
            search,
            null as string
        );
    }

    /// <summary>
    /// Facilitates testing an exception message to end with another substring
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation With(
        this IStringPropertyEndingContinuation continuation,
        string search,
        string customMessage
    )
    {
        return continuation.With(
            search,
            () => customMessage
        );
    }

    /// <summary>
    /// Facilitates testing an exception message to end with another substring
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <param name="messageGenerator"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation With(
        this IStringPropertyEndingContinuation continuation,
        string search,
        Func<string> messageGenerator
    )
    {
        continuation.AddMatcher(
            actual =>
            {
                var passed =
                    actual?.EndsWith(
                        search,
                        DefaultStringComparison
                    ) ?? false;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected {actual.Stringify()} {passed.AsNot()}to end with {search.Stringify()}",
                        messageGenerator
                    )
                );
            }
        );
        return continuation.More();
    }

    /// <summary>
    /// Facilitates testing an exception message to start with another substring
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation With(
        this IStringPropertyStartingContinuation continuation,
        string search
    )
    {
        return continuation.With(
            search,
            DefaultStringComparison
        );
    }

    /// <summary>
    /// Facilitates testing an exception message to start with another substring
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <param name="comparison"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation With(
        this IStringPropertyStartingContinuation continuation,
        string search,
        StringComparison comparison
    )
    {
        return continuation.With(
            search,
            comparison,
            null as string
        );
    }

    /// <summary>
    /// Facilitates testing an exception message to start with another substring
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation With(
        this IStringPropertyStartingContinuation continuation,
        string search,
        string customMessage
    )
    {
        return continuation.With(
            search,
            DefaultStringComparison,
            customMessage
        );
    }

    /// <summary>
    /// Facilitates testing an exception message to start with another substring
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <param name="comparison"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation With(
        this IStringPropertyStartingContinuation continuation,
        string search,
        StringComparison comparison,
        string customMessage
    )
    {
        return continuation.With(
            search,
            comparison,
            () => customMessage
        );
    }

    /// <summary>
    /// Facilitates testing an exception message to start with another substring
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <param name="messageGenerator"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation With(
        this IStringPropertyStartingContinuation continuation,
        string search,
        Func<string> messageGenerator
    )
    {
        return continuation.With(
            search,
            DefaultStringComparison,
            messageGenerator
        );
    }

    /// <summary>
    /// Facilitates testing an exception message to start with another substring
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="search"></param>
    /// <param name="comparison"></param>
    /// <param name="messageGenerator"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation With(
        this IStringPropertyStartingContinuation continuation,
        string search,
        StringComparison comparison,
        Func<string> messageGenerator
    )
    {
        continuation.AddMatcher(
            actual =>
            {
                var passed =
                    actual?.StartsWith(
                        search,
                        comparison
                    ) ?? false;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected {actual.Stringify()} {passed.AsNot()}to start with {search.Stringify()}",
                        messageGenerator
                    )
                );
            }
        );
        return continuation.More();
    }

    /// <summary>
    /// Facilitates chained expectations on the existing .Ending
    /// </summary>
    /// <param name="continuation"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation More(
        this IStringPropertyEndingContinuation continuation
    )
    {
        return (continuation as IExpectationContext<string>).More();
    }

    /// <summary>
    /// Facilitates chained expectations on the existing .Starting
    /// </summary>
    /// <param name="continuation"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation More(
        this IStringPropertyStartingContinuation continuation
    )
    {
        return (continuation as IExpectationContext<string>).More();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="continuation"></param>
    /// <returns></returns>
    public static IStringPropertyContinuation More(
        this IPropertyNot<string> continuation
    )
    {
        return (continuation as IExpectationContext<string>).More();
    }

    private static IStringPropertyContinuation More(
        this IExpectationContext<string> context
    )
    {
        return ContinuationFactory.Create<string, StringPropertyContinuation>(
            () => (context as ICanAddMatcher<string>).GetActual(),
            context
        );
    }
}