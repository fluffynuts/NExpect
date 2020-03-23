using System;
using System.Linq;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;
using NExpect.Implementations.Fluency;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable UnusedMethodReturnValue.Global

namespace NExpect
{
    /// <summary>
    /// Provides matchers for testing if object are instances of specific types
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public static class TypeMatchers
    {
        /// <summary>
        /// Tests if actual is an instance of TExpected
        /// </summary>
        /// <param name="instance">Instance to operate on</param>
        /// <typeparam name="TExpected">Expected Type of the Instance</typeparam>
        public static IMore<TExpected> Of<TExpected>(this IInstanceContinuation instance)
        {
            return Of<TExpected>(instance, NULL_STRING);
        }

        /// <summary>
        /// Tests if actual is an instance of TExpected
        /// </summary>
        /// <param name="instance">Instance to operate on</param>
        /// <param name="customMessage">Custom error message</param>
        /// <typeparam name="TExpected">Expected Type of the Instance</typeparam>
        public static IMore<TExpected> Of<TExpected>(
            this IInstanceContinuation instance,
            string customMessage)
        {
            return instance.Of<TExpected>(() => customMessage);
        }

        /// <summary>
        /// Tests if actual is an instance of TExpected
        /// </summary>
        /// <param name="instance">Instance to operate on</param>
        /// <param name="customMessageGenerator">Custom error message</param>
        /// <typeparam name="TExpected">Expected Type of the Instance</typeparam>
        public static IMore<TExpected> Of<TExpected>(
            this IInstanceContinuation instance,
            Func<string> customMessageGenerator)
        {
            instance.AddMatcher(actual =>
            {
                var expected = typeof(TExpected);
                var passed = expected.IsAssignableFrom(instance.Actual);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        new[]
                        {
                            "Expected",
                            $"<{actual.PrettyName()}>",
                            $"to {passed.AsNot()}be an instance of",
                            $"<{expected.PrettyName()}>"
                        },
                        customMessageGenerator
                    )
                );
            });

            Type incomingType = null;
            if (instance is InstanceContinuation concrete)
            {
                incomingType = concrete.Actual;
                if (concrete.Parent is ICanAddMatcher<TExpected> addMatcher &&
                    concrete.Parent is IExpectationContext<TExpected> expectationContext)
                {
                    return ContinuationFactory.Create<TExpected, More<TExpected>>(
                        addMatcher.GetActual(),
                        expectationContext
                    );
                }
            }

