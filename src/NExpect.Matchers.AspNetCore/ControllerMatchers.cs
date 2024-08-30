using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using Imported.PeanutButter.Utils;
using Microsoft.AspNetCore.Mvc;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using _HttpMethod_ = Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect;

/// <summary>
/// Adds matchers for AspNetCore Controllers and Controller Actions
/// </summary>
public static class ControllerMatchers
{
    /// <summary>
    /// Asserts that the controller has the expected base route
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Route(
        this IHave<MethodInfo> have,
        string expected
    )
    {
        return have.Route(expected, () => NULL_STRING);
    }

    /// <summary>
    /// Asserts that the controller has the expected base route
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Route(
        this IHave<MethodInfo> have,
        string expected,
        Func<string> customMessageGenerator
    )
    {
        return AddRouteMatcher(
            have,
            expected,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Quick-n-dirty assertion that a controller's method has the required route
    /// </summary>
    /// <param name="have"></param>
    /// <param name="member"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Route(
        this IHave<MethodInfo> have,
        string member,
        string expected
    )
    {
        return have.Route(
            member,
            () => expected
        );
    }

    /// <summary>
    /// Asserts that the controller has the expected base route
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Route(
        this IWith<MethodInfo> have,
        string expected
    )
    {
        return have.Route(expected, () => NULL_STRING);
    }

    /// <summary>
    /// Asserts that the controller has the expected base route
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Route(
        this IWith<MethodInfo> have,
        string expected,
        Func<string> customMessageGenerator
    )
    {
        return AddRouteMatcher(
            have,
            expected,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Quick-n-dirty assertion that a controller's method has the required route
    /// </summary>
    /// <param name="have"></param>
    /// <param name="member"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Route(
        this IWith<MethodInfo> have,
        string member,
        string expected
    )
    {
        return have.Route(
            member,
            () => expected
        );
    }

    /// <summary>
    /// Asserts that the controller has the expected base route
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Route(
        this IAnd<MethodInfo> have,
        string expected
    )
    {
        return have.Route(expected, () => NULL_STRING);
    }

    /// <summary>
    /// Asserts that the controller has the expected base route
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Route(
        this IAnd<MethodInfo> have,
        string expected,
        Func<string> customMessageGenerator
    )
    {
        return AddRouteMatcher(
            have,
            expected,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Quick-n-dirty assertion that a controller's method has the required route
    /// </summary>
    /// <param name="have"></param>
    /// <param name="member"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Route(
        this IAnd<MethodInfo> have,
        string member,
        string expected
    )
    {
        return have.Route(
            member,
            () => expected
        );
    }

    private static IMore<MethodInfo> AddRouteMatcher(
        ICanAddMatcher<MethodInfo> continuation,
        string expected,
        Func<string> customMessageGenerator
    )
    {
        return continuation.AddMatcher(
            actual =>
            {
                var attribs = actual.GetCustomAttributes(false).OfType<RouteAttribute>();
                var passed = attribs.Any(a => a.Template == expected);
                if (!passed)
                {
                    return new MatcherResult(
                        false,
                        FinalMessageFor(
                            () =>
                                $"Expected {actual.DeclaringType.PrettyName()}.{actual.Name} {passed.AsNot()}to have route '{expected}'",
                            customMessageGenerator
                        )
                    );
                }

                var parametersMatch = FindParametersInRoute.Matches(expected);
                var mismatched = new List<string>();
                var methodParameters = actual.GetParameters();
                foreach (Match m in parametersMatch)
                {
                    var parameter = m.Value.Trim(['{', '}']);
                    var matchedParameter = methodParameters.FirstOrDefault(
                        pi => pi.Name is not null && pi.Name.Equals(
                            parameter,
                            StringComparison.OrdinalIgnoreCase
                        )
                    );
                    if (matchedParameter is null)
                    {
                        mismatched.Add($"{parameter} (missing parameter)");
                    }
                    else
                    {
                        var attrib = matchedParameter.GetCustomAttributes<FromRouteAttribute>()
                            .FirstOrDefault();
                        if (attrib is null)
                        {
                            mismatched.Add($"{parameter} (should decorate with [FromQuery])");
                        }
                    }
                }

                passed = !mismatched.Any();
                var s = mismatched.Count == 1 ? "" : "s";

                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"""
                               Parameter{s} in route '{expected}' should have matching method parameter{s} on {actual.DeclaringType}.{actual.Name} decorated with [FromRoute]:
                               - {mismatched.JoinWith("\n- ")}
                               """,
                        customMessageGenerator
                    )
                );
            }
        );
    }

    private static readonly Regex FindParametersInRoute = new(
        "((?:{[^{}]+})+)",
        RegexOptions.Compiled
    );

    /// <summary>
    /// Verifies that the method is decorated
    /// with the relevant attribute for determining
    /// the http verb to use to access the endpoint
    /// </summary>
    /// <param name="more"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Supporting(
        this IMore<MethodInfo> more,
        HttpMethod expected
    )
    {
        return more.Supporting(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verifies that the method is decorated
    /// with the relevant attribute for determining
    /// the http verb to use to access the endpoint
    /// </summary>
    /// <param name="more"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Supporting(
        this IMore<MethodInfo> more,
        HttpMethod expected,
        string customMessage
    )
    {
        return more.Supporting(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that the method is decorated
    /// with the relevant attribute for determining
    /// the http verb to use to access the endpoint
    /// </summary>
    /// <param name="more"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Supporting(
        this IMore<MethodInfo> more,
        HttpMethod expected,
        Func<string> customMessageGenerator
    )
    {
        var continuation = more as ICanAddMatcher<MethodInfo>;
        if (!HttpMethodLookup.TryGetValue(expected, out var method))
        {
            throw new InvalidOperationException(
                $"Unable to translate '{expected}' to type Microsoft.AspNetCore.Server.Kestrel.Core.Internal.HttpMethod"
            );
        }

        return continuation.Supporting(
            method,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Verifies that the method is decorated
    /// with the relevant attribute for determining
    /// the http verb to use to access the endpoint
    /// </summary>
    /// <param name="more"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Supporting(
        this IAnd<MethodInfo> more,
        HttpMethod expected
    )
    {
        return more.Supporting(expected, NULL_STRING);
    }

    /// <summary>
    /// Verifies that the method is decorated
    /// with the relevant attribute for determining
    /// the http verb to use to access the endpoint
    /// </summary>
    /// <param name="more"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Supporting(
        this IAnd<MethodInfo> more,
        HttpMethod expected,
        string customMessage
    )
    {
        return more.Supporting(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that the method is decorated
    /// with the relevant attribute for determining
    /// the http verb to use to access the endpoint
    /// </summary>
    /// <param name="more"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Supporting(
        this IAnd<MethodInfo> more,
        HttpMethod expected,
        Func<string> customMessageGenerator
    )
    {
        var continuation = more as ICanAddMatcher<MethodInfo>;
        if (!HttpMethodLookup.TryGetValue(expected, out var method))
        {
            throw new InvalidOperationException(
                $"Unable to translate '{expected}' to type Microsoft.AspNetCore.Server.Kestrel.Core.Internal.HttpMethod"
            );
        }

        return continuation.Supporting(
            method,
            customMessageGenerator
        );
    }

    private static IDictionary<HttpMethod, _HttpMethod_> HttpMethodLookup =
        new Dictionary<HttpMethod, _HttpMethod_>()
        {
            [HttpMethod.Delete] = _HttpMethod_.Delete,
            [HttpMethod.Get] = _HttpMethod_.Get,
            [HttpMethod.Head] = _HttpMethod_.Head,
            [HttpMethod.Options] = _HttpMethod_.Options,
            [HttpMethod.Patch] = _HttpMethod_.Patch,
            [HttpMethod.Post] = _HttpMethod_.Post,
            [HttpMethod.Put] = _HttpMethod_.Put,
            [HttpMethod.Trace] = _HttpMethod_.Trace
        };

    private static IMore<MethodInfo> Supporting(
        this ICanAddMatcher<MethodInfo> more,
        _HttpMethod_ expected,
        Func<string> customMessageGenerator
    )
    {
        return more.AddMatcher(
            actual =>
            {
                var attribs = actual.GetCustomAttributes(inherit: false);
                var specified = new HashSet<_HttpMethod_>();
                foreach (var attrib in attribs)
                {
                    var match = attrib switch
                    {
                        HttpGetAttribute => _HttpMethod_.Get,
                        HttpPutAttribute => _HttpMethod_.Put,
                        HttpPostAttribute => _HttpMethod_.Post,
                        HttpPatchAttribute => _HttpMethod_.Patch,
                        HttpOptionsAttribute => _HttpMethod_.Options,
                        HttpDeleteAttribute => _HttpMethod_.Delete,
                        _ => null as _HttpMethod_?
                    };
                    if (match is null)
                    {
                        continue;
                    }

                    specified.Add(match.Value);
                }

                var configuredOnAction = specified.IsEmpty()
                    ?
                    [
                        _HttpMethod_.Get
                    ] // implied when nothing is configured
                    : specified.ToArray();
                var passed = configuredOnAction.Contains(expected);
                return new MatcherResult(
                    passed,
                    () =>
                        $"Expected '{
                            actual.Name
                        }' on '{
                            actual.DeclaringType.PrettyName()
                        }' {passed.AsNot()}to support method '{expected}', which supports: ({string.Join(",", configuredOnAction)})",
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Verifies that the method is decorated
    /// with the relevant attribute for determining
    /// the http verb to use to access the endpoint
    /// </summary>
    /// <param name="more"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Supporting(
        this IMore<MethodInfo> more,
        _HttpMethod_ expected
    )
    {
        return more.Supporting(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verifies that the method is decorated
    /// with the relevant attribute for determining
    /// the http verb to use to access the endpoint
    /// </summary>
    /// <param name="more"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Supporting(
        this IMore<MethodInfo> more,
        _HttpMethod_ expected,
        string customMessage
    )
    {
        return more.Supporting(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that the method is decorated
    /// with the relevant attribute for determining
    /// the http verb to use to access the endpoint
    /// </summary>
    /// <param name="more"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Supporting(
        this IMore<MethodInfo> more,
        _HttpMethod_ expected,
        Func<string> customMessageGenerator
    )
    {
        var continuation = more as ICanAddMatcher<MethodInfo>;
        return continuation.Supporting(
            expected,
            customMessageGenerator
        );
    }


    /// <summary>
    /// Verifies that the method is decorated
    /// with the relevant attribute for determining
    /// the http verb to use to access the endpoint
    /// </summary>
    /// <param name="more"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Supporting(
        this IAnd<MethodInfo> more,
        _HttpMethod_ expected
    )
    {
        return more.Supporting(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verifies that the method is decorated
    /// with the relevant attribute for determining
    /// the http verb to use to access the endpoint
    /// </summary>
    /// <param name="more"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Supporting(
        this IAnd<MethodInfo> more,
        _HttpMethod_ expected,
        string customMessage
    )
    {
        return more.Supporting(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that the method is decorated
    /// with the relevant attribute for determining
    /// the http verb to use to access the endpoint
    /// </summary>
    /// <param name="more"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<MethodInfo> Supporting(
        this IAnd<MethodInfo> more,
        _HttpMethod_ expected,
        Func<string> customMessageGenerator
    )
    {
        var continuation = more as ICanAddMatcher<MethodInfo>;
        return continuation.Supporting(
            expected,
            customMessageGenerator
        );
    }


    /// <summary>
    /// Assert that a controller type has been decorated with the applicable [Area] attribute
    /// </summary>
    /// <param name="have"></param>
    /// <param name="areaName"></param>
    /// <returns></returns>
    public static IMore<Type> Area(
        this IHave<Type> have,
        string areaName
    )
    {
        return have.Area(
            areaName,
            NULL_STRING
        );
    }

    /// <summary>
    /// Assert that a controller type has been decorated with the applicable [Area] attribute
    /// </summary>
    /// <param name="have"></param>
    /// <param name="areaName"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<Type> Area(
        this IHave<Type> have,
        string areaName,
        string customMessage
    )
    {
        return have.Area(
            areaName,
            () => customMessage
        );
    }

    /// <summary>
    /// Assert that a controller type has been decorated with the applicable [Area] attribute
    /// </summary>
    /// <param name="have"></param>
    /// <param name="areaName"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<Type> Area(
        this IHave<Type> have,
        string areaName,
        Func<string> customMessageGenerator
    )
    {
        return have.AddMatcher(
            actual =>
            {
                if (actual is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        "Provided controller type is null"
                    );
                }

                if (areaName is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        "Provided area name is null"
                    );
                }

                var attrib = actual.GetCustomAttributes(inherit: false)
                    .OfType<AreaAttribute>()
                    .FirstOrDefault();

                if (attrib is null)
                {
                    return new MatcherResult(
                        false,
                        FinalMessageFor(
                            () =>
                                $"Expected type {actual} {false.AsNot()} to be decorated with [Area(\"{areaName}\")]",
                            customMessageGenerator
                        )
                    );
                }

                var passed = areaName.Equals(attrib.RouteValue, StringComparison.OrdinalIgnoreCase);
                var more = passed
                    ? $" but found [Area(\"{attrib.RouteValue}\")]"
                    : " but found exactly that";
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () =>
                            $"Expected type {actual} {passed.AsNot()} to be decorated with [Area(\"{areaName}\")]{more}",
                        customMessageGenerator
                    )
                );
            }
        );
    }

    /// <summary>
    /// Verifies that the controller is decorated
    /// with a [Route(...)] attribute with the required
    /// route
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<Type> Route(
        this IHave<Type> have,
        string expected
    )
    {
        return have.Route(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verifies that the controller is decorated
    /// with a [Route(...)] attribute with the required
    /// route
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<Type> Route(
        this IHave<Type> have,
        string expected,
        string customMessage
    )
    {
        return have.Route(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that the controller is decorated
    /// with a [Route(...)] attribute with the required
    /// route
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<Type> Route(
        this IHave<Type> have,
        string expected,
        Func<string> customMessageGenerator
    )
    {
        return have.AddMatcher(
            actual =>
            {
                var attribs = actual.GetCustomAttributes(false).OfType<RouteAttribute>();
                var passed = attribs.Any(a => a.Template == expected);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected {actual.Name} {passed.AsNot()}to have route '{expected}'",
                        customMessageGenerator
                    )
                );
            }
        );
    }
}