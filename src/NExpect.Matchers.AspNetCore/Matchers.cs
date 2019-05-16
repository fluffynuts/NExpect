using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect
{
    /// <summary>
    /// Adds matchers for AspNetCore Controllers and Controller Actions
    /// </summary>
    public static class Matchers
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
            var result = new SupportingExtension();
            have.AddMatcher(
                actual =>
                {
                    result.Member = method;
                    result.Continuation = have;
                    var passed = actual.GetMethod(method) != null;
                    return new MatcherResult(
                        passed,
                        () => $"Expected type {actual} to have method {method}");
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
            string expected)
        {
            have.AddMatcher(
                actual =>
                {
                    var attribs = actual.GetCustomAttributes(false).OfType<RouteAttribute>();
                    var passed = attribs.Any(a => a.Template == expected);
                    return new MatcherResult(
                        passed,
                        () => $"Expected {actual.Name} {passed.AsNot()}to have route '{expected}'"
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
                        var passed = supportedMethods
                                ?.Any(m => m.Equals(method.Method, StringComparison.OrdinalIgnoreCase))
                            ?? false;
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