using System;
using System.Collections.Generic;
using System.Linq;
using Imported.PeanutButter.Utils;
using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Exceptions;

namespace NExpect;

/// <summary>
/// Provides extension for quickly testing that something has or hasn't
/// been called in any way
/// </summary>
public static class CalledMatchers
{
    /// <summary>
    /// Tests if a substitute has received any calls whatsoever
    /// </summary>
    /// <param name="been"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>IMore&lt;T&gt; continuation</returns>
    public static IMore<T> Called<T>(
        this IBeen<T> been
    ) where T : class
    {
        return been.Called(MessageHelpers.NULL_STRING);
    }

    /// <summary>
    /// Tests if a substitute has received any calls whatsoever
    /// </summary>
    /// <param name="been"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>IMore&lt;T&gt; continuation</returns>
    public static IMore<T> Called<T>(
        this IBeen<T> been,
        string customMessage
    ) where T : class
    {
        return been.Called(() => customMessage);
    }

    /// <summary>
    /// Tests if a substitute has received any calls whatsoever
    /// </summary>
    /// <param name="been"></param>
    /// <param name="messageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>IMore&lt;T&gt; continuation</returns>
    public static IMore<T> Called<T>(
        this IBeen<T> been,
        Func<string> messageGenerator
    ) where T : class
    {
        return been.AddMatcher(actual =>
        {
            try
            {
                var all = actual.ReceivedCalls().ToArray();
                var passed = all.Any();
                return new MatcherResult(
                    passed,
                    MessageHelpers.FinalMessageFor(
                        () => DumpCalls(all, passed),
                        messageGenerator
                    )
                );
            }
            catch (NotASubstituteException)
            {
                // can't have a matcher result as negation gets in the way
                throw new UnmetExpectationException(
                    $"{actual} is not a substitute"
                );
            }
        });
    }

    private static string DumpCalls(
        ICall[] calls,
        bool passed
    )
    {
        var callInfo = calls.Aggregate(
            new List<string>(),
            (acc, cur) =>
            {
                acc.Add(DumpCallInfo(cur));
                return acc;
            }).JoinWith("\n*").Trim();
        return $@"Expected {passed.AsNot()}to have received any calls, but received {
            (callInfo.IsNullOrWhiteSpace() ? "none" : callInfo)
        }";
    }

    private static string DumpCallInfo(ICall cur)
    {
        var methodName = cur.GetMethodInfo().Name;
        var args = cur.GetArguments()
            .Select(a => a is string
                ? $"\"{a}\""
                : a)
            .JoinWith(", ");
        return $"{methodName}({args})";
    }
}