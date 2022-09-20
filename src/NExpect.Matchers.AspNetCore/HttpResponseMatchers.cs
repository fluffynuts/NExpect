using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Imported.PeanutButter.Utils;
using Microsoft.AspNetCore.Http;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

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
                var cookies = actual.Headers.Where(
                        h => h.Key == "Set-Cookie"
                    ).Select(h => h.Value
                        .Select(ParseCookieHeader)
                        .SelectMany(o => o)
                    )
                    .SelectMany(o => o)
                    .ToArray();
                resolvedCookie = cookies.FirstOrDefault(c => c.Name.Equals(name));
                var passed = resolvedCookie != null;
                return new MatcherResult(
                    passed,
                    MessageHelpers.FinalMessageFor(
                        () => $"Expected {passed.AsNot()}to find set-cookie for '{name}'",
                        customMessageGenerator
                    )
                );
            });
            return new Next<Cookie>(
                () => resolvedCookie,
                have as IExpectationContext
            );
        }

        private static IEnumerable<Cookie> ParseCookieHeader(
            string header
        )
        {
            foreach (var cookiePart in header.Split(',').Select(p => p.Trim()))
            {
                var parts = cookiePart.Split(';');
                yield return parts.Aggregate(
                    new Cookie(),
                    (acc, cur) =>
                    {
                        var subs = cur.Split('=');
                        var key = subs[0].Trim();
                        var value = string.Join("=", subs.Skip(1));
                        if (string.IsNullOrWhiteSpace(acc.Name))
                        {
                            acc.Name = key;
                            acc.Value = value;
                        }
                        else
                        {
                            if (CookieMutations.TryGetValue(key, out var modifier))
                            {
                                modifier(acc, value);
                            }
                        }

                        return acc;
                    }
                );
            }
        }

        private static readonly Dictionary<string, Action<Cookie, string>>
            CookieMutations = new Dictionary<string, Action<Cookie, string>>(
                StringComparer.InvariantCultureIgnoreCase
            )
            {
                ["Expires"] = SetCookieExpiration,
                ["Max-Age"] = SetCookieMaxAge,
                ["Domain"] = SetCookieDomain,
                ["Secure"] = SetCookieSecure,
                ["HttpOnly"] = SetCookieHttpOnly,
                ["SameSite"] = SetCookieSameSite
            };

        private static void SetCookieSameSite(
            Cookie cookie,
            string value
        )
        {
            // Cookie object doesn't natively support the SameSite property, yet
            cookie.SetMetadata("SameSite", value);
        }

        private static void SetCookieHttpOnly(
            Cookie cookie,
            string value
        )
        {
            cookie.HttpOnly = true;
        }

        private static void SetCookieSecure(
            Cookie cookie,
            string value
        )
        {
            cookie.Secure = true;
        }

        private static void SetCookieDomain(
            Cookie cookie,
            string value
        )
        {
            cookie.Domain = value;
        }

        private static void SetCookieMaxAge(
            Cookie cookie,
            string value
        )
        {
            if (!int.TryParse(value, out var seconds))
            {
                throw new ArgumentException(
                    $"Unable to parse '{value}' as an integer value"
                );
            }

            cookie.Expires = DateTime.Now.AddSeconds(seconds);
            cookie.Expired = seconds < 1;
            cookie.SetMetadata("MaxAge", seconds);
        }

        private static void SetCookieExpiration(
            Cookie cookie,
            string value
        )
        {
            if (cookie.TryGetMetadata<int>("MaxAge", out _))
            {
                // Max-Age takes precedence over Expires
                return;
            }

            if (!DateTime.TryParse(value, out var expires))
            {
                throw new ArgumentException(
                    $"Unable to parse '{value}' as a date-time value"
                );
            }

            cookie.Expires = expires;
            cookie.Expired = expires <= DateTime.Now;
        }

        private static string Name(this Cookie cookie)
        {
            return cookie?.Name ?? "unknown cookie";
        }
    }
}