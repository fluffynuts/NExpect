using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Helpers;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.Helpers.DeepTestHelpers;
using static Imported.PeanutButter.Utils.PyLike;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect;

/// <summary>
/// Provides extensions for intersection equality testing on collections
/// </summary>
public static class CollectionIntersectionEqualityExtensions
{
    /// <summary>
    /// Does intersection-equality testing on two collections, ignoring complex item referencing
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Collection to match</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Collection item type</typeparam>
    public static ICollectionMore<T> Equal<T>(
        this ICollectionIntersection<T> continuation,
        IEnumerable<T> expected,
        params object[] customEqualityComparers
    )
    {
        return continuation.Equal(
            expected,
            NULL_STRING,
            customEqualityComparers
        );
    }

    /// <summary>
    /// Does intersection-equality testing on two collections, ignoring complex item referencing
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Collection to match</param>
    /// <param name="customMessage">Custom message to add when failing</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Collection item type</typeparam>
    public static ICollectionMore<T> Equal<T>(
        this ICollectionIntersection<T> continuation,
        IEnumerable<T> expected,
        string customMessage,
        params object[] customEqualityComparers
    )
    {
        return continuation.Equal(
            expected,
            () => customMessage,
            customEqualityComparers
        );
    }

    /// <summary>
    /// Does intersection-equality testing on two collections, ignoring complex item referencing
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Collection to match</param>
    /// <param name="customMessageGenerator">Generates a custom message to add when failing</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Collection item type</typeparam>
    public static ICollectionMore<T> Equal<T>(
        this ICollectionIntersection<T> continuation,
        IEnumerable<T> expected,
        Func<string> customMessageGenerator,
        params object[] customEqualityComparers
    )
    {
        return continuation.AddMatcher(
            MakeCollectionIntersectionEqualMatcherFor(
                expected,
                customMessageGenerator,
                customEqualityComparers
            )
        );
    }

    /// <summary>
    /// Performs intersection-equality testing on two collections
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected collection values</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Type of collection item</typeparam>
    public static ICollectionMore<T> To<T>(
        this ICollectionIntersectionEqual<T> continuation,
        IEnumerable<T> expected,
        params object[] customEqualityComparers
    )
    {
        return continuation.To(expected, NULL_STRING, customEqualityComparers);
    }

    /// <summary>
    /// Performs intersection-equality testing on two collections
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected collection values</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Type of collection item</typeparam>
    public static ICollectionMore<T> To<T>(
        this ICollectionIntersectionEqual<T> continuation,
        IEnumerable<T> expected,
        string customMessage,
        params object[] customEqualityComparers
    )
    {
        return continuation.To(expected, () => customMessage, customEqualityComparers);
    }

    /// <summary>
    /// Performs intersection-equality testing on two collections
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected collection values</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Type of collection item</typeparam>
    public static ICollectionMore<T> To<T>(
        this ICollectionIntersectionEqual<T> continuation,
        IEnumerable<T> expected,
        Func<string> customMessageGenerator,
        params object[] customEqualityComparers
    )
    {
        return continuation.AddMatcher(
            MakeCollectionIntersectionEqualMatcherFor(
                expected,
                customMessageGenerator,
                customEqualityComparers
            )
        );
    }


    private static Func<IEnumerable<T>, IMatcherResult> MakeCollectionIntersectionEqualMatcherFor<T>(
        IEnumerable<T> expected,
        Func<string> customMessage,
        params object[] customEqualityComparers
    )
    {
        return collection =>
        {
            var result = CollectionsAreIntersectionEqual(
                collection,
                expected,
                customEqualityComparers
            );
            return new MatcherResult(
                result.AreEqual,
                FinalMessageFor(
                    () => new[]
                    {
                        "Expected",
                        collection.LimitedPrint(),
                        $"{result.AreEqual.AsNot()} to intersect equal",
                        expected.LimitedPrint()
                    }.Concat(result.Errors).ToArray(),
                    customMessage
                )
            );
        };
    }

    private static DeepTestResult CollectionsAreIntersectionEqual<T>(
        IEnumerable<T> collection,
        IEnumerable<T> expected,
        params object[] customEqualityComparers
    )
    {
        return CollectionCompare(
            collection,
            expected,
            (master, compare) => Zip(master, compare)
                .Aggregate(
                    null as DeepTestResult,
                    (acc, cur) =>
                    {
                        if (acc != null)
                        {
                            return acc;
                        }

                        var result = AreIntersectionEqual(
                            cur.Item1,
                            cur.Item2,
                            customEqualityComparers
                        );
                        return result.AreEqual
                            ? null
                            : result;
                    }
                )
        ) ?? DeepTestResult.Pass;
    }
}