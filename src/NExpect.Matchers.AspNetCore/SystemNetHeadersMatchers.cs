using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Imported.PeanutButter.Utils;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides matchers for HttpRequestHeaders
/// </summary>
public static class SystemNetHeadersMatchers
{
    /// <summary>
    /// Tests the contents of the header collections to
    /// assert that they contain the same data
    /// </summary>
    /// <param name="to"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ICollectionMore<KeyValuePair<string, string[]>> Equal(
        this ICollectionTo<KeyValuePair<string, string[]>> to,
        HttpRequestHeaders other
    )
    {
        return to.Equal(
            other,
            NULL_STRING
        );
    }

    /// <summary>
    /// Tests the contents of the header collections to
    /// assert that they contain the same data
    /// </summary>
    /// <param name="to"></param>
    /// <param name="other"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static ICollectionMore<KeyValuePair<string, string[]>> Equal(
        this ICollectionTo<KeyValuePair<string, string[]>> to,
        HttpRequestHeaders other,
        string customMessage
    )
    {
        return to.Equal(
            other,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests the contents of the header collections to
    /// assert that they contain the same data
    /// </summary>
    /// <param name="to"></param>
    /// <param name="other"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static ICollectionMore<KeyValuePair<string, string[]>> Equal(
        this ICollectionTo<KeyValuePair<string, string[]>> to,
        HttpRequestHeaders other,
        Func<string> customMessageGenerator
    )
    {
        var result = to.AddMatcher(
            actual =>
            {
                var otherData = other?.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.ToArray(),
                    StringComparer.OrdinalIgnoreCase
                ) ?? new();

                return TestHeaderCollection(
                    actual,
                    otherData,
                    customMessageGenerator
                );
            }
        );
        return result;
    }

    private static IMatcherResult TestHeaderCollection(
        IEnumerable<KeyValuePair<string, string[]>> actual,
        IDictionary<string, string[]> other,
        Func<string> customMessageGenerator
    )
    {
        var actualArray = actual as KeyValuePair<string, string[]>[]
            ?? actual.ToArray<KeyValuePair<string, string[]>>();
        var otherData = other?.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.ToArray(),
            StringComparer.OrdinalIgnoreCase
        ) ?? new();
        var haveSameNumberOfItems = otherData.Count == actualArray.Length;
        var passed = haveSameNumberOfItems;
        if (passed)
        {
            foreach (var item in actualArray)
            {
                if (!otherData.TryGetValue(item.Key, out var stored))
                {
                    passed = false;
                    break;
                }

                if (stored.Length != item.Value.Length)
                {
                    passed = false;
                    break;
                }

                foreach (var subItem in item.Value)
                {
                    if (!stored.Contains(subItem))
                    {
                        passed = false;
                        break;
                    }
                }
            }
        }

        return new MatcherResult(
            passed,
            () =>
                $"Expected headers\n{actualArray.Stringify()}\n{passed.AsNot()}to match\n{otherData.Stringify()}",
            customMessageGenerator
        );
    }

    /// <summary>
    /// Convenience: test the contents of http request headers
    /// against a simpler Dictionary&lt;string, string&gt;
    /// - most of the time, headers only have one value
    /// - most of the time, that's a convenient way to work with them
    /// - we can assume equivalence if all the keys &amp; values match and
    ///   the target doesn't have multiple values per key
    /// </summary>
    /// <param name="to"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ICollectionMore<KeyValuePair<string, string[]>> Equal(
        this ICollectionTo<KeyValuePair<string, string[]>> to,
        IDictionary<string, string> other
    )
    {
        return to.Equal(
            other,
            NULL_STRING
        );
    }

    /// <summary>
    /// Convenience: test the contents of http request headers
    /// against a simpler Dictionary&lt;string, string&gt;
    /// - most of the time, headers only have one value
    /// - most of the time, that's a convenient way to work with them
    /// - we can assume equivalence if all the keys &amp; values match and
    ///   the target doesn't have multiple values per key
    /// </summary>
    /// <param name="to"></param>
    /// <param name="other"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static ICollectionMore<KeyValuePair<string, string[]>> Equal(
        this ICollectionTo<KeyValuePair<string, string[]>> to,
        IDictionary<string, string> other,
        string customMessage
    )
    {
        return to.Equal(
            other,
            () => customMessage
        );
    }


    /// <summary>
    /// Convenience: test the contents of http request headers
    /// against a simpler Dictionary&lt;string, string&gt;
    /// - most of the time, headers only have one value
    /// - most of the time, that's a convenient way to work with them
    /// - we can assume equivalence if all the keys &amp; values match and
    ///   the target doesn't have multiple values per key
    /// </summary>
    /// <param name="to"></param>
    /// <param name="other"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static ICollectionMore<KeyValuePair<string, string[]>> Equal(
        this ICollectionTo<KeyValuePair<string, string[]>> to,
        IDictionary<string, string> other,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
            actual =>
            {
                var copy = other.ToDictionary(
                    o => o.Key,
                    o => o.Value.InArray()
                );
                return TestHeaderCollection(
                    actual,
                    copy,
                    customMessageGenerator
                );
            }
        );
    }


    /// <summary>
    /// Asserts equality of content between two sets of http response headers
    /// </summary>
    /// <param name="to"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ICollectionMore<KeyValuePair<string, string[]>> Equal(
        this ICollectionTo<KeyValuePair<string, string[]>> to,
        HttpResponseHeaders other
    )
    {
        return to.Equal(
            other,
            NULL_STRING
        );
    }

    /// <summary>
    /// Asserts equality of content between two sets of http response headers
    /// </summary>
    /// <param name="to"></param>
    /// <param name="other"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static ICollectionMore<KeyValuePair<string, string[]>> Equal(
        this ICollectionTo<KeyValuePair<string, string[]>> to,
        HttpResponseHeaders other,
        string customMessage
    )
    {
        return to.Equal(
            other,
            () => customMessage
        );
    }

    /// <summary>
    /// Asserts equality of content between two sets of http response headers
    /// </summary>
    /// <param name="to"></param>
    /// <param name="other"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static ICollectionMore<KeyValuePair<string, string[]>> Equal(
        this ICollectionTo<KeyValuePair<string, string[]>> to,
        HttpResponseHeaders other,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
            actual =>
            {
                var otherData = other?.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.ToArray(),
                    StringComparer.OrdinalIgnoreCase
                ) ?? new();

                return TestHeaderCollection(
                    actual,
                    otherData,
                    customMessageGenerator
                );
            }
        );
    }
}