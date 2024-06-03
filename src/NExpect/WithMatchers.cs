using System;
using System.Collections.Generic;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect;

/// <summary>
/// Provides some default matchers to use on .With
/// </summary>
public static class WithMatchers
{
    /// <summary>
    /// Allows performing generic property tests
    /// </summary>
    /// <param name="with"></param>
    /// <param name="propertySelector"></param>
    /// <typeparam name="TActual"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <returns></returns>
    public static IBe<TProperty> Property<TActual, TProperty>(
        this IWith<TActual> with,
        Func<TActual, TProperty> propertySelector
    )
    {
        var continuation = new Be<TProperty>(default);
        with.AddMatcher(
            actual =>
            {
                var propValue = propertySelector(actual);
                continuation.SetActual(propValue);
                var newParent = new Expectation<TProperty>(propValue);
                continuation.SetParent(newParent);
                return new MatcherResult(true);
            }
        );
        return continuation;
    }
}

/// <summary>
/// Provides mechanisms for performing
/// Deep/Intersection equality testing with
/// explicitly-ignored properties
/// </summary>
public static class PartialDeepEqualityLogic
{
    internal const string DEEP_EQUALITY_PROPERTY_IGNORE_LIST =
        "__deep_equality_property_ignore_list__";

    internal static HashSet<string> FindOrAddPropertyIgnoreListMetadata(
        this object o
    )
    {
        if (o is null)
        {
            // can't be stored anyway
            return [];
        }

        if (!o.TryGetMetadata<HashSet<string>>(
                DEEP_EQUALITY_PROPERTY_IGNORE_LIST,
                out var props
            )
           )
        {
            props = new HashSet<string>();
            o.SetMetadata(DEEP_EQUALITY_PROPERTY_IGNORE_LIST, props);
        }

        return props;
    }

    /// <summary>
    /// Specify properties to omit from deep/intersection
    /// equality testing
    /// </summary>
    /// <param name="expectation"></param>
    /// <param name="propertyNames"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IExpectation<T> Without<T>(
        this IExpectation<T> expectation,
        params string[] propertyNames
    )
    {
        var actual = expectation.Actual;
        AddIgnores(actual, propertyNames);
        return expectation;
    }

    /// <summary>
    /// Specify properties to omit from deep/intersection
    /// equality testing
    /// </summary>
    /// <param name="collectionExpectation"></param>
    /// <param name="propertyNames"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionExpectation<T> Without<T>(
        this ICollectionExpectation<T> collectionExpectation,
        params string[] propertyNames
    )
    {
        var actual = collectionExpectation.Actual;
        AddIgnores(actual, propertyNames);
        return collectionExpectation;
    }

    private static void AddIgnores(object obj, string[] ignore)
    {
        var propsList = obj.FindOrAddPropertyIgnoreListMetadata();
        propsList.AddRange(ignore);
    }
}