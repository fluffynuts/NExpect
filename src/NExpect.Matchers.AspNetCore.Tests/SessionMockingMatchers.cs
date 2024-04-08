using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NSubstitute;
using PeanutButter.Utils;

namespace NExpect.Matchers.AspNet.Tests;

public static class SessionMockingMatchers
{
    /// <summary>
    /// Returns NSubstitute Received()
    /// </summary>
    /// <param name="have"></param>
    /// <typeparam name="T"></typeparam>
    public static ISession Received(this ICollectionHave<KeyValuePair<string, string>> have)
    {
        Assertions.Forget(have);
        if (!have.TryGetMetadata<ISession>(AspNetCoreExpectations.ACTUAL_SESSION_KEY, out var session))
        {
            throw new InvalidOperationException(
                $"Unable to perform NSubstitute assertions: session is not available"
            );
        }

        var context = session.GetMetadata<IExpectationContext>(METADATA_KEY);
        return context.IsNegated()
            ? SubstituteExtensions.DidNotReceive(session)
            : SubstituteExtensions.Received(session);
    }

    /// <summary>
    /// Returns NSubstitute Received(count)
    /// </summary>
    /// <param name="have"></param>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    public static T Received<T>(this IHave<T> have, int count) where T : class
    {
        Assertions.Forget(have);
        var actual = have.GetActual();
        var context = actual.GetMetadata<IExpectationContext>(METADATA_KEY);
        if (context.IsNegated())
        {
            throw new NotSupportedException(
                $"Negation of numbered Receive(N) expectations is not supported! (What would it mean, anyway?)"
            );
        }

        return SubstituteExtensions.Received(have.GetActual(), count);
    }

    private const string METADATA_KEY = "__ExpectationContext__";
}