            return new TerminatedMore<TExpected>(incomingType);
        }

        /// <summary>
        /// Tests if actual is an instance of expected
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="expected"></param>
        public static void Of(
            this IInstanceContinuation instance,
            Type expected)
        {
            instance.Of(expected, null as string);
        }

        /// <summary>
        /// Tests if actual is an instance of expected
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="expected"></param>
        /// <param name="customMessage"></param>
        public static void Of(
            this IInstanceContinuation instance,
            Type expected,
            string customMessage)
        {
            instance.Of(expected, () => customMessage);
        }

        /// <summary>
        /// Tests if actual is an instance of expected
        /// </summary>
        /// <param name="instance">Instance to operate on</param>
        /// <param name="expected">Expected Type of the Instance</param>
        /// <param name="customMessageGenerator">Custom error message</param>
        public static void Of(
            this IInstanceContinuation instance,
            Type expected,
            Func<string> customMessageGenerator)
        {
            instance.AddMatcher(
                actual =>
                {
                    var passed = expected.IsAssignableFrom(instance.Actual);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            new[]
                            {
                                "Expected",
                                $"<{actual.PrettyName()}>",
                                $"to {passed.AsNot()}be an instance of",
                                $"<{expected.PrettyName()}>"
                            },
                            customMessageGenerator
                        ));
                });
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="not">Continuation to operate on</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this INotAfterTo<Type> not
        )
        {
            return not.Implement<TInterface>(NULL_STRING);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="not">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this INotAfterTo<Type> not,
            string customMessage
        )
        {
            return not.Implement<TInterface>(() => customMessage);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="not">Continuation to operate on</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this INotAfterTo<Type> not,
            Func<string> customMessageGenerator
        )
        {
            return not.AddImplementsMatcher<TInterface>(customMessageGenerator);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this IToAfterNot<Type> to
        )
        {
            return to.Implement<TInterface>(NULL_STRING);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this IToAfterNot<Type> to,
            string customMessage
        )
        {
            return to.Implement<TInterface>(() => customMessage);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this IToAfterNot<Type> to,
            Func<string> customMessageGenerator
        )
        {
            return to.AddImplementsMatcher<TInterface>(customMessageGenerator);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this ITo<Type> to
        )
        {
            return to.Implement<TInterface>(null);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this ITo<Type> to,
            string customMessage
        )
        {
            return to.AddImplementsMatcher<TInterface>(() => customMessage);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="not">Continuation to operate on</param>
        /// <param name="expected">Interface type which should be implemented</param>
        /// <returns></returns>
        public static IMore<Type> Implement(
            this INotAfterTo<Type> not,
            Type expected
        )
        {
            return not.Implement(expected, NULL_STRING);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="not">Continuation to operate on</param>
        /// <param name="expected">Interface type which should be implemented</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <returns></returns>
        public static IMore<Type> Implement(
            this INotAfterTo<Type> not,
            Type expected,
            string customMessage
        )
        {
            return not.Implement(expected, () => customMessage);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="not">Continuation to operate on</param>
        /// <param name="expected">Interface type which should be implemented</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <returns></returns>
        public static IMore<Type> Implement(
            this INotAfterTo<Type> not,
            Type expected,
            Func<string> customMessageGenerator
        )
        {
            return not.AddImplementsMatcher(expected, customMessageGenerator);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="expected">Interface type which should be implemented</param>
        /// <returns></returns>
        public static IMore<Type> Implement(
            this IToAfterNot<Type> to,
            Type expected
        )
        {
            return to.Implement(expected, NULL_STRING);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="expected">Interface type which should be implemented</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <returns></returns>
        public static IMore<Type> Implement(
            this IToAfterNot<Type> to,
            Type expected,
            string customMessage
        )
        {
            return to.Implement(expected, () => customMessage);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="expected">Interface type which should be implemented</param>
        /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
        /// <returns></returns>
        public static IMore<Type> Implement(
            this IToAfterNot<Type> to,
            Type expected,
            Func<string> customMessageGenerator
        )
        {
            return to.AddImplementsMatcher(expected, customMessageGenerator);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="expected">Interface type which should be implemented</param>
        /// <returns></returns>
        public static IMore<Type> Implement(
            this ITo<Type> to,
            Type expected
        )
        {
            return to.Implement(expected, NULL_STRING);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="expected">Interface type which should be implemented</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <returns></returns>
        public static IMore<Type> Implement(
            this ITo<Type> to,
            Type expected,
            string customMessage
        )
        {
            return to.Implement(expected, () => customMessage);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="expected">Interface type which should be implemented</param>
        /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
        /// <returns></returns>
        public static IMore<Type> Implement(
            this ITo<Type> to,
            Type expected,
            Func<string> customMessageGenerator
        )
        {
            return AddImplementsMatcher(to, expected, customMessageGenerator);
        }

        private static IMore<Type> AddImplementsMatcher<TInterface>(
            this ICanAddMatcher<Type> addTo,
            Func<string> customMessage
        )
        {
            return AddImplementsMatcher(
                addTo,
                typeof(TInterface),
                customMessage);
        }

        private static IMore<Type> AddImplementsMatcher(
            this ICanAddMatcher<Type> addTo,
            Type shouldImplement,
            Func<string> customMessage)
        {
            addTo.AddMatcher(
                actual =>
                {
                    var interfaces = actual?.GetAllImplementedInterfaces() ?? new Type[0];
                    if (!shouldImplement.IsInterface())
                    {
                        return new MatcherResult(
                            false,
                            FinalMessageFor(
                                () => new[]
                                {
                                    actual.Stringify(),
                                    "is not an interface."
                                },
                                customMessage));
                    }

                    var passed = interfaces.Contains(shouldImplement);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => new[]
                            {
                                "Expected",
                                actual.Stringify(),
                                $"{passed.AsNot()}to implement",
                                shouldImplement.Stringify()
                            },
                            customMessage
                        )
                    );
                });
            return addTo.More();
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="not">Continuation to operate on</param>
        /// <typeparam name="TBase">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Inherit<TBase>(
            this INotAfterTo<Type> not
        )
        {
            return not.Inherit<TBase>(NULL_STRING);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="not">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TBase">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Inherit<TBase>(
            this INotAfterTo<Type> not,
            string customMessage
        )
        {
            return not.AddInheritsMatcher<TBase>(() => customMessage);
        }


        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="not">Continuation to operate on</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="TBase">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Inherit<TBase>(
            this INotAfterTo<Type> not,
            Func<string> customMessageGenerator
        )
        {
            return not.AddInheritsMatcher<TBase>(customMessageGenerator);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <typeparam name="TBase">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Inherit<TBase>(
            this IToAfterNot<Type> to
        )
        {
            return to.Inherit<TBase>(NULL_STRING);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TBase">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Inherit<TBase>(
            this IToAfterNot<Type> to,
            string customMessage
        )
        {
            return to.AddInheritsMatcher<TBase>(() => customMessage);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="TBase">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Inherit<TBase>(
            this IToAfterNot<Type> to,
            Func<string> customMessageGenerator
        )
        {
            return to.AddInheritsMatcher<TBase>(customMessageGenerator);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <typeparam name="TBase">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Inherit<TBase>(
            this ITo<Type> to
        )
        {
            return to.Inherit<TBase>(NULL_STRING);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TBase">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Inherit<TBase>(
            this ITo<Type> to,
            string customMessage
        )
        {
            return to.AddInheritsMatcher<TBase>(() => customMessage);
        }

        /// <summary>
        /// Expects that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <typeparam name="TBase">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Inherit<TBase>(
            this ITo<Type> to,
            Func<string> customMessageGenerator
        )
        {
            return to.AddInheritsMatcher<TBase>(customMessageGenerator);
        }

        private static IMore<Type> AddInheritsMatcher<TBase>(
            this ICanAddMatcher<Type> addTo,
            Func<string> customMessageGenerator
        )
        {
            addTo.AddMatcher(
                actual =>
                {
                    var expected = typeof(TBase);
                    if (expected.IsInterface())
                    {
                        return new MatcherResult(
                            false,
                            FinalMessageFor(
                                () => new[]
                                {
                                    actual.Stringify(),
                                    "is not a class."
                                },
                                customMessageGenerator));
                    }

                    var passed = expected.IsAssignableFrom(actual);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => new[]
                            {
                                "Expected",
                                actual.Stringify(),
                                $"{passed.AsNot()}to inherit",
                                expected.Stringify()
                            },
                            customMessageGenerator
                        )
                    );
                });
            return addTo.More();
        }

        //TODO: find a better home for this method
        private static string PrettyName(this Type type)
        {
            if (type == null)
                return "(null Type)";
            if (type.IsGenericType())
            {
                if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var underlyingType = type.GetGenericArguments()[0];
                    return $"{PrettyName(underlyingType)}?";
                }

                var typeFyllName = type.FullName ?? string.Empty;
                var baseName = typeFyllName.Substring(0, typeFyllName.IndexOf("`", StringComparison.Ordinal));
                var parts = baseName.Split('.');
                return parts.Last() + "<" + string.Join(", ", type.GetGenericArguments().Select(PrettyName)) + ">";
            }
            else
                return type.FullName;
        }
    }
}