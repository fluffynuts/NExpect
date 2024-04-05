using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using Imported.PeanutButter.Utils;

namespace NExpect
{
    /// <summary>
    /// Provides matchers for HttpResponseMessages
    /// </summary>
    public static class HttpResponseMessageMatchers
    {
        /// <summary>
        /// Tests if an HttpResponseMessage contains the Set-Cookie
        /// header that would set the cookie with the provided name
        /// </summary>
        /// <param name="have"></param>
        /// <param name="name"></param>
        /// <returns>Continuation to further test the cookie, if found</returns>
        public static IMore<Cookie> Cookie(
            this IHave<HttpResponseMessage> have,
            string name
        )
        {
            return have.Cookie(name, NULL_STRING);
        }

        /// <summary>
        /// Tests if an HttpResponseMessage contains the Set-Cookie
        /// header that would set the cookie with the provided name
        /// </summary>
        /// <param name="have"></param>
        /// <param name="name"></param>
        /// <param name="customMessage"></param>
        /// <returns>Continuation to further test the cookie, if found</returns>
        public static IMore<Cookie> Cookie(
            this IHave<HttpResponseMessage> have,
            string name,
            string customMessage
        )
        {
            return have.Cookie(name, () => customMessage);
        }

        /// <summary>
        /// Tests if an HttpResponseMessage contains the Set-Cookie
        /// header that would set the cookie with the provided name
        /// </summary>
        /// <param name="have"></param>
        /// <param name="name"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns>Continuation to further test the cookie, if found</returns>
        public static IMore<Cookie> Cookie(
            this IHave<HttpResponseMessage> have,
            string name,
            Func<string> customMessageGenerator
        )
        {
            Cookie resolvedCookie = null;
            have.AddMatcher(
                actual =>
                {
                    var cookies = actual.Headers.Where(
                            h => h.Key == "Set-Cookie"
                        ).Select(h => h.Value.Select(ParseCookieHeader))
                        .SelectMany(o => o)
                        .ToArray();
                    resolvedCookie = cookies.FirstOrDefault(c => c.Name.Equals(name));
                    var passed = resolvedCookie != null;
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => $"Expected {passed.AsNot()}to find set-cookie for '{name}'",
                            customMessageGenerator
                        )
                    );
                }
            );
            return new Next<Cookie>(
                () => resolvedCookie,
                have as IExpectationContext
            );
        }

        /// <summary>
        /// Tests that a cookie has the expected value
        /// </summary>
        /// <param name="with"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IMore<Cookie> Value(
            this IWith<Cookie> with,
            string value
        )
        {
            return with.Value(value, NULL_STRING);
        }

        /// <summary>
        /// Tests that a cookie has the expected value
        /// </summary>
        /// <param name="with"></param>
        /// <param name="value"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<Cookie> Value(
            this IWith<Cookie> with,
            string value,
            string customMessage
        )
        {
            return with.Value(value, () => customMessage);
        }

        /// <summary>
        /// Tests that a cookie has the expected value
        /// </summary>
        /// <param name="with"></param>
        /// <param name="value"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<Cookie> Value(
            this IWith<Cookie> with,
            string value,
            Func<string> customMessageGenerator
        )
        {
            return with.AddMatcher(
                actual =>
                {
                    var passed = actual is not null && actual.Value.Equals(value);
                    return new MatcherResult(
                        passed,
                        () => actual is null
                            ? $"Expected {passed.AsNot()}to find cookie with value '{value}'"
                            : $"Expected {passed.AsNot()}to find value '{value}' for cookie '{actual.Name}' (found value: '{actual.Value}')",
                        customMessageGenerator
                    );
                }
            );
        }

        /// <summary>
        /// Tests if the cookie has the Secure flag
        /// </summary>
        /// <param name="more"></param>
        /// <returns></returns>
        public static IMore<Cookie> Secure(
            this ICanAddMatcher<Cookie> more
        )
        {
            return more.Secure(NULL_STRING);
        }

        /// <summary>
        /// Tests if the cookie has the Secure flag
        /// </summary>
        /// <param name="more"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<Cookie> Secure(
            this ICanAddMatcher<Cookie> more,
            string customMessage
        )
        {
            return more.Secure(() => customMessage);
        }

        /// <summary>
        /// Tests if the cookie has the Secure flag
        /// </summary>
        /// <param name="more"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<Cookie> Secure(
            this ICanAddMatcher<Cookie> more,
            Func<string> customMessageGenerator
        )
        {
            return more.AddMatcher(
                actual =>
                {
                    var passed = actual?.Secure ?? false;
                    return new MatcherResult(
                        passed,
                        () => $"Expected {actual.Name()} {passed.AsNot()}to be secure",
                        customMessageGenerator
                    );
                }
            );
        }

        /// <summary>
        /// Tests if the cookie has the HttpOnly flag
        /// </summary>
        /// <param name="more"></param>
        /// <returns></returns>
        public static IMore<Cookie> HttpOnly(
            this ICanAddMatcher<Cookie> more
        )
        {
            return more.HttpOnly(NULL_STRING);
        }

        /// <summary>
        /// Tests if the cookie has the HttpOnly flag
        /// </summary>
        /// <param name="more"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<Cookie> HttpOnly(
            this ICanAddMatcher<Cookie> more,
            string customMessage
        )
        {
            return more.HttpOnly(() => customMessage);
        }

        /// <summary>
        /// Tests if the cookie has the HttpOnly flag
        /// </summary>
        /// <param name="more"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<Cookie> HttpOnly(
            this ICanAddMatcher<Cookie> more,
            Func<string> customMessageGenerator
        )
        {
            return more.AddMatcher(
                actual =>
                {
                    var passed = actual?.HttpOnly ?? false;
                    return new MatcherResult(
                        passed,
                        () => $"Expected {actual.Name()} {passed.AsNot()}to be http-only",
                        customMessageGenerator
                    );
                }
            );
        }

        /// <summary>
        /// Tests if the cookie has the Domain flag
        /// </summary>
        /// <param name="more"></param>
        /// <param name="expectedDomain"></param>
        /// <returns></returns>
        public static IMore<Cookie> Domain(
            this ICanAddMatcher<Cookie> more,
            string expectedDomain
        )
        {
            return more.Domain(expectedDomain, NULL_STRING);
        }

        /// <summary>
        /// Tests if the cookie has the Domain flag
        /// </summary>
        /// <param name="more"></param>
        /// <param name="expectedDomain"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<Cookie> Domain(
            this ICanAddMatcher<Cookie> more,
            string expectedDomain,
            string customMessage
        )
        {
            return more.Domain(expectedDomain, () => customMessage);
        }

        /// <summary>
        /// Tests if the cookie has the Domain flag
        /// </summary>
        /// <param name="more"></param>
        /// <param name="expectedDomain"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<Cookie> Domain(
            this ICanAddMatcher<Cookie> more,
            string expectedDomain,
            Func<string> customMessageGenerator
        )
        {
            return more.AddMatcher(
                actual =>
                {
                    if (actual is null)
                    {
                        return new EnforcedMatcherResult(
                            false,
                            FinalMessageFor(
                                () => $"Unable to verify domain on null cookie",
                                customMessageGenerator
                            )
                        );
                    }

                    var passed = actual.Domain == expectedDomain;
                    return new MatcherResult(
                        passed,
                        () => $"Expected {actual.Name()} {passed.AsNot()}to be for domain '{expectedDomain}' (received: '{actual.Domain}')",
                        customMessageGenerator
                    );
                }
            );
        }

        /// <summary>
        /// Tests if the cookie has the Age flag
        /// </summary>
        /// <param name="more"></param>
        /// <param name="expectedAge"></param>
        /// <returns></returns>
        public static IMore<Cookie> Age(
            this IMax<Cookie> more,
            int expectedAge
        )
        {
            return more.Age(expectedAge, NULL_STRING);
        }

        /// <summary>
        /// Tests if the cookie has the Age flag
        /// </summary>
        /// <param name="more"></param>
        /// <param name="expectedAge"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<Cookie> Age(
            this IMax<Cookie> more,
            int expectedAge,
            string customMessage
        )
        {
            return more.Age(expectedAge, () => customMessage);
        }

        /// <summary>
        /// Tests if the cookie has the Age flag
        /// </summary>
        /// <param name="more"></param>
        /// <param name="expectedAge"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<Cookie> Age(
            this IMax<Cookie> more,
            int expectedAge,
            Func<string> customMessageGenerator
        )
        {
            return more.AddMatcher(
                actual =>
                {
                    var maxAgeSeconds = MaxAgeSecondsFor(actual);
                    var passed = maxAgeSeconds == expectedAge;

                    return new MatcherResult(
                        passed,
                        () =>
                            $"Expected {actual.Name()} {passed.AsNot()}to have Max-Age '{expectedAge}' (found {maxAgeSeconds})",
                        customMessageGenerator
                    );
                }
            );
        }

        private static int MaxAgeSecondsFor(Cookie cookie)
        {
            return (int)Math.Round((cookie.Expires - DateTime.Now).TotalSeconds);
        }

        private static Cookie ParseCookieHeader(
            string header
        )
        {
            var parts = header.Split(';');
            return parts.Aggregate(
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