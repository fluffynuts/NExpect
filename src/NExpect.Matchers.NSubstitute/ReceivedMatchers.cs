using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Imported.PeanutButter.Utils;
using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Exceptions;

// ReSharper disable InvokeAsExtensionMethod
namespace NExpect;

/// <summary>
/// Provides NSubstitute extensions for NExpect
/// </summary>
public static class ReceivedMatchers
{
    /// <summary>
    /// Returns NSubstitute Received()
    /// </summary>
    /// <param name="have"></param>
    /// <typeparam name="T"></typeparam>
    public static T Received<T>(this IHave<T> have) where T : class
    {
        Assertions.Forget(have);
        var actual = have.GetActual();
        var context = actual.GetMetadata<IExpectationContext>(Expectations.METADATA_KEY);
        return context.IsNegated()
            ? SubstituteExtensions.DidNotReceive(actual)
            : SubstituteExtensions.Received(actual);
    }

    /// <summary>
    /// Returns NSubstitute Received(count)
    /// </summary>
    /// <param name="have"></param>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    public static T Received<T>(
        this IHave<T> have,
        int count
    ) where T : class
    {
        Assertions.Forget(have);
        var actual = have.GetActual();
        var context = actual.GetMetadata<IExpectationContext>(Expectations.METADATA_KEY);
        if (context.IsNegated())
        {
            Assertions.Throw(
                "Negation of numbered Receive(N) expectations is not supported! (What would it mean, anyway?)"
            );
        }

        return SubstituteExtensions.Received(actual, count);
    }

    /// <summary>
    /// Verifies that the substitute received only the provided
    /// number of calls, and only to that one method
    /// </summary>
    /// <param name="only"></param>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="UnmetExpectationException"></exception>
    public static T Received<T>(
        this IOnly<T> only,
        int count
    ) where T : class
    {
        Assertions.Forget(only);
        var actual = only.GetActual();
        var context = actual.GetMetadata<IExpectationContext>(Expectations.METADATA_KEY);
        if (context.IsNegated())
        {
            Assertions.Throw(
                "Negation of Only.Received(N) expectations is not supported! (What would it mean, anyway?)"
            );
            return null;
        }

        // we need nsubstitute to do the actual verification, and we don't know
        // what the method will be yet - so the next best thing is to validate
        // that there are the required number of calls, and only to a single method
        IEnumerable<ICall> calls = null;
        calls = SubstituteExtensions.ReceivedCalls(actual);

        var methods = new Dictionary<MethodInfo, int>();
        foreach (var call in calls)
        {
            var methodInfo = call.GetMethodInfo();
            methods.FindOrAdd(
                methodInfo,
                () => 0
            );
            methods[methodInfo]++;
        }

        // let nsubstitute catch the zero case
        if (methods.Count > 1)
        {
            var moreInfo = methods.Select(
                    kvp => $"{kvp.Key.Name} :: {kvp.Value} calls"
                ).OrderBy(s => s)
                .ToArray();
            Assertions.Throw(
                $"""
                 Calls to multiple substitute methods detected:
                 - {moreInfo.JoinWith("\n- ")}
                 """
            );
            return null;
        }

        return SubstituteExtensions.Received(actual, count);
    }
}