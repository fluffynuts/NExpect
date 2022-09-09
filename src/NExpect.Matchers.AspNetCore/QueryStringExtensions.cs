using System.Collections.Generic;
using System.Linq;
using System.Net;
using Imported.PeanutButter.Utils;
using Microsoft.AspNetCore.Http;

namespace NExpect
{
    /// <summary>
    /// Extensions for AspNet QueryString objects
    /// </summary>
    public static class QueryStringExtensions
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
            return queryString.Value.Substring(1)
                .Split('&')
                .Select(p =>
                {
                    var subs = p.Split('=');
                    return new KeyValuePair<string, string>(
                        WebUtility.UrlDecode(subs.First()),
                        WebUtility.UrlDecode(subs.Skip(1).JoinWith("="))
                    );
                }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}