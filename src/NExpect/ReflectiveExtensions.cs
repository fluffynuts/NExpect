using System;
using System.Reflection;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect
{
    /// <summary>
    /// Used to continue reflective property assertions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHasWith<T>
    {
        /// <summary>
        /// Continues the .Property().With syntax
        /// </summary>
        IWith<T> With { get; }
    }

    internal class HasWith<T1, T> : With<T>, IHasWith<T>
    {
        public IWith<T> With { get; }

        internal HasWith(
            IExpectationContext<T1> parent,
            string propertyName,
            Func<T> actualFetcher
        ) : base(actualFetcher)
        {
            With = ContinuationFactory.Create<T, WithPropertyName<T>>(
                actualFetcher,
                this
            );
            (this as IExpectationContext<T1>).TypedParent = parent;
            (With as WithPropertyName<T>).PropertyName = propertyName;
        }
    }

    /// <summary>
    /// Performs expectations reflectively
    /// </summary>
    public static class ReflectiveExtensions
    {
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
        /// Tests the type on a property With continuation
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

                var passed = propInfo.PropertyType == expected;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $@"Expected property '{propInfo.DeclaringType.PrettyName()}.{propInfo.Name}' {passed.AsNot()}to be of type '{expected}', but it has type '{propInfo.PropertyType}'",
                        customMessageGenerator
                    )
                );
            });
        }
    }
}