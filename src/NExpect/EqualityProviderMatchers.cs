using System;
using System.Collections.Generic;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect;

/// <summary>
/// Provides matchers for testing equality
/// </summary>
public static class EqualityProviderMatchers
{
    /// <summary>
    /// Performs reference equality checking between your actual and the provided expected value
    /// </summary>
    /// <param name="be">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <typeparam name="T">Type of the object being tested</typeparam>
    public static IMore<T> Be<T>(
        this ITo<T> be,
        object expected)
    {
        return be.Be(expected, NULL_STRING);
    }

    /// <summary>
    /// Performs reference equality checking between your actual and the provided expected value
    /// </summary>
    /// <param name="be">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <typeparam name="T">Type of the object being tested</typeparam>
    public static IMore<T> Be<T>(
        this ITo<T> be,
        object expected,
        string customMessage)
    {
        return be.Be(expected, () => customMessage);
    }

    /// <summary>
    /// Performs reference equality checking between your actual and the provided expected value
    /// </summary>
    /// <param name="be">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="T">Type of the object being tested</typeparam>
    public static IMore<T> Be<T>(
        this ITo<T> be,
        object expected,
        Func<string> customMessageGenerator)
    {
        return be.AddMatcher(
            CreateRefEqualMatcherFor<T>(
                expected,
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Performs reference equality checking between your actual and the provided expected value
    /// </summary>
    /// <param name="be">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <typeparam name="T">Type of the object being tested</typeparam>
    public static IMore<T> Be<T>(this IToAfterNot<T> be, object expected)
    {
        return be.Be(expected, NULL_STRING);
    }

    /// <summary>
    /// Performs reference equality checking between your actual and the provided expected value
    /// </summary>
    /// <param name="be">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <typeparam name="T">Type of the object being tested</typeparam>
    public static IMore<T> Be<T>(
        this IToAfterNot<T> be,
        object expected,
        string customMessage)
    {
        return be.Be(expected, () => customMessage);
    }

    /// <summary>
    /// Performs reference equality checking between your actual and the provided expected value
    /// </summary>
    /// <param name="be">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="T">Type of the object being tested</typeparam>
    public static IMore<T> Be<T>(
        this IToAfterNot<T> be,
        object expected,
        Func<string> customMessageGenerator)
    {
        return be.AddMatcher(
            CreateRefEqualMatcherFor<T>(
                expected,
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Performs reference equality checking between your actual and the provided expected value
    /// </summary>
    /// <param name="be">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <typeparam name="T">Type of the object being tested</typeparam>
    public static IMore<T> Be<T>(this INotAfterTo<T> be, object expected)
    {
        return be.Be(expected, NULL_STRING);
    }

    /// <summary>
    /// Performs reference equality checking between your actual and the provided expected value
    /// </summary>
    /// <param name="be">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <typeparam name="T">Type of the object being tested</typeparam>
    public static IMore<T> Be<T>(
        this INotAfterTo<T> be,
        object expected,
        string customMessage)
    {
        return be.Be(expected, () => customMessage);
    }

    /// <summary>
    /// Performs reference equality checking between your actual and the provided expected value
    /// </summary>
    /// <param name="be">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="T">Type of the object being tested</typeparam>
    public static IMore<T> Be<T>(
        this INotAfterTo<T> be,
        object expected,
        Func<string> customMessageGenerator)
    {
        return be.AddMatcher(
            CreateRefEqualMatcherFor<T>(
                expected,
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Performs reference equality checking between your actual and the provided expected value
    /// </summary>
    /// <param name="be">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <typeparam name="T">Type of the object being tested</typeparam>
    public static IMore<IEnumerable<T>> Be<T>(
        this ICollectionTo<T> be,
        object expected)
    {
        return be.Be(expected, NULL_STRING);
    }

    /// <summary>
    /// Performs reference equality checking between your actual and the provided expected value
    /// </summary>
    /// <param name="be">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T">Type of the object being tested</typeparam>
    public static IMore<IEnumerable<T>> Be<T>(
        this ICollectionTo<T> be,
        object expected,
        string customMessage)
    {
        return be.Be(expected, () => customMessage);
    }

    /// <summary>
    /// Performs reference equality checking between your actual and the provided expected value
    /// </summary>
    /// <param name="be">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T">Type of the object being tested</typeparam>
    public static IMore<IEnumerable<T>> Be<T>(
        this ICollectionTo<T> be,
        object expected,
        Func<string> customMessageGenerator)
    {
        return be.AddMatcher(
            CreateCollectionRefEqualMatcherFor<T>(
                expected,
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Performs reference equality checking between actual and expected values
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    public static IMore<IEnumerable<T>> Be<T>(
        this ICollectionToAfterNot<T> be,
        object expected
    )
    {
        return be.Be(expected, NULL_STRING);
    }

    /// <summary>
    /// Performs reference equality checking between actual and expected values
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    public static IMore<IEnumerable<T>> Be<T>(
        this ICollectionToAfterNot<T> be,
        object expected,
        string customMessage
    )
    {
        return be.Be(expected, () => customMessage);
    }

    /// <summary>
    /// Performs reference equality checking between actual and expected values
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    public static IMore<IEnumerable<T>> Be<T>(
        this ICollectionToAfterNot<T> be,
        object expected,
        Func<string> customMessageGenerator
    )
    {
        return be.AddMatcher(
            CreateCollectionRefEqualMatcherFor<T>(
                expected,
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Performs reference equality checking between actual and expected values
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    public static IMore<IEnumerable<T>> Be<T>(
        this ICollectionNotAfterTo<T> be,
        object expected
    )
    {
        return be.Be(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Performs reference equality checking between actual and expected values
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    public static IMore<IEnumerable<T>> Be<T>(
        this ICollectionNotAfterTo<T> be,
        object expected,
        string customMessage
    )
    {
        return be.Be(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Performs reference equality checking between actual and expected values
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    public static IMore<IEnumerable<T>> Be<T>(
        this ICollectionNotAfterTo<T> be,
        object expected,
        Func<string> customMessageGenerator
    )
    {
        return be.AddMatcher(
            CreateCollectionRefEqualMatcherFor<T>(
                expected,
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this ITo<T> continuation,
        T expected
    )
    {
        return continuation.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this ITo<T> continuation,
        T expected,
        string customMessage
    )
    {
        return continuation.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this ITo<T> continuation,
        T? expected
    ) where T : struct
    {
        return continuation.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to add into failure messages</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this ITo<T> continuation,
        T? expected,
        string customMessage
    ) where T : struct
    {
        return continuation.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Custom message to add into failure messages</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this ITo<T> continuation,
        T? expected,
        Func<string> customMessageGenerator
    ) where T : struct
    {
        return continuation.AddMatcher(
            GenerateNullableEqualityMatcherFor(expected, customMessageGenerator)
        );
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this IToAfterNot<T> continuation,
        T expected
    )
    {
        return continuation.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this IToAfterNot<T> continuation,
        T expected,
        string customMessage
    )
    {
        return continuation.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this IToAfterNot<T> continuation,
        T expected,
        Func<string> customMessageGenerator
    )
    {
        return continuation.AddMatcher(
            GenerateEqualityMatcherFor(expected, customMessageGenerator)
        );
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this IToAfterNot<T> continuation,
        T? expected
    ) where T : struct
    {
        return continuation.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this IToAfterNot<T> continuation,
        T? expected,
        string customMessage
    ) where T : struct
    {
        return continuation.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this IToAfterNot<T> continuation,
        T? expected,
        Func<string> customMessageGenerator
    ) where T : struct
    {
        return continuation.AddMatcher(
            GenerateNullableEqualityMatcherFor(expected, customMessageGenerator)
        );
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this INotAfterTo<T> continuation,
        T expected
    )
    {
        return continuation.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this INotAfterTo<T> continuation,
        T expected,
        string customMessage
    )
    {
        return continuation.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this INotAfterTo<T> continuation,
        T expected,
        Func<string> customMessageGenerator
    )
    {
        return continuation.AddMatcher(
            GenerateEqualityMatcherFor(expected, customMessageGenerator)
        );
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this INotAfterTo<T> continuation,
        T? expected
    ) where T : struct
    {
        return continuation.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this INotAfterTo<T> continuation,
        T? expected,
        string customMessage
    ) where T : struct
    {
        return continuation.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this INotAfterTo<T> continuation,
        T? expected,
        Func<string> customMessageGenerator
    ) where T : struct
    {
        return continuation.AddMatcher(
            GenerateNullableEqualityMatcherFor(expected, customMessageGenerator)
        );
    }

    /// <summary>
    /// Performs equality checking -- the end of .To.Equal()
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Custom message to include when failing</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Equal<T>(
        this ITo<T> continuation,
        T expected,
        Func<string> customMessageGenerator
    )
    {
        return continuation.AddMatcher(
            GenerateEqualityMatcherFor(expected, customMessageGenerator)
        );
    }

    /// <summary>
    /// Tests if a value is null
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="customMessage">Custom message to include when failing</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Null<T>(
        this IBe<T> continuation,
        string customMessage
    )
    {
        return continuation.Null(() => customMessage);
    }

    /// <summary>
    /// Tests if a value is null
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="customMessageGenerator">Generates a custom message to include when failing</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Null<T>(
        this IBe<T> continuation,
        Func<string> customMessageGenerator
    )
    {
        continuation.AddMatcher(
            actual =>
            {
                var passed = actual == null;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => passed
                            ? new[] { "Expected not to get null" }
                            : new[] { "Expected null but got", Quote(actual) },
                        customMessageGenerator)
                );
            });
        return continuation.More();
    }

    /// <summary>
    /// Tests if a value is null
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <typeparam name="T">Type of object being tested</typeparam>
    public static IMore<T> Null<T>(this IBe<T> continuation)
    {
        return continuation.Null(NULL_STRING);
    }

    /// <summary>
    /// Last part of the .To.Be.Equal.To() chain
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <typeparam name="T">Object type being tested</typeparam>
    public static IMore<T> To<T>(
        this IEqualityContinuation<T> continuation,
        T expected
    )
    {
        return continuation.To(expected, NULL_STRING);
    }

    /// <summary>
    /// Last part of the .To.Be.Equal.To() chain
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to include when failing</param>
    /// <typeparam name="T">Object type being tested</typeparam>
    public static IMore<T> To<T>(
        this IEqualityContinuation<T> continuation,
        T expected,
        string customMessage
    )
    {
        return continuation.To(expected, () => customMessage);
    }

    /// <summary>
    /// Last part of the .To.Be.Equal.To() chain
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Generates a custom message to include when failing</param>
    /// <typeparam name="T">Object type being tested</typeparam>
    public static IMore<T> To<T>(
        this IEqualityContinuation<T> continuation,
        T expected,
        Func<string> customMessageGenerator
    )
    {
        return continuation.AddMatcher(
            actual =>
            {
                var passed = (actual == null && expected == null) ||
                    (actual?.Equals(expected) ?? false);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => new[]
                        {
                            "Expected",
                            Quote(actual),
                            $"{passed.AsNot()}to equal",
                            Quote(expected)
                        },
                        customMessageGenerator
                    ));
            });
    }

    /// <summary>
    /// Tests if a string is empty, with a provided custom error message
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="customMessage">Custom message to include when failing</param>
    public static IMore<string> Empty(
        this IBe<string> continuation,
        string customMessage)
    {
        return continuation.Empty(() => customMessage);
    }

    /// <summary>
    /// Tests if a string is empty, with a provided custom error message
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="customMessageGenerator">Generates a custom message to include when failing</param>
    public static IMore<string> Empty(
        this IBe<string> continuation,
        Func<string> customMessageGenerator)
    {
        return continuation.AddMatcher(
            actual =>
            {
                var passed = actual == "";
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => passed
                            ? new[] { "Expected not to be empty" }
                            : new[] { "Expected empty string but got", Quote(actual) },
                        customMessageGenerator)
                );
            });
    }

    /// <summary>
    /// Tests if a string is empty
    /// </summary>
    /// <param name="continuation"></param>
    public static IMore<string> Empty(this IBe<string> continuation)
    {
        return continuation.Empty(NULL_STRING);
    }

    /// <summary>
    /// Tests if a string is null or empty
    /// </summary>
    /// <param name="nullOr"></param>
    public static IMore<string> Empty(
        this INullOr<string> nullOr
    )
    {
        return nullOr.Empty(NULL_STRING);
    }

    /// <summary>
    /// Tests if a string is null or empty
    /// </summary>
    /// <param name="nullOr"></param>
    /// <param name="customMessage">Custom message to add to the final failure message</param>
    public static IMore<string> Empty(
        this INullOr<string> nullOr,
        string customMessage
    )
    {
        return nullOr.Empty(() => customMessage);
    }

    /// <summary>
    /// Tests if a string is null or empty
    /// </summary>
    /// <param name="nullOr"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to the final failure message</param>
    public static IMore<string> Empty(
        this INullOr<string> nullOr,
        Func<string> customMessageGenerator
    )
    {
        return nullOr.AddMatcher(
            actual =>
            {
                var passed = string.IsNullOrEmpty(actual);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected {actual} {passed.AsNot()}to be null or empty",
                        customMessageGenerator
                    )
                );
            });
    }

    /// <summary>
    /// Test if string is null or whitespace
    /// </summary>
    /// <param name="nullOr"></param>
    public static IMore<string> Whitespace(
        this INullOr<string> nullOr
    )
    {
        return nullOr.Whitespace(NULL_STRING);
    }

    /// <summary>
    /// Test if string is null or whitespace
    /// </summary>
    /// <param name="nullOr"></param>
    /// <param name="customMessage">Custom message to add to the final failure message</param>
    public static IMore<string> Whitespace(
        this INullOr<string> nullOr,
        string customMessage
    )
    {
        return nullOr.Whitespace(() => customMessage);
    }

    /// <summary>
    /// Test if string is null or whitespace
    /// </summary>
    /// <param name="nullOr"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to the final failure message</param>
    public static IMore<string> Whitespace(
        this INullOr<string> nullOr,
        Func<string> customMessageGenerator
    )
    {
        return nullOr.AddMatcher(
            actual =>
            {
                var passed = string.IsNullOrWhiteSpace(actual);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected {actual} {passed.AsNot()}to be null or whitespace",
                        customMessageGenerator
                    )
                );
            });
    }

    private static Func<T, IMatcherResult> CreateRefEqualMatcherFor<T>(
        object other,
        Func<string> customMessageGenerator)
    {
        return actual => RefCompare(actual, other, customMessageGenerator);
    }

    private static Func<IEnumerable<T>, IMatcherResult> CreateCollectionRefEqualMatcherFor<T>(
        object other,
        Func<string> customMessageGenerator)
    {
        return actual => RefCompare(actual, other, customMessageGenerator);
    }

    private static IMatcherResult RefCompare(object actual,
        object other,
        Func<string> customMessageGenerator)
    {
        var passed = ReferenceEquals(actual, other);
        return new MatcherResult(
            passed,
            () => FinalMessageFor(
                $"Expected {actual?.ToString() ?? NULL_REPLACER} {passed.AsNot()}to be the same reference as {other?.ToString() ?? NULL_REPLACER}",
                customMessageGenerator()
            )
        );
    }

    private static Func<T, IMatcherResult> GenerateNullableEqualityMatcherFor<T>(
        T? expected,
        Func<string> customMessage
    ) where T : struct
    {
        return actual =>
        {
            var nullableActual = actual as T?;
            return CompareForEquality(nullableActual, expected, customMessage);
        };
    }

    internal static Func<T, IMatcherResult> GenerateEqualityMatcherFor<T>(
        T expected,
        Func<string> customMessageGenerator
    )
    {
        return actual => CompareForEquality(
            actual,
            expected,
            customMessageGenerator);
    }

    private static IMatcherResult CompareForEquality<T>(
        T actual,
        T expected,
        Func<string> customMessageGenerator)
    {
        if (BothAreNull(actual, expected) ||
            ValuesAreEqual(actual, expected))
        {
            return new MatcherResult(
                true,
                FinalMessageFor(
                    () => new[]
                    {
                        "Did not expect",
                        Quote(expected),
                        "but got exactly that"
                    },
                    customMessageGenerator)
            );
        }

        var extraMessage = DifferenceHighlighting.ProvideMoreInfoFor(
            actual,
            expected
        );
        var message = new List<string>(new[]
        {
            "Expected",
            Quote(expected),
            "but got",
            Quote(actual)
        });
        if (!string.IsNullOrWhiteSpace(extraMessage))
        {
            message.Add(extraMessage);
        }

        return new MatcherResult(
            false,
            FinalMessageFor(
                () => message.ToArray(),
                customMessageGenerator
            ));
    }

    private static bool ValuesAreEqual<T>(
        T actual,
        T expected)
    {
        if (actual == null &&
            expected == null)
        {
            return true;
        }

        if (actual == null ||
            expected == null)
        {
            return false;
        }

        var result = (actual.Equals(expected) ||
            CollectionsAreEqual(actual, expected));
        if (!result)
            return false;

        if (expected is DateTime expectedDateTime &&
            actual is DateTime actualDateTime)
        {
            return expectedDateTime.Kind == actualDateTime.Kind;
        }

        return true;
    }

    private static bool CollectionsAreEqual<T>(
        T actual,
        T expected)
    {
        var actualEnumerable = new EnumerableWrapper(actual);
        if (!actualEnumerable.IsValid)
        {
            return false;
        }

        var expectedEnumerable = new EnumerableWrapper(expected);
        if (!expectedEnumerable.IsValid)
        {
            return false;
        }

        var actualEnumerator = actualEnumerable.GetEnumerator();
        var expectedEnumerator = expectedEnumerable.GetEnumerator();

        var actualHasNext = actualEnumerator.MoveNext();
        var expectedHasNext = expectedEnumerator.MoveNext();
        while (actualHasNext && expectedHasNext)
        {
            if (!ValuesAreEqual(
                    actualEnumerator.Current,
                    expectedEnumerator.Current))
            {
                return false;
            }

            actualHasNext = actualEnumerator.MoveNext();
            expectedHasNext = expectedEnumerator.MoveNext();
        }

        return actualHasNext == expectedHasNext;
    }

    private static bool BothAreNull<T>(T expected, T actual)
    {
        return actual == null && expected == null;
    }
}