using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Helpers;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.Helpers.DeepTestHelpers;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect;

/// <summary>
/// Provides matchers for performing deep equality testing on collections
/// </summary>
// ReSharper disable once UnusedMember.Global
public static class CollectionDeepEqualityMatchers
{
    /// <summary>
    /// Does deep-equality testing on two collections, ignoring complex item referencing
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Collection to match</param>
    /// <typeparam name="T">Collection item type</typeparam>
    public static IMore<IEnumerable<T>> Equal<T>(
        this ICollectionDeep<T> continuation,
        IEnumerable<T> expected
    )
    {
        return continuation.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Does deep-equality testing on two collections, ignoring complex item referencing
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Collection to match</param>
    /// <param name="customMessage">Custom message to add when failing</param>
    /// <typeparam name="T">Collection item type</typeparam>
    public static IMore<IEnumerable<T>> Equal<T>(
        this ICollectionDeep<T> continuation,
        IEnumerable<T> expected,
        string customMessage
    )
    {
        return continuation.Equal(expected, () => customMessage);
    }

    /// <summary>
    /// Does deep-equality testing on two collections, ignoring complex item referencing
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Collection to match</param>
    /// <param name="customMessageGenerator">Generates a custom message to add when failing</param>
    /// <typeparam name="T">Collection item type</typeparam>
    public static IMore<IEnumerable<T>> Equal<T>(
        this ICollectionDeep<T> continuation,
        IEnumerable<T> expected,
        Func<string> customMessageGenerator
    )
    {
        return continuation.AddMatcher(
            MakeCollectionDeepEqualMatcherFor(expected, customMessageGenerator)
        );
    }

    /// <summary>
    /// Does deep-equality testing on two collections, ignoring complex item referencing
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Collection to match</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Collection item type</typeparam>
    public static IMore<IEnumerable<T>> To<T>(
        this ICollectionDeepEqual<T> continuation,
        IEnumerable<T> expected,
        params object[] customEqualityComparers
    )
    {
        return continuation.To(expected, NULL_STRING, customEqualityComparers);
    }

    /// <summary>
    /// Does deep-equality testing on two collections, ignoring complex item referencing
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Collection to match</param>
    /// <param name="customMessage">Custom message to add when failing</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Collection item type</typeparam>
    public static IMore<IEnumerable<T>> To<T>(
        this ICollectionDeepEqual<T> continuation,
        IEnumerable<T> expected,
        string customMessage,
        params object[] customEqualityComparers
    )
    {
        return continuation.To(expected, () => customMessage, customEqualityComparers);
    }

    /// <summary>
    /// Does deep-equality testing on two collections, ignoring complex item referencing
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Collection to match</param>
    /// <param name="customMessage">Generates a custom message to add when failing</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Collection item type</typeparam>
    public static IMore<IEnumerable<T>> To<T>(
        this ICollectionDeepEqual<T> continuation,
        IEnumerable<T> expected,
        Func<string> customMessage,
        params object[] customEqualityComparers
    )
    {
        return continuation.AddMatcher(
            MakeCollectionDeepEqualMatcherFor(
                expected,
                customMessage,
                customEqualityComparers));
    }

    private static Func<IEnumerable<T>, IMatcherResult> MakeCollectionDeepEqualMatcherFor<T>(
        IEnumerable<T> expected,
        Func<string> customMessage,
        params object[] customEqualityComparers)
    {
        return collection =>
        {
            var result = CollectionsAreDeepEqual(collection, expected, customEqualityComparers);
            return new MatcherResult(
                result.AreEqual,
                FinalMessageFor(
                    () => new[]
                    {
                        "Expected",
                        collection.LimitedPrint(),
                        $"{result.AreEqual.AsNot()}to deep equal",
                        expected.LimitedPrint()
                    }.Concat(result.Errors).ToArray(),
                    customMessage
                )
            );
        };
    }

    private static DeepTestResult CollectionsAreDeepEqual<T>(
        IEnumerable<T> collection,
        IEnumerable<T> expected,
        object[] customEqualityComparers
    )
    {
        if (collection == null && expected == null)
        {
            return DeepTestResult.Pass;
        }

        if (collection == null || expected == null)
        {
            return DeepTestResult.Fail(
                expected == null
                    ? $"Expected collection is null but actual is not"
                    : $"Actual collection is null but expected is not"
            );
        }

        var expectedCount = expected.Count();
        var collectionCount = collection.Count();

        if (expectedCount != collectionCount)
        {
            return DeepTestResult.Fail(
                $"Expected collection with {expectedCount} items, but got {collectionCount}"
            );
        }

        return CollectionCompare(
            collection,
            expected,
            (master, compare)
                => master.Zip(compare, Tuple.Create)
                    .Aggregate(
                        null as DeepTestResult,
                        (acc, cur) =>
                        {
                            if (acc != null)
                            {
                                return acc;
                            }

                            var result =
                                AreDeepEqual(
                                    cur.Item1, cur.Item2, customEqualityComparers);
                            return result.AreEqual ? null : result;
                        }
                    )
        ) ?? DeepTestResult.Pass;
    }
}