using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using Imported.PeanutButter.TestUtils.AspNetCore;
using Imported.PeanutButter.Utils;

namespace NExpect
{
    /// <summary>
    /// Provides matchers for HttpResponses
    /// </summary>
    public static class HttpResponseMatchers
    {
        /// <summary>
        /// Tests if an HttpResponse contains the Set-Cookie
        /// header that would set the cookie with the provided name
        /// </summary>
        /// <param name="have"></param>
        /// <param name="name"></param>
        /// <returns>Continuation to further test the cookie, if found</returns>
        public static IMore<Cookie> Cookie(
            this IHave<HttpResponse> have,
            string name
        )
        {
            return have.Cookie(name, MessageHelpers.NULL_STRING);
        }

        /// <summary>
        /// Tests if an HttpResponse contains the Set-Cookie
        /// header that would set the cookie with the provided name
        /// </summary>
        /// <param name="have"></param>
        /// <param name="name"></param>
        /// <param name="customMessage"></param>
        /// <returns>Continuation to further test the cookie, if found</returns>
        public static IMore<Cookie> Cookie(
            this IHave<HttpResponse> have,
            string name,
            string customMessage
        )
        {
            return have.Cookie(name, () => customMessage);
        }

        /// <summary>
        /// Tests if an HttpResponse contains the Set-Cookie
        /// header that would set the cookie with the provided name
        /// </summary>
        /// <param name="have"></param>
        /// <param name="name"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns>Continuation to further test the cookie, if found</returns>
        public static IMore<Cookie> Cookie(
            this IHave<HttpResponse> have,
            string name,
            Func<string> customMessageGenerator
        )
        {
            Cookie resolvedCookie = null;
            have.AddMatcher(actual =>
            {
                var cookies = actual.Headers.ParseCookies();
                var encodedName = name;
                resolvedCookie = cookies.FirstOrDefault(c => c.Name.Equals(encodedName));
                resolvedCookie?.SetMetadata("response", actual);

                var passed = resolvedCookie is not null;
                return new MatcherResult(
                    passed,
                    MessageHelpers.FinalMessageFor(
                        () => $"Expected {passed.AsNot()}to find set-cookie header for '{encodedName}'",
                        customMessageGenerator
                    )
                );
            });
            return new Next<Cookie>(
                () => resolvedCookie,
                have as IExpectationContext
            );
        }

        private static string Name(this Cookie cookie)
        {
            return cookie?.Name ?? "unknown cookie";
        }
    }
}