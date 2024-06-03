using System;
using System.Linq;
using NExpect.Helpers;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect;

/// <summary>
/// Provides matchers for testing Intersection equality between objects
/// </summary>
public static class IntersectionEqualityMatchers
{
    /// <summary>
    /// Performs intersection-equality testing on two objects
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Object to test against</param>
    /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
    /// for customising equality testing for properties of type TProperty</param>
    /// <typeparam name="T">Original type</typeparam>
    public static IMore<T> Equal<T>(
        this IIntersection<T> continuation,
        object expected,
        params object[] customEqualityComparers
    )
    {
        return continuation.Equal(expected, NULL_STRING, customEqualityComparers);
    }

    /// <summary>
    /// Performs intersection-equality testing on two objects
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Object to test against</param>
    /// <param name="customMessage"></param>
    /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
    /// for customising equality testing for properties of type TProperty</param>
    /// <typeparam name="T">Original type</typeparam>
    public static IMore<T> Equal<T>(
        this IIntersection<T> continuation,
        object expected,
        string customMessage,
        params object[] customEqualityComparers
    )
    {
        return continuation.Equal(expected, () => customMessage, customEqualityComparers);
    }

    /// <summary>
    /// Performs intersection-equality testing on two objects
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Object to test against</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
    /// for customising equality testing for properties of type TProperty</param>
    /// <typeparam name="T">Original type</typeparam>
    public static IMore<T> Equal<T>(
        this IIntersection<T> continuation,
        object expected,
        Func<string> customMessageGenerator,
        params object[] customEqualityComparers
    )
    {
        return RunIntersectionEqualityTest(
            continuation,
            expected,
            customMessageGenerator,
            customEqualityComparers
        );
    }

    /// <summary>
    /// Performs intersection-equality testing on two objects
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Object to test against</param>
    /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
    /// for customising equality testing for properties of type TProperty</param>
    /// <typeparam name="T">Original type</typeparam>
    public static IMore<T> To<T>(
        this IIntersectionEqual<T> continuation,
        object expected,
        params object[] customEqualityComparers
    )
    {
        return continuation.To(expected, NULL_STRING, customEqualityComparers);
    }

    /// <summary>
    /// Performs intersection-equality testing on two objects
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Object to test against</param>
    /// <param name="customMessage"></param>
    /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
    /// for customising equality testing for properties of type TProperty</param>
    /// <typeparam name="T">Original type</typeparam>
    public static IMore<T> To<T>(
        this IIntersectionEqual<T> continuation,
        object expected,
        string customMessage,
        params object[] customEqualityComparers
    )
    {
        return continuation.To(expected, () => customMessage, customEqualityComparers);
    }

    /// <summary>
    /// Performs intersection-equality testing on two objects
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Object to test against</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <param name="customEqualityComparers">Objects implementing IEqualityComparer&lt;TProperty&gt;
    /// for customising equality testing for properties of type TProperty</param>
    /// <typeparam name="T">Original type</typeparam>
    public static IMore<T> To<T>(
        this IIntersectionEqual<T> continuation,
        object expected,
        Func<string> customMessageGenerator,
        params object[] customEqualityComparers
    )
    {
        return RunIntersectionEqualityTest(
            continuation,
            expected,
            customMessageGenerator,
            customEqualityComparers
        );
    }

    private static IMore<T> RunIntersectionEqualityTest<T>(
        ICanAddMatcher<T> continuation,
        object expected,
        Func<string> customMessageGenerator,
        params object[] customEqualityComparers
    )
    {
        return continuation.AddMatcher(
            actual =>
            {
                var ignoreProperties = actual.FindOrAddPropertyIgnoreListMetadata();
                var result = DeepTestHelpers.AreIntersectionEqual(
                    actual,
                    expected,
                    customEqualityComparers,
                    ignoreProperties
                );
                return new MatcherResult(
                    result.AreEqual,
                    FinalMessageFor(
                        () => new[]
                        {
                            "Expected",
                            actual.Stringify(),
                            $"{result.AreEqual.AsNot()}to intersection equal",
                            expected.Stringify()
                        }.Concat(result.Errors).ToArray(),
                        customMessageGenerator
                    )
                );
            }
        );
    }
}