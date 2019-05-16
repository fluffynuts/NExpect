using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Matchers.AspNet
{
    public static class Matchers
    {
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

        public class SupportingExtension
        {
            public string Member { get; set; }
            public IHave<Type> Continuation { get; set; }

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

            public SupportingExtension With => this;
            public SupportingExtension And => this;

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

        public class AndSupportingExtension
        {
            private readonly string _member;
            private readonly IHave<Type> _continuation;

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