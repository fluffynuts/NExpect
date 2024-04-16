using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Imported.PeanutButter.TestUtils.AspNetCore;
using Imported.PeanutButter.Utils.Dictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using Imported.PeanutButter.Utils;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides matchers for action results, eg
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
    public static IMore<IActionResult> Redirect(
        this ITo<IActionResult> to
    )
    {
        return to.Redirect(NULL_STRING);
    }

    /// <summary>
    /// Verify that an action result is a redirection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<IActionResult> Redirect(
        this ITo<IActionResult> to,
        string customMessage
    )
    {
        return to.Redirect(() => customMessage);
    }

    /// <summary>
    /// Verify that an action result is a redirection
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<IActionResult> Redirect(
        this ITo<IActionResult> to,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
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

    private static IMatcherResult CreateActionMatcherFor(
        IActionResult actual,
        string actionName,
        Func<string> customMessageGenerator
    )
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
    public static IMore<ActionResult> CurrentController(
        this IOn<ActionResult> on
    )
    {
        return on.CurrentController(NULL_STRING);
    }

    /// <summary>
    /// Verifies that the redirection is to an action on the current controller
    /// </summary>
    /// <param name="on"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<ActionResult> CurrentController(
        this IOn<ActionResult> on,
        string customMessage
    )
    {
        return on.CurrentController(() => customMessage);
    }

    /// <summary>
    /// Verifies that the redirection is to an action on the current controller
    /// </summary>
    /// <param name="on"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<ActionResult> CurrentController(
        this IOn<ActionResult> on,
        Func<string> customMessageGenerator
    )
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
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<ActionResult> Controller(
        this IOn<ActionResult> on,
        string controller,
        Func<string> customMessageGenerator
    )
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

    private static MatcherResult CantReadRouteValuesOn(
        ActionResult actionResult,
        out RouteValueDictionary routeValues
    )
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
    public static IMore<ActionResult> Area(
        this IIn<ActionResult> @in,
        string area
    )
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
    public static IMore<ActionResult> Area(
        this IIn<ActionResult> @in,
        string area,
        string customMessage
    )
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
    public static IMore<ActionResult> Area(
        this IIn<ActionResult> @in,
        string area,
        Func<string> customMessageGenerator
    )
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
    /// </summary>
    /// <param name="in"></param>
    /// <returns></returns>
    public static IMore<ActionResult> Root(
        this IIn<ActionResult> @in
    )
    {
        return @in.Root(NULL_STRING);
    }

    /// <summary>
    /// Verifies that the redirect is to the root of the application (/)
    /// </summary>
    /// <param name="in"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<ActionResult> Root(
        this IIn<ActionResult> @in,
        string customMessage
    )
    {
        return @in.Root(() => customMessage);
    }

    /// <summary>
    /// Verifies that the redirect is to the root of the application (/)
    /// </summary>
    /// <param name="in"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<ActionResult> Root(
        this IIn<ActionResult> @in,
        Func<string> customMessageGenerator
    )
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
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<ActionResult> Parameters(
        this IWith<ActionResult> with,
        object parameters,
        Func<string> customMessageGenerator
    )
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

                var mismatches = expected.Keys.Aggregate(
                    new Dictionary<string, object>(),
                    (acc, cur) =>
                    {
                        var areEqual = expected[cur]?.Equals(routeValues[cur]) ?? false;
                        if (!areEqual)
                        {
                            acc[cur] =
                                $"expected {expected[cur].Stringify()} but received {routeValues[cur].Stringify()}";
                        }

                        return acc;
                    }
                );
                var haveAllParameters = mismatches.Count == 0;

                return new MatcherResult(
                    haveAllParameters,
                    () =>
                    {
                        var errors = mismatches.Keys.Aggregate(
                            new List<string>(),
                            (acc, cur) =>
                            {
                                acc.Add($"{cur}: {mismatches[cur]}");
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
        return have.Content<T>(expected, NULL_STRING);
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
        return ValidateStatusCode(be, HttpStatusCode.Forbidden);
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
        return ValidateStatusCode(be, HttpStatusCode.OK);
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
        return ValidateStatusCode(have, expected);
    }

    private static IMore<T> ValidateStatusCode<T>(
        this ICanAddMatcher<T> canAddMatcher,
        HttpStatusCode expected
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
                    () => $"Expected {passed.AsNot()}to find status code {expected}, {more}"
                );
            }
        );
    }

    public static IMore<T> Rejected<T>(
        this IBe<T> be,
        HttpStatusCode statusCode
    ) where T : IActionResult
    {
        return be.Rejected(statusCode, () => null);
    }

    public static IMore<T> Rejected<T>(
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

    public static IMore<T> Created201<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Resolved(HttpStatusCode.Created, () => null);
    }

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

    public static IMore<T> Accepted<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Accepted(null);
    }

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
                        }to have been accepted with a {
                            (int)expectedCode
                        } {expectedCode}, but found {response.StatusCode}",
                        customMessageGenerator
                    )
                );
            }
        );
    }

    public static IMore<T> Rejected<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Rejected(null);
    }

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

    public static IMore<T> Rejected401<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Rejected401(() => null);
    }

    public static IMore<T> Rejected401<T>(
        this IBe<T> be,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.Rejected(HttpStatusCode.Unauthorized, customMessageGenerator);
    }

    public static IMore<T> Rejected403<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Rejected403(() => null);
    }

    public static IMore<T> Rejected403<T>(
        this IBe<T> be,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.Rejected(HttpStatusCode.Forbidden, customMessageGenerator);
    }

    public static IMore<T> Rejected404<T>(
        this IBe<T> be
    ) where T : IActionResult
    {
        return be.Rejected404(() => null);
    }

    public static IMore<T> Rejected404<T>(
        this IBe<T> be,
        Func<string> customMessageGenerator
    ) where T : IActionResult
    {
        return be.Rejected(HttpStatusCode.NotFound, customMessageGenerator);
    }
}