using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;

namespace NExpect;

/// <summary>
/// Direction to select when testing ordering with Order.By
/// </summary>
public enum Direction
{
    /// <summary>
    /// Order should be ascending
    /// </summary>
    Ascending,

    /// <summary>
    /// Order should be descending
    /// </summary>
    Descending
}

/// <summary>
/// Provides matchers for asserting the order of collections
/// </summary>
public static class CollectionOrderMatchers
{
    /// <summary>
    /// Asserts that the given collection is ordered ascending with the
    /// default comparer for T. A collection with less than 2 items will
    /// always fail.
    /// </summary>
    /// <param name="collectionOrdered">Continuation</param>
    /// <typeparam name="T">Collection item type</typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Ascending<T>(
        this ICollectionOrdered<T> collectionOrdered
    )
    {
        return collectionOrdered.Ascending(
            Comparer<T>.Default
        );
    }

    /// <summary>
    /// Asserts that the given collection is ordered ascending with the
    /// provided comparer for T. A collection with less than 2 items will
    /// always fail.
    /// </summary>
    /// <param name="collectionOrdered">Continuation</param>
    /// <param name="comparer">Comparer for two items of type T,
    /// implementing IComparer&lt;T&gt;</param>
    /// <typeparam name="T">Collection item type</typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Ascending<T>(
        this ICollectionOrdered<T> collectionOrdered,
        IComparer<T> comparer
    )
    {
        return collectionOrdered.Ascending(comparer, NULL_STRING);
    }

    /// <summary>
    /// Asserts that the given collection is ordered ascending with the
    /// default comparer for T. A collection with less than 2 items will
    /// always fail.
    /// </summary>
    /// <param name="collectionOrdered">Continuation</param>
    /// <param name="customMessage">Custom message to include when assertion fails</param>
    /// <typeparam name="T">Collection item type</typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Ascending<T>(
        this ICollectionOrdered<T> collectionOrdered,
        string customMessage)
    {
        return collectionOrdered.Ascending(
            Comparer<T>.Default,
            customMessage
        );
    }

    /// <summary>
    /// Asserts that the given collection is ordered ascending with the
    /// provided comparer for T. A collection with less than 2 items will
    /// always fail.
    /// </summary>
    /// <param name="collectionOrdered">Continuation</param>
    /// <param name="comparer">Comparer for two items of type T,
    /// implementing IComparer&lt;T&gt;</param>
    /// <param name="customMessage">Custom message to include when assertion fails</param>
    /// <typeparam name="T">Collection item type</typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Ascending<T>(
        this ICollectionOrdered<T> collectionOrdered,
        IComparer<T> comparer,
        string customMessage
    )
    {
        return collectionOrdered.Ascending(
            comparer,
            () => customMessage
        );
    }

    /// <summary>
    /// Asserts that the given collection is ordered ascending with the
    /// default comparer for T. A collection with less than 2 items will
    /// always fail.
    /// </summary>
    /// <param name="collectionOrdered">Continuation</param>
    /// <param name="customMessageGenerator">
    /// Generates a custom message to include when assertion fails</param>
    /// <typeparam name="T">Collection item type</typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Ascending<T>(
        this ICollectionOrdered<T> collectionOrdered,
        Func<string> customMessageGenerator
    )
    {
        return collectionOrdered.Ascending(
            Comparer<T>.Default,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Asserts that the given collection is ordered ascending with the
    /// provided comparer for T. A collection with less than 2 items will
    /// always fail.
    /// </summary>
    /// <param name="collectionOrdered">Continuation</param>
    /// <param name="comparer">Comparer for two items of type T,
    /// implementing IComparer&lt;T&gt;</param>
    /// <param name="customMessageGenerator">
    /// Generates a custom message to include when assertion fails</param>
    /// <typeparam name="T">Collection item type</typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Ascending<T>(
        this ICollectionOrdered<T> collectionOrdered,
        IComparer<T> comparer,
        Func<string> customMessageGenerator
    )
    {
        return collectionOrdered.AddMatcher(
            actual =>
            {
                return TestOrderingOf(
                    actual,
                    comparer,
                    i => i > 0,
                    customMessageGenerator
                );
            });
    }


    // -> descending start
    /// <summary>
    /// Asserts that the given collection is ordered descending with the
    /// default comparer for T. A collection with less than 2 items will
    /// always fail.
    /// </summary>
    /// <param name="collectionOrdered">Continuation</param>
    /// <typeparam name="T">Collection item type</typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Descending<T>(
        this ICollectionOrdered<T> collectionOrdered
    )
    {
        return collectionOrdered.Descending(
            Comparer<T>.Default
        );
    }

    /// <summary>
    /// Asserts that the given collection is ordered descending with the
    /// provided comparer for T. A collection with less than 2 items will
    /// always fail.
    /// </summary>
    /// <param name="collectionOrdered">Continuation</param>
    /// <param name="comparer">Comparer for two items of type T,
    /// implementing IComparer&lt;T&gt;</param>
    /// <typeparam name="T">Collection item type</typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Descending<T>(
        this ICollectionOrdered<T> collectionOrdered,
        IComparer<T> comparer
    )
    {
        return collectionOrdered.Descending(comparer, NULL_STRING);
    }

    /// <summary>
    /// Asserts that the given collection is ordered descending with the
    /// default comparer for T. A collection with less than 2 items will
    /// always fail.
    /// </summary>
    /// <param name="collectionOrdered">Continuation</param>
    /// <param name="customMessage">Custom message to include when assertion fails</param>
    /// <typeparam name="T">Collection item type</typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Descending<T>(
        this ICollectionOrdered<T> collectionOrdered,
        string customMessage)
    {
        return collectionOrdered.Descending(
            Comparer<T>.Default,
            customMessage
        );
    }

    /// <summary>
    /// Asserts that the given collection is ordered descending with the
    /// provided comparer for T. A collection with less than 2 items will
    /// always fail.
    /// </summary>
    /// <param name="collectionOrdered">Continuation</param>
    /// <param name="comparer">Comparer for two items of type T,
    /// implementing IComparer&lt;T&gt;</param>
    /// <param name="customMessage">Custom message to include when assertion fails</param>
    /// <typeparam name="T">Collection item type</typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Descending<T>(
        this ICollectionOrdered<T> collectionOrdered,
        IComparer<T> comparer,
        string customMessage
    )
    {
        return collectionOrdered.Descending(
            comparer,
            () => customMessage
        );
    }

    /// <summary>
    /// Asserts that the given collection is ordered descending with the
    /// default comparer for T. A collection with less than 2 items will
    /// always fail.
    /// </summary>
    /// <param name="collectionOrdered">Continuation</param>
    /// <param name="customMessageGenerator">
    /// Generates a custom message to include when assertion fails</param>
    /// <typeparam name="T">Collection item type</typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Descending<T>(
        this ICollectionOrdered<T> collectionOrdered,
        Func<string> customMessageGenerator
    )
    {
        return collectionOrdered.Descending(
            Comparer<T>.Default,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Asserts that the given collection is ordered descending with the
    /// provided comparer for T. A collection with less than 2 items will
    /// always fail.
    /// </summary>
    /// <param name="collectionOrdered">Continuation</param>
    /// <param name="comparer">Comparer for two items of type T,
    /// implementing IComparer&lt;T&gt;</param>
    /// <param name="customMessageGenerator">
    /// Generates a custom message to include when assertion fails</param>
    /// <typeparam name="T">Collection item type</typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> Descending<T>(
        this ICollectionOrdered<T> collectionOrdered,
        IComparer<T> comparer,
        Func<string> customMessageGenerator
    )
    {
        return collectionOrdered.AddMatcher(
            actual =>
            {
                return TestOrderingOf(
                    actual,
                    comparer,
                    i => i < 0,
                    customMessageGenerator
                );
            });
    }

    /// <summary>
    /// Tests if the collection is ordered ascending
    /// by the property exposed by the selector
    /// </summary>
    /// <param name="collectionOrdered"></param>
    /// <param name="selector"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> By<T>(
        this ICollectionOrdered<T> collectionOrdered,
        Expression<Func<T, object>> selector
    )
    {
        return collectionOrdered.By(
            selector,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests if the collection is ordered ascending
    /// by the property exposed by the selector
    /// </summary>
    /// <param name="collectionOrdered"></param>
    /// <param name="selector"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> By<T>(
        this ICollectionOrdered<T> collectionOrdered,
        Expression<Func<T, object>> selector,
        string customMessage
    )
    {
        return collectionOrdered.By(
            selector,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if the collection is ordered ascending
    /// by the property exposed by the selector
    /// </summary>
    /// <param name="collectionOrdered"></param>
    /// <param name="selector"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> By<T>(
        this ICollectionOrdered<T> collectionOrdered,
        Expression<Func<T, object>> selector,
        Func<string> customMessageGenerator
    )
    {
        return collectionOrdered.By(
            selector,
            Direction.Ascending,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Tests if the collection is ordered in the given direction
    /// by the property exposed by the selector
    /// </summary>
    /// <param name="collectionOrdered"></param>
    /// <param name="selector"></param>
    /// <param name="direction"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> By<T>(
        this ICollectionOrdered<T> collectionOrdered,
        Expression<Func<T, object>> selector,
        Direction direction
    )
    {
        return collectionOrdered.By(
            selector,
            direction,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests if the collection is ordered in the given direction
    /// by the property exposed by the selector
    /// </summary>
    /// <param name="collectionOrdered"></param>
    /// <param name="selector"></param>
    /// <param name="direction"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> By<T>(
        this ICollectionOrdered<T> collectionOrdered,
        Expression<Func<T, object>> selector,
        Direction direction,
        string customMessage
    )
    {
        return collectionOrdered.By(
            selector,
            direction,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if the collection is ordered in the given direction
    /// by the property exposed by the selector
    /// </summary>
    /// <param name="collectionOrdered"></param>
    /// <param name="selector"></param>
    /// <param name="direction"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<IEnumerable<T>> By<T>(
        this ICollectionOrdered<T> collectionOrdered,
        Expression<Func<T, object>> selector,
        Direction direction,
        Func<string> customMessageGenerator
    )
    {
        return collectionOrdered.AddMatcher(actual =>
        {
            if (actual is null)
            {
                return new EnforcedMatcherResult(
                    false,
                    () => "Cannot test ordering on a null collection"
                );
            }

            var passed = true;
            using var enumerator = actual.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return new EnforcedMatcherResult(
                    false,
                    () => "Cannot test ordering on an empty collection"
                );
            }

            var sel = selector.Compile();
            var lastValue = sel(enumerator.Current);
            Func<int, bool> outOfOrder = direction == Direction.Ascending
                ? i => i > 0
                : i => i < 0;
            ComparerWrapper comparer = null;
            do
            {
                var currentValue = sel(enumerator.Current);
                var comparisonResult = CompareWithDefaultComparer(lastValue, currentValue, ref comparer);
                if (outOfOrder(comparisonResult))
                {
                    passed = false;
                    break;
                }

                lastValue = currentValue;
            } while (enumerator.MoveNext());

            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () => $@"Expected collection {
                        passed.AsNot()
                    }to be ordered {
                        direction.ToString().ToLower()
                    } by: [{selector}], which produces
{actual.Select(sel).Stringify<IEnumerable<object>>()}",
                    customMessageGenerator
                )
            );
        });
    }

    private static bool TestOrdering<T>(
        Expression<Func<T, object>> selector,
        IEnumerator<T> enumerator,
        Direction direction
        )
    {
        var passed = false;
        var sel = selector.Compile();
        var lastValue = sel(enumerator.Current);
        Func<int, bool> outOfOrder = direction == Direction.Ascending
            ? i => i > 0
            : i => i < 0;
        ComparerWrapper comparer = null;
        while(enumerator.MoveNext())
        {
            passed = true;
            var currentValue = sel(enumerator.Current);
            var comparisonResult = CompareWithDefaultComparer(lastValue, currentValue, ref comparer);
            if (outOfOrder(comparisonResult))
            {
                passed = false;
                break;
            }

            lastValue = currentValue;
        }
        
        return passed;
    }


    private static int CompareWithDefaultComparer(
        object lastValue,
        object currentValue,
        ref ComparerWrapper comparer
    )
    {
        if (lastValue is null && currentValue is null)
        {
            return 0;
        }

        if (lastValue is null)
        {
            return -1;
        }

        if (currentValue is null)
        {
            return 1;
        }

        comparer ??= new ComparerWrapper(currentValue.GetType());
        return comparer.Compare(lastValue, currentValue);
    }

    private class ComparerWrapper
    {
        public ComparerWrapper(Type forType)
        {
            var comparerType = GenericComparerType.MakeGenericType(forType);
            var prop = comparerType.GetProperty(
                nameof(Comparer<int>.Default),
                BindingFlags.Static | BindingFlags.Public
            );
            _actualComparer = prop?.GetValue(null)
                ?? throw new ArgumentException($"No Default comparer on Comparer<{forType}>");
            _compareMethod = comparerType.GetMethod(
                nameof(Comparer<int>.Default.Compare),
                BindingFlags.Public | BindingFlags.Instance
            );
        }

        public int Compare(object left, object right)
        {
            return (int) _compareMethod.Invoke(_actualComparer, new[] { left, right });
        }

        private static readonly Type GenericComparerType = typeof(Comparer<>);
        private readonly object _actualComparer;
        private readonly MethodInfo _compareMethod;
    }

    private static MatcherResult TestOrderingOf<T>(
        IEnumerable<T> actual,
        IComparer<T> comparer,
        Func<int, bool> failsWhen,
        Func<string> customMessageGenerator
    )
    {
        var itemCount = 0;
        var last = default(T);
        foreach (var item in actual)
        {
            if (itemCount == 0)
            {
                last = item;
                itemCount++;
                continue;
            }

            var thisResult = comparer.Compare(last, item);
            if (failsWhen(thisResult))
            {
                return new MatcherResult(
                    false,
                    FinalMessageFor(
                        () => $"Expected collection {false.AsNot()}to be ordered ascending",
                        customMessageGenerator
                    )
                );
            }

            last = item;
            itemCount++;
        }

        if (itemCount < 2)
        {
            var context = actual.GetMetadata<IExpectationContext>(Expectations.METADATA_KEY);
            return new MatcherResult(
                context.IsNegated(),
                "Ordering expectations require collections containing at least two items"
            );
        }

        return new MatcherResult(
            true,
            FinalMessageFor(
                () => $"Expected collection {true.AsNot()}to be ordered ascending",
                customMessageGenerator
            )
        );
    }
}