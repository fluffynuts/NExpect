using System;
using System.Linq;
using System.Net.Http;
using Imported.PeanutButter.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect
{
    /// <summary>
    /// Adds matchers for AspNetCore Controllers and Controller Actions
    /// </summary>
    public static class ControllerMatchers
    {
        /// <summary>
        /// Asserts that the controller has the named method
        /// </summary>
        /// <param name="have"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static SupportingExtension Method(
            this IHave<Type> have,
            string method
        )
        {
            return have.Method(method, NULL_STRING);
        }

        /// <summary>
        /// Asserts that the controller has the named method
        /// </summary>
        /// <param name="have"></param>
        /// <param name="method"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static SupportingExtension Method(
            this IHave<Type> have,
            string method,
            string customMessage
        )
        {
            return have.Method(method, () => customMessage);
        }

        /// <summary>
        /// Asserts that the controller has the named method
        /// </summary>
        /// <param name="have"></param>
        /// <param name="method"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static SupportingExtension Method(
            this IHave<Type> have,
            string method,
            Func<string> customMessageGenerator
        )
        {
            var result = new SupportingExtension();
            have.AddMatcher(
                actual =>
                {
                    result.Member = method;
                    result.Continuation = have;
                    var passed = actual.GetMethod(method) != null;
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => $"Expected type {actual} to have method {method}",
                            customMessageGenerator
                        )
                    );
                });
            return result;
        }

        /// <summary>
        /// Asserts that the controller has the expected base route
        /// </summary>
        /// <param name="have"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static IMore<Type> Route(
            this IHave<Type> have,
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
        public static IMore<Type> Route(
            this IHave<Type> have,
            string expected,
            Func<string> customMessageGenerator
        )
        {
            have.AddMatcher(
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
                });
            return have.More();
        }

        /// <summary>
        /// Quick-n-dirty assertion that a controller's method has the required route
        /// </summary>
        /// <param name="have"></param>
        /// <param name="member"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static IMore<Type> Route(
            this IHave<Type> have,
            string member,
            string expected)
        {
            have.AddMatcher(
                actual =>
                {
                    var method = actual.GetMethod(member);
                    if (method == null)
                    {
                        return new MatcherResult(
                            false,
                            () => $"Expected {false.AsNot()}to find method {actual}.{method}"
                        );
                    }

                    var attribs = method.GetCustomAttributes(false).OfType<RouteAttribute>();
                    var passed = attribs.Any(a => a.Template == expected);
                    return new MatcherResult(
                        passed,
                        () => $"Expected {actual}.{method} {passed.AsNot()}to have route '{expected}'"
                    );
                });
            return have.More();
        }

        /// <summary>
        /// Provides fluency for Supporting()
        /// </summary>
        public class SupportingExtension
        {
            internal string Member { get; set; }
            internal IHave<Type> Continuation { get; set; }

            /// <summary>
            /// Asserts that the controller method being operated on supports
            /// the desired HttpMethod
            /// </summary>
            /// <param name="method"></param>
            /// <returns></returns>
            public AndSupportingExtension Supporting(HttpMethod method)
            {
                Continuation.AddMatcher(
                    controllerType =>
                    {
                        var supportedMethods = controllerType.GetMethod(Member)
                            ?.GetCustomAttributes(false)
                            .Select(attrib => attrib as IActionHttpMethodProvider)
                            .Where(a => a != null)
                            .SelectMany(a => a.HttpMethods)
                            .Distinct()
                            .ToArray();
                        var isImplicitHttp = method == HttpMethod.Get &&
                            supportedMethods.None();
                        var passed = isImplicitHttp || (
                            supportedMethods?.Any(
                                m => m.Equals(method.Method, StringComparison.OrdinalIgnoreCase)
                            ) ?? false
                        );
                        return new MatcherResult(
                            passed,
                            () => $"Expected {controllerType}.{Member} to support HttpMethod {method}"
                        );
                    });
                return Next();
            }

            /// <summary>
            /// Fluency extension
            /// </summary>
            public SupportingExtension With => this;

            /// <summary>
            /// Fluency extension
            /// </summary>
            public SupportingExtension And => this;

            /// <summary>
            /// Asserts that the controller action being operated on has the specified route
            /// </summary>
            /// <param name="expected"></param>
            /// <returns></returns>
            public SupportingExtension Route(string expected)
            {
                Continuation.AddMatcher(
                    controllerType =>
                    {
                        var routes = controllerType.GetMethod(Member)
                            ?.GetCustomAttributes(false)
                            .OfType<RouteAttribute>()
                            .Select(a => a.Template)
                            .ToArray();
                        var passed = routes?.Contains(expected) ?? false;
                        return new MatcherResult(
                            passed,
                            () =>
                            {
                                var start = $"Expected {controllerType}.{Member} to have route '{expected}'";
                                var count = routes.Count();
                                var no = count == 0
                                    ? "no "
                                    : "";
                                var s = count == 1
                                    ? ""
                                    : "s";
                                var colon = count > 0
                                    ? ":"
                                    : "";
                                return string.Join("\n", new[]
                                    {
                                        start,
                                        $"Have {no}route{s}{colon}"
                                    }
                                    .Concat(routes.Select(r => $" - {r}")));
                            }
                        );
                    });
                return this;
            }

            private AndSupportingExtension Next()
            {
                return new AndSupportingExtension(Continuation, Member, this);
            }
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
            return have.AddMatcher(actual =>
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
                            () => $"Expected type {actual} {false.AsNot()} to be decorated with [Area(\"{areaName}\")]",
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
            });
        }

        /// <summary>
        /// Fluency extension
        /// </summary>
        public class AndSupportingExtension
        {
            private readonly string _member;
            private readonly IHave<Type> _continuation;

            /// <summary>
            /// Fluency extension
            /// </summary>
            public SupportingExtension With { get; }

            internal AndSupportingExtension(
                IHave<Type> continuation,
                string member,
                SupportingExtension supportingExtension)
            {
                _continuation = continuation;
                _member = member;
                With = supportingExtension;
            }

            /// <summary>
            /// Asserts that the controller method being operated on has an
            /// additional supported HttpMethod
            /// </summary>
            /// <param name="method"></param>
            /// <returns></returns>
            public AndSupportingExtension And(HttpMethod method)
            {
                return (new SupportingExtension()
                {
                    Member = _member,
                    Continuation = _continuation
                }).Supporting(method);
            }
        }
    }
}