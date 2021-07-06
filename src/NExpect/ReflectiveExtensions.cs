using System;
using System.Linq;
using System.Reflection;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect
{
    /// <summary>
    /// Performs expectations reflectively
    /// </summary>
    public static class ReflectiveExtensions
    {
        /// <summary>
        /// Tests if an object or Type has a method with the provided name
        /// </summary>
        /// <param name="have"></param>
        /// <param name="methodName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Method<T>(
            this IHave<T> have,
            string methodName
        )
        {
            return have.Method(
                methodName,
                NULL_STRING
            );
        }

        /// <summary>
        /// Tests if an object or Type has a method with the provided name
        /// </summary>
        /// <param name="have"></param>
        /// <param name="methodName"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Method<T>(
            this IHave<T> have,
            string methodName,
            string customMessage
        )
        {
            return have.Method(
                methodName,
                () => customMessage
            );
        }

        /// <summary>
        /// Tests if an object or Type has a method with the provided name
        /// </summary>
        /// <param name="have"></param>
        /// <param name="methodName"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Method<T>(
            this IHave<T> have,
            string methodName,
            Func<string> customMessageGenerator
        )
        {
            return have.AddMatcher(actual =>
            {
                if (actual is null)
                {
                    return Fail("actual is null");
                }

                var type = actual as Type ?? typeof(T);
                var methodInfos = type.GetMethods(
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static
                    ).Where(mi => mi.Name == methodName)
                    .ToArray();
                var methodInfo = methodInfos.FirstOrDefault();
                var haveMultiple = methodInfos.Length > 1;
                var passed = methodInfo is not null && !haveMultiple;
                actual.SetMetadata(METADATA_KEY_METHOD_INFO, methodInfo);

                return new MatcherResultWithNext<MethodInfo>(
                    passed,
                    () =>
                        haveMultiple
                            ? $"Expected '{type.PrettyName()}' {passed.AsNot()}to have a single method called '{methodName}' (perhaps you need a discriminator?)"
                            : $"Expected '{type.PrettyName()}' {passed.AsNot()}to have method '{methodName}'",
                    customMessageGenerator,
                    () => methodInfo
                );
            });

            IMatcherResultWithNext<MethodInfo> Fail(string message)
            {
                return new MatcherResultWithNext<MethodInfo>(
                    false,
                    () => message,
                    customMessageGenerator,
                    () => default
                );
            }
        }

        /// <summary>
        /// Tests for a method by name and discriminator
        /// - use if you're trying to discern between overloads
        /// </summary>
        /// <param name="have"></param>
        /// <param name="methodName"></param>
        /// <param name="discriminator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Method<T>(
            this IHave<T> have,
            string methodName,
            Func<MethodInfo, bool> discriminator
        )
        {
            return have.Method(
                methodName,
                discriminator,
                NULL_STRING
            );
        }

        /// <summary>
        /// Tests for a method by name and discriminator
        /// - use if you're trying to discern between overloads
        /// </summary>
        /// <param name="have"></param>
        /// <param name="methodName"></param>
        /// <param name="discriminator"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Method<T>(
            this IHave<T> have,
            string methodName,
            Func<MethodInfo, bool> discriminator,
            string customMessage
        )
        {
            return have.Method(
                methodName,
                discriminator,
                () => customMessage
            );
        }

        /// <summary>
        /// Tests for a method by name and discriminator
        /// - use if you're trying to discern between overloads
        /// </summary>
        /// <param name="have"></param>
        /// <param name="methodName"></param>
        /// <param name="discriminator"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Method<T>(
            this IHave<T> have,
            string methodName,
            Func<MethodInfo, bool> discriminator,
            Func<string> customMessageGenerator
        )
        {
            return have.AddMatcher(actual =>
            {
                if (actual is null)
                {
                    return Fail("actual is null");
                }

                var type = actual as Type ?? typeof(T);
                var matchesName = type.GetMethods(
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static
                    )
                    .Where(mi => mi.Name == methodName)
                    .ToArray();
                var allDiscriminatorMatches = matchesName
                    .Where(
                        mi => mi.Name == methodName && discriminator(mi)
                    ).ToArray();
                var methodInfo = allDiscriminatorMatches.FirstOrDefault();
                var passed = methodInfo is not null && allDiscriminatorMatches.Length == 1;
                return new MatcherResultWithNext<MethodInfo>(
                    passed,
                    () =>
                    {
                        var single = matchesName.Length == 1;
                        var moreInfo = matchesName.Any()
                            ? $" (there {(single ? "was" : "were")} {matchesName.Length} match{(single ? "" : "es")} by name '{methodName}')"
                            : "";
                        if (matchesName.Any() && allDiscriminatorMatches.Length > 1)
                        {
                            moreInfo = moreInfo.RegexReplace(")$",
                                " - there were multiple matches for your provided discriminator)");
                        }

                        return $@"Expected {
                            type.PrettyName()
                        } to have one matching method for name '{
                            methodName
                        }' and provided discriminator{
                            moreInfo
                        }";
                    },
                    customMessageGenerator,
                    () => methodInfo
                );
            });

            IMatcherResultWithNext<MethodInfo> Fail(string message)
            {
                return new MatcherResultWithNext<MethodInfo>(
                    false,
                    () => message,
                    customMessageGenerator,
                    () => default
                );
            }
        }

        /// <summary>
        /// Asserts against an expected attribute, with a matcher for the attribute
        /// </summary>
        /// <param name="with"></param>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Attribute<TAttribute>(
            this IWith<MethodInfo> with
        ) where TAttribute : Attribute
        {
            return with.Attribute<TAttribute>(NULL_STRING);
        }

        /// <summary>
        /// Asserts against an expected attribute, with a matcher for the attribute
        /// </summary>
        /// <param name="with"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Attribute<TAttribute>(
            this IWith<MethodInfo> with,
            string customMessage
        ) where TAttribute : Attribute
        {
            return with.Attribute<TAttribute>(() => customMessage);
        }

        /// <summary>
        /// Asserts against an expected attribute, with a matcher for the attribute
        /// </summary>
        /// <param name="with"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Attribute<TAttribute>(
            this IWith<MethodInfo> with,
            Func<string> customMessageGenerator
        ) where TAttribute : Attribute
        {
            return with.Attribute(
                null as Func<TAttribute, bool>,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Asserts against an expected attribute, with a matcher for the attribute
        /// </summary>
        /// <param name="with"></param>
        /// <param name="matcher"></param>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Attribute<TAttribute>(
            this IWith<MethodInfo> with,
            Func<TAttribute, bool> matcher
        ) where TAttribute : Attribute
        {
            return with.Attribute(
                matcher,
                NULL_STRING
            );
        }

        /// <summary>
        /// Asserts against an expected attribute, with a matcher for the attribute
        /// </summary>
        /// <param name="with"></param>
        /// <param name="matcher"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Attribute<TAttribute>(
            this IWith<MethodInfo> with,
            Func<TAttribute, bool> matcher,
            string customMessage
        ) where TAttribute : Attribute
        {
            return with.Attribute(
                matcher, () => customMessage
            );
        }

        /// <summary>
        /// Asserts against an expected attribute, with a matcher for the attribute
        /// </summary>
        /// <param name="with"></param>
        /// <param name="matcher"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Attribute<TAttribute>(
            this IWith<MethodInfo> with,
            Func<TAttribute, bool> matcher,
            Func<string> customMessageGenerator
        ) where TAttribute : Attribute
        {
            matcher ??= _ => true;
            return with.AddMatcher(actual =>
                TryMatchAnyParameterAttribute(actual, matcher, customMessageGenerator)
                ?? TryMatchParameterAttribute(actual, matcher, customMessageGenerator)
                ?? MatchMethodAttribute(actual, matcher, customMessageGenerator)
            );
        }

        private static MatcherResult MatchMethodAttribute<TAttribute>(
            MethodInfo actual,
            Func<TAttribute, bool> matcher,
            Func<string> customMessageGenerator
        ) where TAttribute : Attribute
        {
            var attribs = actual.GetCustomAttributes()
                .OfType<TAttribute>()
                .Where(matcher)
                .ToArray();
            var passed = attribs.Length > 0;
            return new MatcherResult(
                passed,
                () => $@"Expected {passed.AsNot()}to find {
                    actual.FullName()
                } decorated with {Attrib<TAttribute>()}",
                customMessageGenerator
            );
        }

        private static MatcherResult TryMatchAnyParameterAttribute<TAttribute>(
            MethodInfo actual,
            Func<TAttribute, bool> matcher,
            Func<string> customMessageGenerator
        ) where TAttribute : Attribute
        {
            if (!actual.TryGetMetadata<ParameterInfo[]>(METADATA_KEY_PARAMETER_INFOS, out var parameterInfos))
            {
                return null;
            }

            var attribs = parameterInfos.Select(pi => pi.GetCustomAttributes().OfType<TAttribute>())
                .SelectMany(o => o)
                .Where(matcher)
                .ToArray();
            var passed = attribs.Length == 1;
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () => attribs.Length == 0
                        ? $"Expected {passed.AsNot()}to find a parameter decorated with {Attrib<TAttribute>()}"
                        : $"Expected {passed.AsNot()}to find a single parameter decorated with {Attrib<TAttribute>()} but found {attribs.Length}",
                    customMessageGenerator
                )
            );
        }

        private static MatcherResult TryMatchParameterAttribute<TAttribute>(
            MethodInfo actual,
            Func<TAttribute, bool> matcher,
            Func<string> customMessageGenerator
        ) where TAttribute : Attribute
        {
            if (!actual.TryGetMetadata<ParameterInfo>(METADATA_KEY_PARAMETER_INFO, out var parameterInfo))
            {
                return null;
            }

            var attribs = parameterInfo.GetCustomAttributes()
                .OfType<TAttribute>()
                .Where(matcher)
                .ToArray();
            var passed = attribs.Length == 1;
            return new MatcherResult(
                passed,
                () => FinalMessageFor(
                    () =>
                        $"Expected parameter '{parameterInfo.Name}' of method '{actual.Name}' to be decorated with {Attrib<TAttribute>()}",
                    customMessageGenerator
                )
            );
        }

        private static string FullName(this MethodInfo mi)
        {
            return $"{mi.DeclaringType.PrettyName()}.{mi.Name}";
        }

        private static string Attrib<TAttrib>()
            where TAttrib : Attribute
        {
            return $"[{typeof(TAttrib).Name.RegexReplace("Attribute$", "")}]";
        }

        /// <summary>
        /// Tests if an object has a given property, may continue on to test
        /// the property value
        /// </summary>
        /// <param name="have"></param>
        /// <param name="property"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Property<T>(
            this IHave<T> have,
            string property
        )
        {
            return have.Property(property, NULL_STRING);
        }

        /// <summary>
        /// Tests if an object has a given property, may continue on to test
        /// the property value
        /// </summary>
        /// <param name="have"></param>
        /// <param name="property"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Property<T>(
            this IHave<T> have,
            string property,
            string customMessage
        )
        {
            return have.Property(property, () => customMessage);
        }

        /// <summary>
        /// Tests if an object has a given property, may continue on to test
        /// the property value
        /// </summary>
        /// <param name="have"></param>
        /// <param name="property"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IMore<T> Property<T>(
            this IHave<T> have,
            string property,
            Func<string> customMessageGenerator
        )
        {
            return (have as ICanAddMatcher<T>).Property(property, customMessageGenerator);
        }

        /// <summary>
        /// Tests if an object has a given property, may continue on to test
        /// the property value
        /// </summary>
        /// <param name="have"></param>
        /// <param name="property"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Property<T>(
            this IAnd<T> have,
            string property
        )
        {
            return have.Property(property, NULL_STRING);
        }

        /// <summary>
        /// Tests if an object has a given property, may continue on to test
        /// the property value
        /// </summary>
        /// <param name="have"></param>
        /// <param name="property"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Property<T>(
            this IAnd<T> have,
            string property,
            string customMessage
        )
        {
            return have.Property(property, () => customMessage);
        }

        /// <summary>
        /// Tests if an object has a given property, may continue on to test
        /// the property value
        /// </summary>
        /// <param name="have"></param>
        /// <param name="property"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IMore<T> Property<T>(
            this IAnd<T> have,
            string property,
            Func<string> customMessageGenerator
        )
        {
            return (have as ICanAddMatcher<T>).Property(property, customMessageGenerator);
        }

        private const BindingFlags PUBLIC_INSTANCE = BindingFlags.Public | BindingFlags.Instance;

        private static IMore<T> Property<T>(
            this ICanAddMatcher<T> canAddMatcher,
            string property,
            Func<string> customMessageGenerator
        )
        {
            return canAddMatcher.AddMatcher(actual =>
            {
                if (string.IsNullOrWhiteSpace(property))
                {
                    return Fail(() => "Provided property name is null or whitespace");
                }

                if (actual is null)
                {
                    return Fail(() => "Actual value is null");
                }

                actual.SetMetadata(METADATA_KEY_PROPERTY_NAME, property);
                var actualType = actual.GetType();
                if (actualType.Name == "RuntimeType" && actualType.Namespace == "System")
                {
                    actualType = (Type)(actual as object);
                }

                var propInfo = actualType.GetProperty(property, PUBLIC_INSTANCE);
                if (propInfo is null)
                {
                    return new MatcherResult(
                        false,
                        () =>
                            $@"Expected {
                                actualType.PrettyName()
                            } {
                                false.AsNot()
                            }to have a public property named '{property}'"
                    );
                }


                actual.SetMetadata(METADATA_KEY_PROPERTY_INFO, propInfo);
                return new MatcherResult(
                    true,
                    FinalMessageFor(
                        () => $"Expected {actual.Stringify()} {true.AsNot()}to have property '{property}'",
                        customMessageGenerator
                    )
                );
            });

            MatcherResult Fail(Func<string> defaultGenerator)
            {
                return new MatcherResult(
                    false,
                    () => FinalMessageFor(
                        defaultGenerator,
                        customMessageGenerator
                    )
                );
            }
        }

        private const string METADATA_KEY_PROPERTY_NAME = "__PropertyName__";
        private const string METADATA_KEY_PROPERTY_INFO = "__PropertyInfo__";

        private const string METADATA_KEY_METHOD_INFO = "__MethodInfo__";

        private const string METADATA_KEY_PARAMETER_INFO = "__ParameterInfo__";
        private const string METADATA_KEY_PARAMETER_INFOS = "__ParameterInfos__";

        /// <summary>
        /// Continues testing a named property for an expected value
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <typeparam name="T"></typeparam>
        public static IMore<T> Value<T>(
            this IWith<T> with,
            object expected
        )
        {
            return with.Value<T>(expected, NULL_STRING);
        }


        /// <summary>
        /// Continues testing a named property for an expected value
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        public static IMore<T> Value<T>(
            this IWith<T> with,
            object expected,
            string customMessage
        )
        {
            return with.Value(expected, () => customMessage);
        }

        /// <summary>
        /// Continues testing a named property for an expected value
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        public static IMore<T> Value<T>(
            this IWith<T> with,
            object expected,
            Func<string> customMessageGenerator
        )
        {
            return (with as ICanAddMatcher<T>).Value(expected, customMessageGenerator);
        }

        /// <summary>
        /// Continues testing a named property for an expected value
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <typeparam name="T"></typeparam>
        public static IMore<T> Value<T>(
            this IAnd<T> with,
            object expected
        )
        {
            return with.Value<T>(expected, NULL_STRING);
        }


        /// <summary>
        /// Continues testing a named property for an expected value
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        public static IMore<T> Value<T>(
            this IAnd<T> with,
            object expected,
            string customMessage
        )
        {
            return with.Value(expected, () => customMessage);
        }

        /// <summary>
        /// Continues testing a named property for an expected value
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        public static IMore<T> Value<T>(
            this IAnd<T> with,
            object expected,
            Func<string> customMessageGenerator
        )
        {
            return (with as ICanAddMatcher<T>).Value(expected, customMessageGenerator);
        }

        private static IMore<T> Value<T>(
            this ICanAddMatcher<T> with,
            object expected,
            Func<string> customMessageGenerator
        )
        {
            return with.AddMatcher(actual =>
            {
                if (!actual.TryGetMetadata<PropertyInfo>(
                        METADATA_KEY_PROPERTY_INFO,
                        out var propInfo
                    )
                )
                {
                    actual.TryGetMetadata<string>(METADATA_KEY_PROPERTY_NAME, out var propName);

                    return new MatcherResult(
                        true,
                        FinalMessageFor(
                            () => propName is null
                                ? "actual value was null"
                                : $"actual missing property {propName}",
                            customMessageGenerator
                        )
                    );
                }

                var actualValue = propInfo.GetValue(actual);

                var passed =
                    actualValue is null && expected is null ||
                    (actualValue?.Equals(expected) ?? false) ||
                    (actualValue?.DeepEquals(expected) ?? false);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $@"expected {
                            passed.AsNot()
                        }to find value of {
                            expected.Stringify()
                        } for property '{
                            propInfo.Name
                        }', but found {
                            actualValue.Stringify()
                        }",
                        customMessageGenerator
                    )
                );
            });
        }

        /// <summary>
        /// Tests the type on a property With continuation
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Type<T>(
            this IWith<T> with,
            Type expected
        )
        {
            return with.Type(expected, NULL_STRING);
        }

        /// <summary>
        /// Tests the type on a property With continuation
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Type<T>(
            this IWith<T> with,
            Type expected,
            string customMessage
        )
        {
            return with.Type(expected, () => customMessage);
        }

        /// <summary>
        /// Tests the type on a property With continuation
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Type<T>(
            this IWith<T> with,
            Type expected,
            Func<string> customMessageGenerator
        )
        {
            return (with as ICanAddMatcher<T>).Type(expected, customMessageGenerator);
        }

        /// <summary>
        /// Tests the type on a property With continuation
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Type<T>(
            this IAnd<T> with,
            Type expected
        )
        {
            return with.Type(expected, NULL_STRING);
        }

        /// <summary>
        /// Tests the type on a property With continuation
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Type<T>(
            this IAnd<T> with,
            Type expected,
            string customMessage
        )
        {
            return with.Type(expected, () => customMessage);
        }

        /// <summary>
        /// Tests the type on a property With continuation
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Type<T>(
            this IAnd<T> with,
            Type expected,
            Func<string> customMessageGenerator
        )
        {
            return (with as ICanAddMatcher<T>).Type(expected, customMessageGenerator);
        }

        /// <summary>
        /// Tests the type on a property / method-info With continuation
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Type<T>(
            this IOf<T> with,
            Type expected
        )
        {
            return with.Type(expected, NULL_STRING);
        }

        /// <summary>
        /// tests type on a method-info With continuation
        /// </summary>
        /// <param name="of"></param>
        /// <returns></returns>
        public static IMore<MethodInfo> Type<TExpected>(
            this IOf<MethodInfo> of
        )
        {
            return of.Type<TExpected>(NULL_STRING);
        }

        /// <summary>
        /// tests type on a method-info With continuation
        /// </summary>
        /// <param name="of"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="TExpected"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Type<TExpected>(
            this IOf<MethodInfo> of,
            string customMessage
        )
        {
            return of.Type<TExpected>(
                () => customMessage
            );
        }

        /// <summary>
        /// tests type on a method-info with continuation
        /// </summary>
        /// <param name="of"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="TExpected"></typeparam>
        /// <returns></returns>
        public static IMore<MethodInfo> Type<TExpected>(
            this IOf<MethodInfo> of,
            Func<string> customMessageGenerator
        )
        {
            return of.Type(
                typeof(TExpected),
                customMessageGenerator
            );
        }

        /// <summary>
        /// Tests the type on a property With continuation
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Type<T>(
            this IOf<T> with,
            Type expected,
            string customMessage
        )
        {
            return with.Type(expected, () => customMessage);
        }

        /// <summary>
        /// Tests the type on a property With continuation
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expected"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Type<T>(
            this IOf<T> with,
            Type expected,
            Func<string> customMessageGenerator
        )
        {
            return (with as ICanAddMatcher<T>).Type(expected, customMessageGenerator);
        }

        private static IMore<T> Type<T>(
            this ICanAddMatcher<T> canAddMatcher,
            Type expected,
            Func<string> customMessageGenerator
        )
        {
            return canAddMatcher.AddMatcher(actual =>
                TryMatchPropertyType(actual, expected, customMessageGenerator)
                ?? TryMatchAnyParameterType(actual, expected, customMessageGenerator)
                ?? TryMatchParameterType(actual, expected, customMessageGenerator)
                ?? throw new NotImplementedException(
                    $"Type matching not implemented for this case"
                ));
        }

        private static MatcherResult TryMatchAnyParameterType<T>(
            T actual,
            Type expected,
            Func<string> customMessageGenerator
        )
        {
            if (!actual.TryGetMetadata<ParameterInfo[]>(METADATA_KEY_PARAMETER_INFOS, out var propertyInfos))
            {
                return null;
            }

            if (!actual.TryGetMetadata<MethodInfo>(METADATA_KEY_METHOD_INFO, out var methodInfo))
            {
                methodInfo = actual as MethodInfo;
            }

            var matching = propertyInfos.Where(pi => pi.ParameterType == expected)
                .ToArray();
            // filter down for subsequent testing - if testing without
            // a name (ie only with type)
            actual.SetMetadata(METADATA_KEY_PARAMETER_INFOS, matching);
            var passed = matching.Length == 1;
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () =>
                        matching.Length == 0
                            ? $@"Expected {
                                (methodInfo?.Name ?? "Unknown method")
                            } {
                                passed.AsNot()
                            }to have a parameter with type {expected}"
                            : $@"Expected {
                                (methodInfo?.Name ?? "Unknown method")
                            } to have a single parameter with type {expected}, but found {
                                matching.Length
                            }: {
                                string.Join(", ", matching.Select(p => p.Name))
                            } (suggest: provide a parameter name to filter by)",
                    customMessageGenerator
                )
            );
        }

        private static MatcherResult TryMatchParameterType<T>(
            T actual,
            Type expected,
            Func<string> customMessageGenerator
        )
        {
            if (!actual.TryGetMetadata<ParameterInfo>(
                METADATA_KEY_PARAMETER_INFO,
                out var parameterInfo
            ))
            {
                return null;
            }

            if (!actual.TryGetMetadata<MethodInfo>(METADATA_KEY_METHOD_INFO, out var methodInfo))
            {
                methodInfo = actual as MethodInfo;
            }

            var passed = parameterInfo.ParameterType == expected;
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () => $@"Expected {
                        (methodInfo?.Name ?? "Unknown method")
                    } {
                        passed.AsNot()
                    }to have parameter '{parameterInfo.Name}' with type {expected} (found: {parameterInfo.ParameterType})",
                    customMessageGenerator
                )
            );
        }

        private static MatcherResult TryMatchPropertyType<T>(
            T actual,
            Type expected,
            Func<string> customMessageGenerator
        )
        {
            if (!actual.HasMetadata(METADATA_KEY_PROPERTY_NAME))
            {
                return null;
            }

            if (!actual.TryGetMetadata<PropertyInfo>(
                    METADATA_KEY_PROPERTY_INFO,
                    out var propInfo
                )
            )
            {
                actual.TryGetMetadata<string>(METADATA_KEY_PROPERTY_NAME, out var propName);

                return new MatcherResult(
                    true,
                    FinalMessageFor(
                        () => propName is null
                            ? "actual value was null"
                            : $"actual missing property {propName}",
                        customMessageGenerator
                    )
                );
            }

            var passed = propInfo.PropertyType == expected;
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () =>
                        $@"Expected property '{propInfo.DeclaringType.PrettyName()}.{propInfo.Name}' {passed.AsNot()}to be of type '{expected}', but it has type '{propInfo.PropertyType}'",
                    customMessageGenerator
                )
            );
        }

        /// <summary>
        /// Tests for any parameter on a .Method continuation
        /// </summary>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public static IMore<MethodInfo> Parameter(
            this ICanAddMatcher<MethodInfo> continuation
        )
        {
            return continuation.Parameter(NULL_STRING, NULL_GENERATOR);
        }

        /// <summary>
        /// Tests for any parameter on a .Method continuation
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static IMore<MethodInfo> Parameter(
            this ICanAddMatcher<MethodInfo> continuation,
            string parameterName
        )
        {
            return continuation.Parameter(parameterName, NULL_STRING);
        }

        /// <summary>
        /// Tests for a parameter by name on a .Method continuation
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="parameterName"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<MethodInfo> Parameter(
            this ICanAddMatcher<MethodInfo> continuation,
            string parameterName,
            string customMessage
        )
        {
            return continuation.Parameter(
                parameterName,
                () => customMessage
            );
        }

        /// <summary>
        /// Tests for any parameter on a .Method continuation
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<MethodInfo> Parameter(
            this ICanAddMatcher<MethodInfo> continuation,
            Func<string> customMessageGenerator
        )
        {
            return continuation.Parameter(
                NULL_STRING,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Tests for a parameter by name on a .Method continuation
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="parameterName"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static IMore<MethodInfo> Parameter(
            this ICanAddMatcher<MethodInfo> continuation,
            string parameterName,
            Func<string> customMessageGenerator
        )
        {
            return continuation.AddMatcher(actual =>
            {
                var parameters = actual.GetParameters();
                actual.DeleteMetadata(METADATA_KEY_PROPERTY_INFO);
                actual.DeleteMetadata(METADATA_KEY_PARAMETER_INFO);
                actual.DeleteMetadata(METADATA_KEY_PARAMETER_INFOS);

                if (parameterName is null)
                {
                    actual.SetMetadata(METADATA_KEY_PARAMETER_INFOS, parameters);
                }
                else
                {
                    parameters = parameters.Where(pi => pi.Name == parameterName)
                        .ToArray();
                    actual.SetMetadata(METADATA_KEY_PARAMETER_INFO, parameters.FirstOrDefault());
                }

                var passed = parameters.Length > 0;
                {
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => $"Expected {passed.AsNot()}to find parameters on method {actual.Name}",
                            customMessageGenerator
                        )
                    );
                }
            });
        }

        /// <summary>
        /// Tests the return value on a .Method continuation
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static IMore<MethodInfo> Returns(
            this ICanAddMatcher<MethodInfo> continuation,
            Type expected
        )
        {
            return continuation.Returns(expected, NULL_STRING);
        }

        /// <summary>
        /// Tests the return value on a .Method continuation
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="expected"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<MethodInfo> Returns(
            this ICanAddMatcher<MethodInfo> continuation,
            Type expected,
            string customMessage
        )
        {
            return continuation.Returns(expected, () => customMessage);
        }

        /// <summary>
        /// Tests that the provided method info returns the expected type
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="expected"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static IMore<MethodInfo> Returns(
            this ICanAddMatcher<MethodInfo> continuation,
            Type expected,
            Func<string> customMessageGenerator
        )
        {
            return continuation.AddMatcher(actual =>
            {
                if (actual is null)
                {
                    throw new InvalidOperationException(
                        $"Cannot test return type on null MethodInfo"
                    );
                }

                var passed = actual.ReturnType == expected;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $@"Expected '{
                            actual.Name
                        }' {
                            passed.AsNot()
                        } to have return value of type {expected} (found: {actual.ReturnType})",
                        customMessageGenerator
                    )
                );
            });
        }
    }
}