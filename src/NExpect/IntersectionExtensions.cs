using System;
using System.Collections.Generic;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides matchers to verify whether or not collections
/// intersect at all
/// </summary>
public static class IntersectionExtensions
{
    /// <summary>
    /// Verifies that two collections intersect, ie that they share
    /// at least one common value
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Intersect<T>(
        this ICollectionTo<T> continuation,
        IEnumerable<T> expected
    )
    {
        return continuation.Intersect(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verifies that two collections intersect, ie that they share
    /// at least one common value
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Intersect<T>(
        this ICollectionTo<T> continuation,
        IEnumerable<T> expected,
        string customMessage
    )
    {
        return continuation.Intersect(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that two collections intersect, ie that they share
    /// at least one common value
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Intersect<T>(
        this ICollectionTo<T> continuation,
        IEnumerable<T> expected,
        Func<string> customMessageGenerator
    )
    {
        return continuation.AddMatcher(
            actual => TestIntersection(
                actual,
                expected,
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Verifies that two collections do not intersect,
    /// ie that they share no values in common
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Intersect<T>(
        this ICollectionToAfterNot<T> continuation,
        IEnumerable<T> expected
    )
    {
        return continuation.Intersect(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verifies that two collections do not intersect,
    /// ie that they share no values in common
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Intersect<T>(
        this ICollectionToAfterNot<T> continuation,
        IEnumerable<T> expected,
        string customMessage
    )
    {
        return continuation.Intersect(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that two collections do not intersect,
    /// ie that they share no values in common
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Intersect<T>(
        this ICollectionToAfterNot<T> continuation,
        IEnumerable<T> expected,
        Func<string> customMessageGenerator
    )
    {
        return continuation.AddMatcher(
            actual => TestIntersection(
                actual,
                expected,
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Verifies that two collections do not intersect,
    /// ie that they share no values in common
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Intersect<T>(
        this ICollectionNotAfterTo<T> continuation,
        IEnumerable<T> expected
    )
    {
        return continuation.Intersect(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verifies that two collections do not intersect,
    /// ie that they share no values in common
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Intersect<T>(
        this ICollectionNotAfterTo<T> continuation,
        IEnumerable<T> expected,
        string customMessage
    )
    {
        return continuation.Intersect(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that two collections do not intersect,
    /// ie that they share no values in common
    /// </summary>
    /// <param name="continuation"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Intersect<T>(
        this ICollectionNotAfterTo<T> continuation,
        IEnumerable<T> expected,
        Func<string> customMessageGenerator
    )
    {
        return continuation.AddMatcher(
            actual =>
                TestIntersection(
                    actual,
                    expected,
                    customMessageGenerator
                )
        );
    }

    private static List<T> FindIntersectingElements<T>(
        IEnumerable<T> actual,
        IEnumerable<T> expected
    )
    {
        var result = new List<T>();
        var expectedHash = new HashSet<T>(expected);
        foreach (var value in actual)
        {
            if (expectedHash.Contains(value))
            {
                result.Add(value);
            }
        }

        return result;
    }

    private static MatcherResult TestIntersection<T>(
        IEnumerable<T> actual,
        IEnumerable<T> expected,
        Func<string> customMessageGenerator
    )
    {
        var intersectingElements = FindIntersectingElements(
            actual,
            expected
        );
        if (intersectingElements.Count == 0)
        {
            return new MatcherResult(
                false,
                FinalMessageFor(
                    () => $"Expected intersection when comparing:\n{
                        actual.Stringify()
                    }\nwith\n{
                        expected.Stringify()
                    }\nbut found no matching elements",
                    customMessageGenerator
                )
            );
        }

        return new MatcherResult(
            true,
            FinalMessageFor(
                () => $"Expected no intersection, but found:\n{
                    intersectingElements.Stringify()
                }\nwhen comparing\n{
                    actual.Stringify()
                }\nwith\n{
                    expected.Stringify()
                }",
                customMessageGenerator
            )
        );
    }
}