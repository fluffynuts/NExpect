using System;
using System.Linq;
using System.Net;
using Imported.PeanutButter.TestUtils.AspNetCore;
using Imported.PeanutButter.Utils;
using Microsoft.AspNetCore.Http;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect;

/// <summary>
/// Provides some convenience matching for HttpRequest
/// objects, similar to that which is provided for
/// HttpResponse objects
/// </summary>
public static class HttpRequestMatchers
{
    /// <summary>
    /// Tests if an HttpResponse contains the Set-Cookie
    /// header that would set the cookie with the provided name
    /// </summary>
    /// <param name="have"></param>
    /// <param name="name"></param>
    /// <returns>Continuation to further test the cookie, if found</returns>
    public static IMore<Cookie> Cookie(
        this IHave<HttpRequest> have,
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
        this IHave<HttpRequest> have,
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
        this IHave<HttpRequest> have,
        string name,
        Func<string> customMessageGenerator
    )
    {
        Cookie resolvedCookie = null;
        have.AddMatcher(
            actual =>
            {
                if (!actual.Cookies.Keys.Contains(name))
                {
                    return new MatcherResult(
                        false,
                        MessageHelpers.FinalMessageFor(
                            () => $"Expected {false.AsNot()}to find cookie '{name}'",
                            customMessageGenerator
                        )
                    );
                }
                
                resolvedCookie = new Cookie(name, actual.Cookies[name]);
                return new MatcherResult(
                    true,
                    MessageHelpers.FinalMessageFor(
                        () => $"Expected {true.AsNot()}to find set-cookie header for '{name}'",
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
}
