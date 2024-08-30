using System.Collections.Generic;
using Imported.PeanutButter.Utils;
using NExpect.Interfaces;

namespace NExpect;

/// <summary>
/// Provides mechanisms for performing
/// Deep/Intersection equality testing with
/// explicitly-ignored properties
/// </summary>
public static class PartialDeepEqualityLogic
{
    private const string DEEP_EQUALITY_PROPERTY_IGNORE_LIST =
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
    /// equality testing (alias for Without)
    /// </summary>
    /// <param name="expectation"></param>
    /// <param name="propertyNames"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IExpectation<T> Omitting<T>(
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
    /// equality testing (alias for Omitting)
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
    /// equality testing (alias for Without)
    /// </summary>
    /// <param name="collectionExpectation"></param>
    /// <param name="propertyNames"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionExpectation<T> Omitting<T>(
        this ICollectionExpectation<T> collectionExpectation,
        params string[] propertyNames
    )
    {
        var actual = collectionExpectation.Actual;
        AddIgnores(actual, propertyNames);
        return collectionExpectation;
    }

    /// <summary>
    /// Specify properties to omit from deep/intersection
    /// equality testing (alias for Omitting)
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