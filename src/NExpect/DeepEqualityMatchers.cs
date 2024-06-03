using System;
using System.Linq;
using NExpect.Helpers;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;
using MH = NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect;

/// <summary>
/// Provides matchers for deep equality testing of objects
/// </summary>
public static class DeepEqualityMatchers
{
    /// <summary>
    /// Performs deep equality testing on two objects
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
    /// for customising equality testing for properties of type TProperty</param>
    /// <typeparam name="T">Type of object</typeparam>
    public static IMore<T> Equal<T>(
        this IDeep<T> continuation,
        object expected,
        params object[] customEqualityComparers
    )
    {
        return continuation.Equal(expected, MH.NULL_STRING, customEqualityComparers);
    }

    /// <summary>
    /// Performs deep equality testing on two objects
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
    /// for customising equality testing for properties of type TProperty</param>
    /// <typeparam name="T">Type of object</typeparam>
    public static IMore<T> Equal<T>(
        this IDeep<T> continuation,
        object expected,
        string customMessage,
        params object[] customEqualityComparers
    )
    {
        return continuation.Equal(expected, () => customMessage, customEqualityComparers);
    }

    /// <summary>
    /// Performs deep equality testing on two objects
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
    /// for customising equality testing for properties of type TProperty</param>
    /// <typeparam name="T">Type of object</typeparam>
    public static IMore<T> Equal<T>(
        this IDeep<T> continuation,
        object expected,
        Func<string> customMessageGenerator,
        params object[] customEqualityComparers
    )
    {
        return DoDeepEqualityTesting(
            continuation,
            expected,
            customMessageGenerator,
            customEqualityComparers
        );
    }

    /// <summary>
    /// Performs deep equality testing on two objects
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
    /// for customising equality testing for properties of type TProperty</param>
    /// <typeparam name="T">Type of object</typeparam>
    public static IMore<T> To<T>(
        this IDeepEqual<T> continuation,
        object expected,
        params object[] customEqualityComparers
    )
    {
        return continuation.To(expected, MH.NULL_STRING, customEqualityComparers);
    }

    /// <summary>
    /// Performs deep equality testing on two objects
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
    /// for customising equality testing for properties of type TProperty</param>
    /// <typeparam name="T">Type of object</typeparam>
    public static IMore<T> To<T>(
        this IDeepEqual<T> continuation,
        object expected,
        string customMessage,
        params object[] customEqualityComparers
    )
    {
        return continuation.To(expected, () => customMessage, customEqualityComparers);
    }

    /// <summary>
    /// Performs deep equality testing on two objects
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected value</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
    /// for customising equality testing for properties of type TProperty</param>
    /// <typeparam name="T">Type of object</typeparam>
    public static IMore<T> To<T>(
        this IDeepEqual<T> continuation,
        object expected,
        Func<string> customMessageGenerator,
        params object[] customEqualityComparers
    )
    {
        return DoDeepEqualityTesting(
            continuation,
            expected,
            customMessageGenerator,
            customEqualityComparers
        );
    }

    private static IMore<T> DoDeepEqualityTesting<T>(
        this ICanAddMatcher<T> continuation,
        object expected,
        Func<string> customMessageGenerator,
        params object[] customEqualityComparers
    )
    {
        return continuation.AddMatcher(
            actual =>
            {
                var ignoreProperties = actual.FindOrAddPropertyIgnoreListMetadata();
                var deepEqualResult = DeepTestHelpers.AreDeepEqual(
                    actual,
                    expected,
                    customEqualityComparers,
                    ignoreProperties
                );
                return new MatcherResult(
                    deepEqualResult.AreEqual,
                    MH.FinalMessageFor(
                        () =>
                        {
                            var result = new[]
                            {
                                "Expected",
                                actual.Stringify(),
                                $"{deepEqualResult.AreEqual.AsNot()}to deep equal",
                                expected.Stringify()
                            }.Concat(deepEqualResult.Errors).ToArray();
                            return customEqualityComparers.Any()
                                ? result
                                    .And("Using custom equality comparers:")
                                    .And(customEqualityComparers.Select(o => o.GetType()).ToArray().Stringify())
                                : result;
                        },
                        customMessageGenerator
                    )
                );
            }
        );
    }
}