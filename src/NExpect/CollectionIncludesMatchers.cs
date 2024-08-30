using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect;

/// <summary>
/// Matchers for the syntax
/// Expect(collection).To.Include(item);
/// </summary>
public static class CollectionIncludesMatchers
{
    /// <summary>
    /// Tests if there is any occurrence of the provided
    /// value in the provided collection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Include<T>(
        this ICollectionTo<T> to,
        T value
    )
    {
        return to.Include(value, NULL_STRING);
    }

    /// <summary>
    /// Tests if there is any occurrence of the provided
    /// value in the provided collection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="value"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Include<T>(
        this ICollectionTo<T> to,
        T value,
        string customMessage
    )
    {
        return to.Include(value, () => customMessage);
    }

    /// <summary>
    /// Tests if there is any occurrence of the provided
    /// value in the provided collection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="value"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Include<T>(
        this ICollectionTo<T> to,
        T value,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
            actual => TestValueVsCollection(
                actual,
                value,
                ValueIsInCollection,
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Tests if there is any occurrence of the provided
    /// value in the provided collection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Include<T>(
        this ICollectionToAfterNot<T> to,
        T value
    )
    {
        return to.Include(value, NULL_STRING);
    }

    /// <summary>
    /// Tests if there is any occurrence of the provided
    /// value in the provided collection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="value"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Include<T>(
        this ICollectionToAfterNot<T> to,
        T value,
        string customMessage
    )
    {
        return to.Include(value, () => customMessage);
    }

    /// <summary>
    /// Tests if there is any occurrence of the provided
    /// value in the provided collection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="value"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Include<T>(
        this ICollectionToAfterNot<T> to,
        T value,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
            actual => TestValueVsCollection(
                actual,
                value,
                ValueIsInCollection,
                customMessageGenerator
            )
        );
    }

    private static IMatcherResult TestValueVsCollection<T>(
        IEnumerable<T> actual,
        T value,
        Func<IEnumerable<T>, T, bool> passesFunc,
        Func<string> customMessageGenerator
    )
    {
        var actualArray = actual as IList<T> ?? actual?.ToArray();
        var passed = passesFunc(actualArray, value);
        return new MatcherResult(
            passed,
            FinalMessageFor(
                () => $@"Expected {
                    passed.AsNot()
                }to find value {
                    value
                } in collection:\n{actualArray.Stringify()}",
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Tests if there is any occurrence of the provided
    /// value in the provided collection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Include<T>(
        this ICollectionNotAfterTo<T> to,
        T value
    )
    {
        return to.Include(value, NULL_STRING);
    }

    /// <summary>
    /// Tests if there is any occurrence of the provided
    /// value in the provided collection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="value"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Include<T>(
        this ICollectionNotAfterTo<T> to,
        T value,
        string customMessage
    )
    {
        return to.Include(value, () => customMessage);
    }

    /// <summary>
    /// Tests if there is any occurrence of the provided
    /// value in the provided collection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="value"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> Include<T>(
        this ICollectionNotAfterTo<T> to,
        T value,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
            actual => TestValueVsCollection(
                actual,
                value,
                ValueIsInCollection,
                customMessageGenerator
            )
        );
    }

    private static bool ValueIsInCollection<T>(
        IEnumerable<T> collection,
        T value
    )
    {
        foreach (var item in collection)
        {
            if (value is null && item is null)
            {
                return true;
            }

            if (value is null || item is null)
            {
                continue;
            }

            if (value.Equals(item))
            {
                return true;
            }
        }

        return false;
    }
}