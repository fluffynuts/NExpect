using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Imported.PeanutButter.TestUtils.AspNetCore;
using Imported.PeanutButter.Utils;
using Imported.PeanutButter.Utils.Dictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ConstantConditionalAccessQualifier

namespace NExpect;

/// <summary>
/// Provides matchers for action results, e.g.
/// redirections
/// </summary>
public static class ActionResultMatchers
{
    // interesting notes about RouteValueDictionary instances:
    // 1. indexing in with a missing key provides null (not an exception)
    // 2. keys are case-insensitive
    // 3. values are of type object (so it's a good idea to cast before comparing,
    //      but this is also why values can be numbers or booleans)

    /// <summary>
    /// Verify that an action result is a redirection
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static IMore<T> Redirect<T>(
        this ITo<T> to
    ) where T : IActionResult
    {
        return to.Redirect(NULL_STRING);
    }

    /// <summary>
    /// Verify that an action result is a redirection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<T> Redirect<T>(
        this ITo<T> to,
        string customMessage
    ) where T : IActionResult
    {
        return to.Redirect(() => customMessage);
    }

    /// <summary>
    /// Verify that an action result is a redirection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<T> Redirect<T>(
        this ITo<T> to,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return VerifyRedirection(
            to,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Verify that an action result is not a redirection
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static IMore<T> Redirect<T>(
        this IToAfterNot<T> to
    ) where T : IActionResult
    {
        return to.Redirect(NULL_STRING);
    }

    /// <summary>
    /// Verify that an action result is not a redirection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<T> Redirect<T>(
        this IToAfterNot<T> to,
        string customMessage
    ) where T : IActionResult
    {
        return to.Redirect(() => customMessage);
    }

    /// <summary>
    /// Verify that an action result is not a redirection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<T> Redirect<T>(
        this IToAfterNot<T> to,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return VerifyRedirection(
            to,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Verify that an action result is not a redirection
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static IMore<T> Redirect<T>(
        this INotAfterTo<T> to
    ) where T : IActionResult
    {
        return to.Redirect(NULL_STRING);
    }

    /// <summary>
    /// Verify that an action result is not a redirection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<T> Redirect<T>(
        this INotAfterTo<T> to,
        string customMessage
    ) where T : IActionResult
    {
        return to.Redirect(() => customMessage);
    }

    /// <summary>
    /// Verify that an action result is not a redirection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<T> Redirect<T>(
        this INotAfterTo<T> to,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return VerifyRedirection(
            to,
            customMessageGenerator
        );
    }

    private static IMore<T> VerifyRedirection<T>(
        ICanAddMatcher<T> continuation,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return continuation.AddMatcher(
            actual =>
            {
                var passed =
                    actual is RedirectResult
                        or RedirectToRouteResult
                        or RedirectToActionResult;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => actual is null
                            ? "Expected a redirect result, but received NULL"
                            : $"Expected a redirect result, but got result of type {actual.GetType()}",
                        customMessageGenerator
                    )
                );
            }
        );
    }


    /// <summary>
    /// Verify that an action result is a redirection to a specific url
    /// </summary>
    /// <param name="to"></param>
    /// <param name="url"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Url<T>(
        this ITo<T> to,
        string url
    ) where T : IActionResult
    {
        return to.Url(url, NULL_STRING);
    }

    /// <summary>
    /// Verify that an action result is a redirection to a specific url
    /// </summary>
    /// <param name="to"></param>
    /// <param name="url"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Url<T>(
        this ITo<T> to,
        string url,
        string customMessage
    ) where T : IActionResult
    {
        return to.Url(url, () => customMessage);
    }

    /// <summary>
    /// Verify that an action result is a redirection to a specific url
    /// </summary>
    /// <param name="to"></param>
    /// <param name="url"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Url<T>(
        this ITo<T> to,
        string url,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return to.AddMatcher(
            actual =>
            {
                var redirect = actual as RedirectResult;
                if (redirect is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        () => $"{actual?.GetType()} is not a redirect result"
                    );
                }

                var passed = redirect.Url == url;
                var more = passed
                    ? " (but received exactly that)"
                    : $" (but received '{redirect.Url}'";
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected redirect result {passed.AsNot()}to be '{url}'{more}",
                        customMessageGenerator
                    )
                );
            }
        );
    }

    /// <summary>
    /// Verifies that the action result redirects to another action
    /// </summary>
    /// <param name="to"></param>
    /// <param name="actionName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Action<T>(
        this ITo<T> to,
        string actionName
    ) where T : IActionResult
    {
        return to.Action(
            actionName,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verifies that the action result redirects to another action
    /// </summary>
    /// <param name="to"></param>
    /// <param name="actionName"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Action<T>(
        this ITo<T> to,
        string actionName,
        string customMessage
    ) where T : IActionResult
    {
        return to.Action(
            actionName,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that the action result redirects to another action
    /// </summary>
    /// <param name="to"></param>
    /// <param name="actionName"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Action<T>(
        this ITo<T> to,
        string actionName,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return to.AddMatcher(
            actual => CreateActionMatcherFor(
                actual,
                actionName,
                customMessageGenerator
            )
        );
    }

    private static IMatcherResult CreateActionMatcherFor<T>(
        T actual,
        string actionName,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        var routeValues =
            actual.GetOrDefault<RouteValueDictionary>(
                "RouteValues",
                null // RedirectToRouteResult
            );

        if (actual is RedirectToActionResult redirectToActionResult)
        {
            // RedirectToActionResult does not store controller and action
            // in RouteValues, but may have RouteValues for parameters because
            // obviously whoever wrote the new one was high.
            routeValues ??= new RouteValueDictionary();
            routeValues["action"] = redirectToActionResult.ActionName;
            routeValues["controller"] = redirectToActionResult.ControllerName;
        }

        if (routeValues is null)
        {
            return new EnforcedMatcherResult(
                false,
                () =>
                    $"Provided action result exposes no known RouteValueDictionary-typed property and is not a {nameof(RedirectToActionResult)}"
            );
        }

        actual.SetMetadata(ROUTE_VALUES_KEY, routeValues);
        return GenerateRouteValueMatcherResultFor(
            routeValues,
            "action",
            actionName,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Verifies that the redirection is to an action on the current controller
    /// </summary>
    /// <param name="on"></param>
    /// <returns></returns>
    public static IMore<T> CurrentController<T>(
        this IOn<T> on
    ) where T : IActionResult
    {
        return on.CurrentController(NULL_STRING);
    }

    /// <summary>
    /// Verifies that the redirection is to an action on the current controller
    /// </summary>
    /// <param name="on"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<T> CurrentController<T>(
        this IOn<T> on,
        string customMessage
    ) where T : IActionResult
    {
        return on.CurrentController(() => customMessage);
    }

    /// <summary>
    /// Verifies that the redirection is to an action on the current controller
    /// </summary>
    /// <param name="on"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<T> CurrentController<T>(
        this IOn<T> on,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return on.AddMatcher(
            actual =>
            {
                if (CantReadRouteValuesOn(actual, out var routeValues) is { } fail)
                {
                    return fail;
                }

                var controller = routeValues.TryGetValue("controller", out var value)
                    ? value
                    : null;
                var passed = controller is null;
                return new MatcherResult(
                    passed,
                    () =>
                        $"Expected {passed.AsNot()} to redirect to the same controller, but found controller '{controller}'",
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Verifies that the redirect is to the named controller
    /// </summary>
    /// <param name="on"></param>
    /// <param name="controller"></param>
    /// <returns></returns>
    public static IMore<T> Controller<T>(
        this IOn<T> on,
        string controller
    ) where T : IActionResult
    {
        return on.Controller(
            controller,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verifies that the redirect is to the named controller
    /// </summary>
    /// <param name="on"></param>
    /// <param name="controller"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<T> Controller<T>(
        this IOn<T> on,
        string controller,
        string customMessage
    ) where T : IActionResult
    {
        return on.Controller(
            controller,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that the redirect is to the named controller
    /// </summary>
    /// <param name="on"></param>
    /// <param name="controller"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<T> Controller<T>(
        this IOn<T> on,
        string controller,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return on.AddMatcher(
            actual =>
            {
                if (CantReadRouteValuesOn(actual, out var routeValues) is { } fail)
                {
                    return fail;
                }

                return GenerateRouteValueMatcherResultFor(
                    routeValues,
                    "controller",
                    controller,
                    customMessageGenerator
                );
            }
        );
    }

    private static MatcherResult CantReadRouteValuesOn<T>(
        T actionResult,
        out RouteValueDictionary routeValues
    ) where T : IActionResult
    {
        if (!actionResult.TryGetMetadata(ROUTE_VALUES_KEY, out routeValues))
        {
            return new EnforcedMatcherResult(
                false,
                () => "Please start this expect chain with .To.Redirect().To.Action"
            );
        }

        return null;
    }

    /// <summary>
    /// Verifies that the action result redirects to the
    /// named area
    /// </summary>
    /// <param name="in"></param>
    /// <param name="area"></param>
    /// <returns></returns>
    public static IMore<T> Area<T>(
        this IIn<T> @in,
        string area
    ) where T : IActionResult
    {
        return @in.Area(area, NULL_STRING);
    }

    /// <summary>
    /// Verifies that the action result redirects to the
    /// named area
    /// </summary>
    /// <param name="in"></param>
    /// <param name="area"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<T> Area<T>(
        this IIn<T> @in,
        string area,
        string customMessage
    ) where T : IActionResult
    {
        return @in.Area(area, () => customMessage);
    }

    /// <summary>
    /// Verifies that the action result redirects to the
    /// named area
    /// </summary>
    /// <param name="in"></param>
    /// <param name="area"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<T> Area<T>(
        this IIn<T> @in,
        string area,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return @in.AddMatcher(
            actual =>
            {
                if (CantReadRouteValuesOn(actual, out var routeValues) is { } fail)
                {
                    return fail;
                }

                return GenerateRouteValueMatcherResultFor(
                    routeValues,
                    "area",
                    area,
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Verifies that the redirect is to the root of the application (/)
    /// i.e. not in an area
    /// </summary>
    /// <param name="in"></param>
    /// <returns></returns>
    public static IMore<T> Root<T>(
        this IIn<T> @in
    ) where T : IActionResult
    {
        return @in.Root(NULL_STRING);
    }

    /// <summary>
    /// Verifies that the redirect is to the root of the application (/)
    /// </summary>
    /// <param name="in"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<T> Root<T>(
        this IIn<T> @in,
        string customMessage
    ) where T : IActionResult
    {
        return @in.Root(() => customMessage);
    }

    /// <summary>
    /// Verifies that the redirect is to the root of the application (/)
    /// </summary>
    /// <param name="in"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<T> Root<T>(
        this IIn<T> @in,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return @in.AddMatcher(
            actual =>
            {
                if (CantReadRouteValuesOn(actual, out var routeValues) is { } fail)
                {
                    return fail;
                }

                return GenerateRouteValueMatcherResultFor(
                    routeValues,
                    "area",
                    o => o is null || string.IsNullOrEmpty(o as string),
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Verifies the parameters on the redirecting action result
    /// </summary>
    /// <param name="with"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static IMore<T> Parameters<T>(
        this IWith<T> with,
        object parameters
    ) where T : IActionResult
    {
        return with.Parameters(
            parameters,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verifies the parameters on the redirecting action result
    /// </summary>
    /// <param name="with"></param>
    /// <param name="parameters"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<T> Parameters<T>(
        this IWith<T> with,
        object parameters,
        string customMessage
    ) where T : IActionResult
    {
        return with.Parameters(
            parameters,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies the parameters on the redirecting action result
    /// </summary>
    /// <param name="with"></param>
    /// <param name="parameters"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<T> Parameters<T>(
        this IWith<T> with,
        object parameters,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return with.AddMatcher(
            actual =>
            {
                if (parameters is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        "null parameters provided"
                    );
                }

                if (CantReadRouteValuesOn(actual, out var routeValues) is { } fail)
                {
                    return fail;
                }

                // ReSharper disable once CollectionNeverUpdated.Local
                var expected = new DictionaryWrappingObject(parameters);

                var mismatched = CompareDictionaries(routeValues, expected);
                var passed = mismatched.Count == 0;

                return new MatcherResult(
                    passed,
                    () =>
                    {
                        var errors = mismatched.Keys.Aggregate(
                            new List<string>(),
                            (acc, cur) =>
                            {
                                acc.Add($"{cur}: {mismatched[cur]}");
                                return acc;
                            }
                        );
                        return string.Join("\n", errors);
                    },
                    customMessageGenerator
                );
            }
        );
    }

    private static readonly HashSet<string> ReservedRouteKeys = ["action", "controller", "area"];

    private static Dictionary<string, string> CompareDictionaries(
        IDictionary<string, object> actual,
        IDictionary<string, object> expected
    )
    {
        var result = actual.Keys.Aggregate(
            new Dictionary<string, string>(),
            (acc, cur) =>
            {
                if (ReservedRouteKeys.Contains(cur))
                {
                    return acc;
                }

                if (!expected.TryGetValue(cur, out var found))
                {
                    acc[cur] = "unexpected parameter";
                    return acc;
                }

                var areEqual = found?.Equals(expected[cur]) ?? false;
                if (!areEqual)
                {
                    acc[cur] =
                        $"expected {actual[cur].Stringify()} but received {expected[cur].Stringify()}";
                }

                return acc;
            }
        );
        var missingExpectedParameters = expected.Keys.Where(k => !actual.ContainsKey(k)).ToArray();
        foreach (var item in missingExpectedParameters)
        {
            result[item] = "missing required parameter";
        }

        return result;
    }

    private static MatcherResult GenerateRouteValueMatcherResultFor(
        RouteValueDictionary routeValues,
        string routeValueKey,
        string expected,
        Func<string> customMessageGenerator
    )
    {
        return GenerateRouteValueMatcherResultFor(
            routeValues,
            routeValueKey,
            o => string.Equals(
                o as string,
                expected,
                StringComparison.InvariantCultureIgnoreCase
            ),
            customMessageGenerator
        );
    }

    private static MatcherResult GenerateRouteValueMatcherResultFor(
        RouteValueDictionary routeValues,
        string routeValueKey,
        Func<object, bool> matcher,
        Func<string> customMessageGenerator
    )
    {
        var stored = routeValues[routeValueKey];
        var isMissing = stored is null;
        var passed = matcher(stored);
        return new MatcherResult(
            passed,
            () => $@"Expected {routeValueKey} to be matched but {
                (isMissing ? $"no {routeValueKey} specified" : $"found '{stored}'")
            }",
            customMessageGenerator
        );
    }

    private const string ROUTE_VALUES_KEY = "__route_values__";

    /// <summary>
    /// Verifies that the action result has no body
    /// </summary>
    /// <param name="be"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Empty<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.AddMatcher(
            actionResult =>
            {
                if (actionResult is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        () => "Can't test null action result"
                    );
                }

                var response = actionResult.ResolveResponse();
                if (response is null)
                {
                    return new MatcherResult(
                        false,
                        () => $"Expected response {false.AsNot()}to have empty content, but it has null content"
                    );
                }

                var content = response.Body.ReadAllBytes();
                var passed = content.Length == 0;
                return new MatcherResult(
                    passed,
                    () => $"Expected response {passed.AsNot()}to have no content"
                );
            }
        );
    }

    /// <summary>
    /// Verify the content on the action result
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Content<T>(
        this IHave<T> have,
        string expected
    ) where T : IActionResult
    {
        return have.Content(expected, NULL_STRING);
    }

    /// <summary>
    /// Verify the content on the action result
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Content<T>(
        this IHave<T> have,
        string expected,
        string customMessage
    ) where T : IActionResult
    {
        return have.Content(expected, () => customMessage);
    }

    /// <summary>
    /// Verify the content on the action result
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Content<T>(
        this IHave<T> have,
        string expected,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return have.Content(
            s => s == expected,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Verify the content on the action result
    /// </summary>
    /// <param name="have"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Content<T>(
        this IHave<T> have,
        Func<string, bool> matcher,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return have.AddMatcher(
            actionResult =>
            {
                if (actionResult is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        () => "Can't test null action result"
                    );
                }

                var response = actionResult.ResolveResponse();
                if (response is null)
                {
                    return new MatcherResult(
                        false,
                        () => $"Expected response {false.AsNot()}to have content, but it has null content"
                    );
                }

                var content = response.Body.ReadAllBytes();
                var passed = matcher(
                    Encoding.UTF8.GetString(
                        content
                    )
                );
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Unexpected content on action result:\n\n{content}",
                        customMessageGenerator
                    )
                );
            }
        );
    }

    /// <summary>
    /// Verify the status code is 403 on the action result
    /// </summary>
    /// <param name="be"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Forbidden<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Forbidden(NULL_STRING);
    }

    /// <summary>
    /// Verify the status code is 403 on the action result
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Forbidden<T>(
        this IBe<T> be,
        string customMessage
    ) where T : IActionResult
    {
        return be.Forbidden(() => customMessage);
    }

    /// <summary>
    /// Verify the status code is 403 on the action result
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Forbidden<T>(
        this IBe<T> be,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return ValidateStatusCode(
            be,
            HttpStatusCode.Forbidden,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Verify the status code is 403 on the action result
    /// </summary>
    /// <param name="be"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Ok<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Ok(NULL_STRING);
    }

    /// <summary>
    /// Verify the status code is 403 on the action result
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Ok<T>(
        this IBe<T> be,
        string customMessage
    ) where T : IActionResult
    {
        return be.Ok(() => customMessage);
    }

    /// <summary>
    /// Verify the status code is 403 on the action result
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Ok<T>(
        this IBe<T> be,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return ValidateStatusCode(
            be,
            HttpStatusCode.OK,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Verify the status code of an action result
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> StatusCode<T>(
        this IHave<T> have,
        HttpStatusCode expected
    ) where T : IActionResult
    {
        return have.StatusCode(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verify the status code of an action result
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> StatusCode<T>(
        this IHave<T> have,
        HttpStatusCode expected,
        string customMessage
    ) where T : IActionResult
    {
        return have.StatusCode(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Verify the status code of an action result
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> StatusCode<T>(
        this IHave<T> have,
        HttpStatusCode expected,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return ValidateStatusCode(have, expected, customMessageGenerator);
    }

    private static IMore<T> ValidateStatusCode<T>(
        this ICanAddMatcher<T> canAddMatcher,
        HttpStatusCode expected,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return canAddMatcher.AddMatcher(
            actual =>
            {
                var statusCode = actual.StatusCode();
                var passed = statusCode == expected;
                var more = passed
                    ? "but found exactly that"
                    : $"but found {statusCode}";
                return new MatcherResult(
                    passed,
                    () => $"Expected {passed.AsNot()}to find status code {expected}, {more}",
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Verify that the action result is for a rejected
    /// request with the expected statusCode
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expectedCode"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected<T>(
        this IBe<T> be,
        HttpStatusCode expectedCode
    ) where T : IActionResult
    {
        return be.Rejected(expectedCode, () => null);
    }

    /// <summary>
    /// Verify that the action result is for a rejected
    /// request with the expected statusCode
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expectedCode"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected<T>(
        this IBe<T> be,
        HttpStatusCode expectedCode,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.AddMatcher(
            actionResult =>
            {
                if (actionResult is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        () => "Cannot enforce returned status code on null action result"
                    );
                }

                var response = actionResult?.ResolveResponse();
                if (response is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        () => @"Response has no content.
If your assertion is that it should not be rejected,
rather specify .To.Not.Be.Rejected() instead of specifying
an http status code to not expect, otherwise one couldn't
specify that a result _should_ be rejected, just _not_ with
the specified code. This is in the interest of making tests
as precise as possible."
                    );
                }

                var code = (int)expectedCode;
                var passed = response?.StatusCode == code;
                var verb = code is >= 200 and < 300
                    ? "accepted"
                    : "rejected";

                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $@"Expected request {
                            passed.AsNot()
                        }to have been {verb} with a {
                            (int)expectedCode
                        } {expectedCode}, but found {response.StatusCode}",
                        customMessageGenerator
                    )
                );
            }
        );
    }

    /// <summary>
    /// Verify that the action result is for a rejected
    /// request with the statusCode 201
    /// </summary>
    /// <param name="be"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Created201<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Created201(NULL_STRING);
    }

    /// <summary>
    /// Verify that the action result is for a rejected
    /// request with the statusCode 201
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Created201<T>(
        this IBe<T> be,
        string customMessage
    ) where T : IActionResult
    {
        return be.Created201(() => customMessage);
    }

    /// <summary>
    /// Verify that the action result is for a rejected
    /// request with the statusCode 201
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Created201<T>(
        this IBe<T> be,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.Resolved(HttpStatusCode.Created, () => null);
    }

    /// <summary>
    /// Verify that the action result is resolved with
    /// the expected status code
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expectedCode"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Resolved<T>(
        this IBe<T> be,
        HttpStatusCode expectedCode
    ) where T : IActionResult
    {
        return be.Resolved(
            expectedCode,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verify that the action result is resolved with
    /// the expected status code
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expectedCode"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Resolved<T>(
        this IBe<T> be,
        HttpStatusCode expectedCode,
        string customMessage
    ) where T : IActionResult
    {
        return be.Resolved(
            expectedCode,
            () => customMessage
        );
    }

    /// <summary>
    /// Verify that the action result is resolved with
    /// the expected status code
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expectedCode"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Resolved<T>(
        this IBe<T> be,
        HttpStatusCode expectedCode,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.AddMatcher(
            actionResult =>
            {
                var response = actionResult?.ResolveResponse();
                var passed = response?.StatusCode == (int)expectedCode;

                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () =>
                            $@"Expected request {
                                passed.AsNot()
                            }to have been created with a {
                                (int)expectedCode
                            } {expectedCode}",
                        customMessageGenerator
                    )
                );
            }
        );
    }

    /// <summary>
    /// Verifies that the request has
    /// a status code of 200 (OK)
    /// </summary>
    /// <param name="be"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Accepted<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Accepted(NULL_STRING);
    }

    /// <summary>
    /// Verifies that the request has
    /// a status code of 200 (OK)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Accepted<T>(
        this IBe<T> be,
        string customMessage
    ) where T : IActionResult
    {
        return be.Accepted(
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that the request has
    /// a status code of 200 (OK)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Accepted<T>(
        this IBe<T> be,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.Accepted(
            HttpStatusCode.OK,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Verifies that the request has
    /// the required status code
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expectedCode"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Accepted<T>(
        this IBe<T> be,
        HttpStatusCode expectedCode
    ) where T : IActionResult
    {
        return be.Accepted(
            expectedCode,
            NULL_STRING
        );
    }

    /// <summary>
    /// Verifies that the request has
    /// the required status code
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expectedCode"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Accepted<T>(
        this IBe<T> be,
        HttpStatusCode expectedCode,
        string customMessage
    ) where T : IActionResult
    {
        return be.Accepted(
            expectedCode,
            () => customMessage
        );
    }

    /// <summary>
    /// Verifies that the request has
    /// the required status code
    /// </summary>
    /// <param name="be"></param>
    /// <param name="expectedCode"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Accepted<T>(
        this IBe<T> be,
        HttpStatusCode expectedCode,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.AddMatcher(
            actionResult =>
            {
                var response = actionResult?.ResolveResponse();
                if (response is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        () => @"Response has no content.
If your assertion is that it should not be rejected,
rather specify .To.Not.Be.Accepted() instead of specifying
an http status code to not expect, otherwise one couldn't
specify that a result _should_ be rejected, just _not_ with
the specified code. This is in the interest of making tests
as precise as possible."
                    );
                }

                var passed = response?.StatusCode == (int)expectedCode;

                var verb = ((int)expectedCode >= 200 && (int)expectedCode <= 300)
                    ? "accepted"
                    : "rejected";

                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $@"Expected request {
                            passed.AsNot()
                        }to have been {verb} with a {
                            (int)expectedCode
                        } {expectedCode}, but found {response.StatusCode}",
                        customMessageGenerator
                    )
                );
            }
        );
    }

    /// <summary>
    /// Verifies that an action result is for
    /// a rejected request (http status code >= 400)
    /// </summary>
    /// <param name="be"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Rejected(NULL_STRING);
    }

    /// <summary>
    /// Verifies that an action result is for
    /// a rejected request (http status code >= 400)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected<T>(
        this IBe<T> be,
        string customMessage
    ) where T : IActionResult
    {
        return be.Rejected(() => customMessage);
    }

    /// <summary>
    /// Verifies that an action result is for
    /// a rejected request (http status code >= 400)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected<T>(
        this IBe<T> be,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.AddMatcher(
            actionResult =>
            {
                var response = actionResult?.ResolveResponse();
                var passed = response is not null &&
                    response.StatusCode is >= 400;

                var more = response is null
                    ? "no response"
                    : $"have code {response.StatusCode.ToString()}";
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $@"Expected request {
                            passed.AsNot()
                        }to have been rejected ({more})",
                        customMessageGenerator
                    )
                );
            }
        );
    }

    /// <summary>
    /// Verifies that the action result is
    /// for a request which was rejected
    /// with status code 400 (Bad Request)
    /// </summary>
    /// <param name="be"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected401<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Rejected401(NULL_STRING);
    }

    /// <summary>
    /// Verifies that the action result is
    /// for a request which was rejected
    /// with status code 400 (Bad Request)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected401<T>(
        this IBe<T> be,
        string customMessage
    ) where T : IActionResult
    {
        return be.Rejected401(() => customMessage);
    }

    /// <summary>
    /// Verifies that the action result is
    /// for a request which was rejected
    /// with status code 401 (Unauthorized)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected401<T>(
        this IBe<T> be,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.Rejected(HttpStatusCode.Unauthorized, customMessageGenerator);
    }

    /// <summary>
    /// Verifies that the action result is
    /// for a request which was rejected
    /// with status code 403 (Forbidden)
    /// </summary>
    /// <param name="be"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected403<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Rejected403(NULL_STRING);
    }

    /// <summary>
    /// Verifies that the action result is
    /// for a request which was rejected
    /// with status code 403 (Forbidden)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected403<T>(
        this IBe<T> be,
        string customMessage
    ) where T : IActionResult
    {
        return be.Rejected403(() => customMessage);
    }

    /// <summary>
    /// Verifies that the action result is
    /// for a request which was rejected
    /// with status code 403 (Forbidden)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected403<T>(
        this IBe<T> be,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.Rejected(HttpStatusCode.Forbidden, customMessageGenerator);
    }

    /// <summary>
    /// Verifies that the action result is
    /// for a request which was rejected
    /// with status code 404 (Not Found)
    /// </summary>
    /// <param name="be"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected404<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Rejected404(NULL_STRING);
    }

    /// <summary>
    /// Verifies that the action result is
    /// for a request which was rejected
    /// with status code 404 (Not Found)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected404<T>(
        this IBe<T> be,
        string customMessage
    ) where T : IActionResult
    {
        return be.Rejected404(() => customMessage);
    }

    /// <summary>
    /// Verifies that the action result is
    /// for a request which was rejected
    /// with status code 404 (Not Found)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Rejected404<T>(
        this IBe<T> be,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.Rejected(HttpStatusCode.NotFound, customMessageGenerator);
    }

    /// <summary>
    /// Verifies that the action result is
    /// for a request which was rejected
    /// with status code 404 (Not Found)
    /// </summary>
    /// <param name="be"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> NotFound<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.NotFound(NULL_STRING);
    }

    /// <summary>
    /// Verifies that the action result is
    /// for a request which was rejected
    /// with status code 404 (Not Found)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> NotFound<T>(
        this IBe<T> be,
        string customMessage
    ) where T : IActionResult
    {
        return be.NotFound(() => customMessage);
    }

    /// <summary>
    /// Verifies that the action result is
    /// for a request which was rejected
    /// with status code 404 (Not Found)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> NotFound<T>(
        this IBe<T> be,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.Rejected(HttpStatusCode.NotFound, customMessageGenerator);
    }
}