﻿using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Helpers;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using static NExpect.Helpers.DeepTestHelpers;
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect;

/// <summary>
/// Provides matchers for testing collection deep equivalence
/// </summary>
// ReSharper disable once UnusedMember.Global
public static class CollectionDeepEquivalenceMatchers
{
    /// <summary>
    /// Does deep-equivalence testing on two collections, ignoring complex item referencing.
    /// Two collections are deep-equivalent when their object data matches, but not necessarily
    /// in order.
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Collection to match</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Collection item type</typeparam>
    public static ICollectionMore<T> To<T>(
        this ICollectionDeepEquivalent<T> continuation,
        IEnumerable<T> expected,
        params object[] customEqualityComparers
    )
    {
        return continuation.To(expected, NULL_STRING, customEqualityComparers);
    }

    /// <summary>
    /// Does deep-equivalence testing on two collections, ignoring complex item referencing.
    /// Two collections are deep-equivalent when their object data matches, but not necessarily
    /// in order.
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Collection to match</param>
    /// <param name="customMessage">Custom message to add when failing</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Collection item type</typeparam>
    public static ICollectionMore<T> To<T>(
        this ICollectionDeepEquivalent<T> continuation,
        IEnumerable<T> expected,
        string customMessage,
        params object[] customEqualityComparers
    )
    {
        return continuation.To(expected, () => customMessage, customEqualityComparers);
    }

    /// <summary>
    /// Does deep-equivalence testing on two collections, ignoring complex item referencing.
    /// Two collections are deep-equivalent when their object data matches, but not necessarily
    /// in order.
    /// Hint: if the collections are of disparate type, try using
    /// `Expect(left).As.Objects.To.Intersection.Equal(right)`
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Collection to match</param>
    /// <param name="customMessageGenerator">Generates a custom message to add when failing</param>
    /// <param name="customEqualityComparers">Custom implementations of IEqualityComparer&lt;TProperty&gt;
    /// to use when comparing properties of type TProperty</param>
    /// <typeparam name="T">Collection item type</typeparam>
    public static ICollectionMore<T> To<T>(
        this ICollectionDeepEquivalent<T> continuation,
        IEnumerable<T> expected,
        Func<string> customMessageGenerator,
        params object[] customEqualityComparers
    )
    {
        return continuation.AddMatcher(
            collection =>
            {
                var result = CollectionsAreDeepEquivalent(
                    collection, expected, customEqualityComparers);
                return new MatcherResult(
                    result.AreEqual,
                    () => FinalMessageFor(
                        new[]
                        {
                            "Expected",
                            collection.LimitedPrint(),
                            $"{result.AreEqual.AsNot()} to be deep equivalent to",
                            expected.LimitedPrint()
                        }.Concat(result.Errors).ToArray(),
                        customMessageGenerator
                    ));
            });
    }

    private static DeepTestResult CollectionsAreDeepEquivalent<T>(
        IEnumerable<T> collection,
        IEnumerable<T> expected,
        object[] customEqualityComparers)
    {
        return CollectionCompare(
            collection,
            expected,
            (master, compare) =>
            {
                while (master.Any())
                {
                    var currentMaster = master.First();
                    // ReSharper disable once RedundantNameQualifier
                    if (object.Equals(default(T), currentMaster))
                    {
                        var foundNullMatch = false;
                        for (var i = 0; i < compare.Count; i++)
                        {
                            var item = compare[i] as object;
                            // ReSharper disable once RedundantNameQualifier
                            if (object.Equals(default(T), item))
                            {
                                compare.RemoveAt(i);
                                master.Remove(default);
                                foundNullMatch = true;
                                break;
                            }
                        }
                        if (foundNullMatch)
                        {
                            continue;
                        }

                        return new DeepTestResult(false, $"no match for {default(T)}");
                    }

                    var compareMatch = compare.FirstOrDefault(
                        c => AreDeepEqual(currentMaster, c, customEqualityComparers).AreEqual);
                    if (compareMatch == null)
                    {
                        return new DeepTestResult(false, $"no match for item {currentMaster.Stringify()}");
                    }

                    master.Remove(currentMaster);
                    compare.Remove(compareMatch);
                }

                return DeepTestResult.Pass;
            });
    }
}