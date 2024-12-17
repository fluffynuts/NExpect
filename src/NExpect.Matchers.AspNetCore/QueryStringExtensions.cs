using System.Collections.Generic;
using System.Linq;
using System.Net;
using Imported.PeanutButter.Utils;
using Microsoft.AspNetCore.Http;

namespace NExpect;

/// <summary>
/// Extensions for AspNet QueryString objects
/// </summary>
internal static class QueryStringExtensions
{
    /// <summary>
    /// Provides a dictionary snapshot of a QueryString
    /// </summary>
    /// <param name="queryString"></param>
    /// <returns></returns>
    public static IDictionary<string, string> AsDictionary(
        this QueryString queryString
    )
    {
        if (queryString.Value is null)
        {
            return new Dictionary<string, string>();
        }

        var str = queryString.Value;
        var start = str.StartsWith("?")
            ? 1
            : 0;
        var result = new Dictionary<string, string>();
        var pairs = str.Substring(start)
            .Split('&')
            .Select(
                p =>
                {
                    var subs = p.Split('=');
                    return new KeyValuePair<string, string>(
                        WebUtility.UrlDecode(subs.First()),
                        WebUtility.UrlDecode(subs.Skip(1).JoinWith("="))
                    );
                }
            );
        foreach (var item in pairs)
        {
            if (result.TryGetValue(item.Key, out var existing))
            {
                result[item.Key] = $"{existing},{item.Value}";
                continue;
            }
            result[item.Key] = item.Value;
        }
        return result;
    }
}