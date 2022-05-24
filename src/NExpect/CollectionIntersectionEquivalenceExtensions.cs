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

namespace NExpect;

/// <summary>
/// Provides extensions to perform collection intersection equivalence assertions
/// </summary>
public static class CollectionIntersectionEquivalenceExtensions
{
    /// <summary>
    /// Provides deep intersection-equality testing for two collections
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected values</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Original type of collection</typeparam>
    public static IMore<IEnumerable<T>> To<T>(
        this ICollectionIntersectionEquivalent<T> continuation,
        IEnumerable<T> expected,
        params object[] customEqualityComparers
    )
    {
        return continuation.To(expected, NULL_STRING, customEqualityComparers);
    }

    /// <summary>
    /// Provides deep intersection-equality testing for two collections
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected values</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Original type of collection</typeparam>
    public static IMore<IEnumerable<T>> To<T>(
        this ICollectionIntersectionEquivalent<T> continuation,
        IEnumerable<T> expected,
        string customMessage,
        params object[] customEqualityComparers
    )
    {
        return continuation.To(expected, () => customMessage, customEqualityComparers);
    }

    /// <summary>
    /// Provides deep intersection-equality testing for two collections
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Expected values</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Original type of collection</typeparam>
    public static IMore<IEnumerable<T>> To<T>(
        this ICollectionIntersectionEquivalent<T> continuation,
        IEnumerable<T> expected,
        Func<string> customMessageGenerator,
        params object[] customEqualityComparers
    )
    {
        return continuation.AddMatcher(
            collection =>
            {
                var actualArray = collection as T[] ?? collection.ToArray();
                var expectedArray = expected as T[] ?? expected.ToArray();
                var result = CollectionsAreIntersectionEquivalent(
                    actualArray,
                    expectedArray,
                    customEqualityComparers);
                return new MatcherResult(
                    result.AreEqual,
                    FinalMessageFor(
                        () => new[]
                        {
                            "Expected",
                            actualArray.LimitedPrint(),
                            $"{result.AreEqual.AsNot()} to be intersection equivalent to",
                            expectedArray.LimitedPrint()
                        }.Concat(result.Errors).ToArray(),
                        customMessageGenerator
                    )
                );
            });
    }

    private static DeepTestResult CollectionsAreIntersectionEquivalent<T>(
        IEnumerable<T> collection,
        IEnumerable<T> expected,
        object[] customEqualityMatchers)
    {
        return CollectionCompare(
            collection,
            expected,
            (master, compare) =>
            {
                while (master.Any())
                {
                    var currentMaster = master.First();
                    var compareMatch =
                        compare.FirstOrDefault(
                            c => AreIntersectionEqual(
                                currentMaster,
                                c,
                                customEqualityMatchers).AreEqual);
                    // TODO: add information about mismatch
                    if (compareMatch == null)
                    {
                        return DeepTestResult.Fail(
                            $"Found no match for item\n{currentMaster.Stringify()}"
                        );
                    }

                    master.Remove(currentMaster);
                    compare.Remove(compareMatch);
                }

                return DeepTestResult.Pass;
            });
    }
}