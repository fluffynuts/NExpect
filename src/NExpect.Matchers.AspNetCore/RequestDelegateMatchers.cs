using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Imported.PeanutButter.Utils;
using Microsoft.AspNetCore.Http;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect;

/// <summary>
/// Provides matchers for request delegates, eg when you're
/// testing asp.net middleware
/// </summary>
public static class RequestDelegateMatchers
{
    private const string METADATA_KEY_CALL_ARGS = "__callargs__";

    /// <summary>
    /// Tests if a delegate has been called at all
    /// </summary>
    /// <param name="been"></param>
    /// <returns></returns>
    public static IMore<RequestDelegate> Called(
        this IBeen<RequestDelegate> been
    )
    {
        return been.AddMatcher(actual =>
        {
            if (!actual.TryGetMetadata<List<HttpContext>>(
                    METADATA_KEY_CALL_ARGS,
                    out var args
                )
               )
            {
                return InvalidSetup();
            }

            var passed = args.Count > 0;
            return new MatcherResult(
                passed,
                () => $@"Expected {
                    nameof(RequestDelegate)
                } {passed.AsNot()}to have been called"
            );
        });
    }

    private static bool TryGetMetadata<TOut>(
        object host,
        out TOut result
    )
    {
        // TODO: use reflection to get metadata from
        // the proper source in PeanutButter.Utils (dep of PB.TestUtils.AspNetCore)
        // as metadata is stored in storage on the type
        // in the assembly it's found in, so the `TryGetMetadata` from
        // above will fail as that's our _own_ version of that beast.
        // -> if we can't get the metadata, throw a useful exception
        // encouraging the user to use PB.TestUtils.AspNetCore
        throw new NotImplementedException();
    }

    private static Assembly PeanutButterAssembly
        => _peanutButterAssembly ??= FindLoadedPeanutButterAssembly();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static Assembly FindLoadedPeanutButterAssembly()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(
                    a => a.FullName?.StartsWith(
                        "PeanutButter.Utils,"
                    ) ?? false
            );
    }

    private static Assembly _peanutButterAssembly;

    /// <summary>
    /// Tests if the delegate was called with a required integer value
    /// </summary>
    /// <param name="been"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<RequestDelegate> Called(
        this IBeen<RequestDelegate> been,
        int expected
    )
    {
        return been.AddMatcher(actual =>
        {
            if (
                !actual.TryGetMetadata<List<HttpContext>>(
                    METADATA_KEY_CALL_ARGS,
                    out var args
                )
            )
            {
                return InvalidSetup();
            }

            var passed = args.Count == expected;
            return new MatcherResult(
                passed,
                () => $@"Expected {
                    nameof(RequestDelegate)
                } {passed.AsNot()}to have been called {
                    Times(expected)
                } (was called {Times(args.Count)})"
            );
        });
    }

    private static EnforcedMatcherResult InvalidSetup()
    {
        return new EnforcedMatcherResult(
            false,
            () =>
                $@"Provided RequestDelegate has no '{
                    METADATA_KEY_CALL_ARGS
                }' metadata - please use the 'RequestDelegateTestArena' class from PeanutButter.TestUtils.AspNetCore to set up this test"
        );
    }

    /// <summary>
    /// Fluency: finish off a call to verify that
    /// the delegate was called more than once, ie
    /// Expect(delegate).To.Have.Been.Called(3).Times();
    /// </summary>
    /// <param name="more"></param>
    /// <returns></returns>
    public static IMore<RequestDelegate> Times(
        this IMore<RequestDelegate> more
    )
    {
        return more;
    }

    /// <summary>
    /// Fluency: finish off a call to verify that
    /// the delegate was called 1 Time, ie
    /// Expect(delegate).To.Have.Been.Called(1).Time();
    /// </summary>
    /// <param name="more"></param>
    /// <returns></returns>
    public static IMore<RequestDelegate> Time(
        this IMore<RequestDelegate> more
    )
    {
        return more;
    }

    /// <summary>
    /// Verify that the request delegate was passed the
    /// expected HttpContext
    /// </summary>
    /// <param name="with"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<RequestDelegate> Context(
        this IWith<RequestDelegate> with,
        HttpContext expected
    )
    {
        return with.AddMatcher(actual =>
        {
            if (
                !actual.TryGetMetadata<List<HttpContext>>(
                    METADATA_KEY_CALL_ARGS,
                    out var args
                )
            )
            {
                return InvalidSetup();
            }

            var passed = args.Contains(expected);
            return new MatcherResult(
                passed,
                () => $@"Expected {
                    nameof(RequestDelegate)
                } {passed.AsNot()}to have been called with HttpContext{
                    expected.Stringify()
                }"
            );
        });
    }

    private static string Times(int amount)
    {
        switch (amount)
        {
            case 1:
                return "once";
            case 2:
                return "twice";
            default:
                return $"{amount} times";
        }
    }
}