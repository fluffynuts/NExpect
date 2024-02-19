﻿using System;
using System.Linq;
using System.Reflection;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;
using NExpect.Implementations.Fluency;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable UnusedMethodReturnValue.Global

namespace NExpect;

/// <summary>
/// Provides matchers for testing if object are instances of specific types
/// </summary>
// ReSharper disable once UnusedMember.Global
public static class TypeMatchers
{
    /// <summary>
    /// Assert that the provided object has the expected type
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Type<T>(
        this IHave<T> have,
        Type expected
    )
    {
        return have.Type(expected, NULL_STRING);
    }

    /// <summary>
    /// Assert that the provided object has the expected type
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Type<T>(
        this IHave<T> have,
        Type expected,
        string customMessage
    )
    {
        return have.Type(expected, () => customMessage);
    }

    /// <summary>
    /// Assert that the provided object has the expected type
    /// </summary>
    /// <param name="have"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Type<T>(
        this IHave<T> have,
        Type expected,
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
                        FinalMessageFor(
                            () => "actual is null",
                            customMessageGenerator
                        )
                    );
                }

                if (expected is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        FinalMessageFor(
                            () => "expected type is null",
                            customMessageGenerator
                        )
                    );
                }

                var actualType = actual.GetType();
                var pass = actualType == expected;
                return new MatcherResult(
                    pass,
                    FinalMessageFor(
                        () => $"Expected {actual} [ {actualType} ] {pass.AsNot()}to have type {expected}",
                        customMessageGenerator
                    )
                );
            }
        );
    }

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
        string customMessage
    )
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
        Func<string> customMessageGenerator
    )
    {
        instance.AddMatcher(
            actual =>
            {
                var expected = typeof(TExpected);
                var passed = expected.IsAssignableFrom(instance.Actual);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        new[]
                        {
                            "Expected",
                            $"<{actual.FullyQualifiedPrettyName()}>",
                            $"to {passed.AsNot()}be an instance of",
                            $"<{expected.FullyQualifiedPrettyName()}>"
                        },
                        customMessageGenerator
                    )
                );
            }
        );

        if (instance is InstanceContinuation concrete)
        {
            return new LazyICanAddMatcher<TExpected>(
                concrete.Parent
            ).More();
        }

        return new TerminatedMore<TExpected>(instance.Actual);
    }

    /// <summary>
    /// Tests if actual is an instance of expected
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="expected"></param>
    public static IMore<Type> Of(
        this IInstanceContinuation instance,
        Type expected
    )
    {
        return instance.Of(expected, null as string);
    }

    /// <summary>
    /// Tests if actual is an instance of expected
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    public static IMore<Type> Of(
        this IInstanceContinuation instance,
        Type expected,
        string customMessage
    )
    {
        return instance.Of(expected, () => customMessage);
    }

    /// <summary>
    /// Tests if actual is an instance of expected
    /// </summary>
    /// <param name="instance">Instance to operate on</param>
    /// <param name="expected">Expected Type of the Instance</param>
    /// <param name="customMessageGenerator">Custom error message</param>
    public static IMore<Type> Of(
        this IInstanceContinuation instance,
        Type expected,
        Func<string> customMessageGenerator
    )
    {
        return instance.AddMatcher(
            actual =>
            {
                var passed = expected.IsAssignableFrom(instance.Actual);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        new[]
                        {
                            "Expected",
                            $"<{actual.FullyQualifiedPrettyName()}>",
                            $"to {passed.AsNot()}be an instance of",
                            $"<{expected.FullyQualifiedPrettyName()}>"
                        },
                        customMessageGenerator
                    )
                );
            }
        );
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
            customMessage
        );
    }

    private static IMore<Type> AddImplementsMatcher(
        this ICanAddMatcher<Type> addTo,
        Type shouldImplement,
        Func<string> customMessage
    )
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
                            customMessage
                        )
                    );
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
            }
        );
        return addTo.More();
    }

    /// <summary>
    /// Expects that the Actual type implements the interface provided
    /// as a generic parameter
    /// </summary>
    /// <param name="not">Continuation to operate on</param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<Type> Inherit(
        this INotAfterTo<Type> not,
        Type expected
    )
    {
        return not.Inherit(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Expects that the Actual type implements the interface provided
    /// as a generic parameter
    /// </summary>
    /// <param name="not">Continuation to operate on</param>
    /// <param name="expected"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns></returns>
    public static IMore<Type> Inherit(
        this INotAfterTo<Type> not,
        Type expected,
        string customMessage
    )
    {
        return not.AddInheritsMatcher(
            expected,
            () => customMessage
        );
    }


    /// <summary>
    /// Expects that the Actual type implements the interface provided
    /// as a generic parameter
    /// </summary>
    /// <param name="not">Continuation to operate on</param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns></returns>
    public static IMore<Type> Inherit(
        this INotAfterTo<Type> not,
        Type expected,
        Func<string> customMessageGenerator
    )
    {
        return not.AddInheritsMatcher(
            expected,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Expects that the Actual type implements the interface provided
    /// as a generic parameter
    /// </summary>
    /// <param name="to">Continuation to operate on</param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<Type> Inherit(
        this IToAfterNot<Type> to,
        Type expected
    )
    {
        return to.Inherit(expected, NULL_STRING);
    }

    /// <summary>
    /// Expects that the Actual type implements the interface provided
    /// as a generic parameter
    /// </summary>
    /// <param name="to">Continuation to operate on</param>
    /// <param name="expected"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns></returns>
    public static IMore<Type> Inherit(
        this IToAfterNot<Type> to,
        Type expected,
        string customMessage
    )
    {
        return to.Inherit(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Expects that the Actual type implements the interface provided
    /// as a generic parameter
    /// </summary>
    /// <param name="to">Continuation to operate on</param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns></returns>
    public static IMore<Type> Inherit(
        this IToAfterNot<Type> to,
        Type expected,
        Func<string> customMessageGenerator
    )
    {
        return to.AddInheritsMatcher(
            expected,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Expects that the Actual type implements the interface provided
    /// as a generic parameter
    /// </summary>
    /// <param name="to">Continuation to operate on</param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<Type> Inherit(
        this ITo<Type> to,
        Type expected
    )
    {
        return to.Inherit(
            expected,
            NULL_STRING
        );
    }

    /// <summary>
    /// Expects that the Actual type implements the interface provided
    /// as a generic parameter
    /// </summary>
    /// <param name="to">Continuation to operate on</param>
    /// <param name="expected"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns></returns>
    public static IMore<Type> Inherit(
        this ITo<Type> to,
        Type expected,
        string customMessage
    )
    {
        return to.Inherit(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Expects that the Actual type implements the interface provided
    /// as a generic parameter
    /// </summary>
    /// <param name="to">Continuation to operate on</param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns></returns>
    public static IMore<Type> Inherit(
        this ITo<Type> to,
        Type expected,
        Func<string> customMessageGenerator
    )
    {
        return to.AddInheritsMatcher(
            expected,
            customMessageGenerator
        );
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
        return not.AddInheritsMatcher(typeof(TBase), () => customMessage);
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
        return not.AddInheritsMatcher(
            typeof(TBase),
            customMessageGenerator
        );
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
        return to.Inherit<TBase>(
            () => customMessage
        );
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
        return to.AddInheritsMatcher(
            typeof(TBase),
            customMessageGenerator
        );
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
        return to.Inherit<TBase>(() => customMessage);
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
        return to.AddInheritsMatcher(
            typeof(TBase),
            customMessageGenerator
        );
    }


    private static IMore<Type> AddInheritsMatcher(
        this ICanAddMatcher<Type> addTo,
        Type expected,
        Func<string> customMessageGenerator
    )
    {
        addTo.AddMatcher(
            actual =>
            {
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
                            customMessageGenerator
                        )
                    );
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
            }
        );
        return addTo.More();
    }


    /// <summary>
    /// Tests whether or not an object has all default values
    /// for all properties
    /// </summary>
    /// <param name="def">continuation</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Properties<T>(
        this IDefault<T> def
    )
    {
        return def.Properties(NULL_STRING);
    }

    /// <summary>
    /// Tests whether or not an object has all default values
    /// for all properties
    /// </summary>
    /// <param name="def">continuation</param>
    /// <param name="customMessage">Custom message for failure events</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Properties<T>(
        this IDefault<T> def,
        string customMessage
    )
    {
        return def.Properties(() => customMessage);
    }

    /// <summary>
    /// Tests whether or not an object has all default values
    /// for all properties
    /// </summary>
    /// <param name="def">continuation</param>
    /// <param name="customMessageGenerator">Provides custom message for failure events</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Properties<T>(
        this IDefault<T> def,
        Func<string> customMessageGenerator
    )
    {
        return def.AddMatcher(
            actual =>
            {
                var props = actual?.GetType().GetProperties() ?? new PropertyInfo[0];
                var passed = props.IsEmpty() ||
                    props.Select(
                        pi => AreEqual(pi.GetValue(actual), pi.PropertyType.DefaultValue())
                    ).All(o => o);
                return new MatcherResult(
                    passed,
                    () => $"Expected object {passed.AsNot()}to have default values for all properties",
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Tests whether or not an object has all default values
    /// for all properties
    /// </summary>
    /// <param name="def">continuation</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Fields<T>(
        this IDefault<T> def
    )
    {
        return def.Fields(NULL_STRING);
    }

    /// <summary>
    /// Tests whether or not an object has all default values
    /// for all properties
    /// </summary>
    /// <param name="def">continuation</param>
    /// <param name="customMessage">Custom message for failure events</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Fields<T>(
        this IDefault<T> def,
        string customMessage
    )
    {
        return def.Fields(() => customMessage);
    }

    /// <summary>
    /// Tests whether or not an object has all default values
    /// for all properties
    /// </summary>
    /// <param name="def">continuation</param>
    /// <param name="customMessageGenerator">Provides custom message for failure events</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Fields<T>(
        this IDefault<T> def,
        Func<string> customMessageGenerator
    )
    {
        return def.AddMatcher(
            actual =>
            {
                var props = actual?.GetType().GetFields() ?? new FieldInfo[0];
                var passed = props.IsEmpty() ||
                    props.Select(
                        fi => AreEqual(fi.GetValue(actual), fi.FieldType.DefaultValue())
                    ).All(o => o);
                return new MatcherResult(
                    passed,
                    () => $"Expected object {passed.AsNot()}to have default values for all fields",
                    customMessageGenerator
                );
            }
        );
    }

    private static bool AreEqual(object left, object right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    // TODO: figure out a single source of truth: this produces
    // slightly different results from PB's PrettyName, specifically,
    // it includes namespacing
    private static string FullyQualifiedPrettyName(this Type type)
    {
        if (type == null)
        {
            return "(null Type)";
        }

        if (type.IsGenericType())
        {
            if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var underlyingType = type.GetGenericArguments()[0];
                return $"{FullyQualifiedPrettyName(underlyingType)}?";
            }

            var typeFullName = type.FullName ?? string.Empty;
            var baseName = typeFullName.Substring(0, typeFullName.IndexOf("`", StringComparison.Ordinal));
            var parts = baseName.Split('.');
            return parts.Last() + "<" +
                string.Join(", ", type.GetGenericArguments().Select(FullyQualifiedPrettyName)) + ">";
        }
        else
        {
            return type.FullName;
        }
    }
}