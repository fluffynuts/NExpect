using System;
using System.Linq;
using Imported.PeanutButter.Utils;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect;

/// <summary>
/// Provides matchers to test if data
/// has been gzipped
/// </summary>
public static class GzippedDataMatchers
{
    /// <summary>
    /// Verifies that the provided data has
    /// been gzipped
    /// </summary>
    /// <param name="be"></param>
    /// <returns></returns>
    public static ICollectionMore<byte> GZipped(
        this ICollectionBe<byte> be
    )
    {
        return be.GZipped(NULL_STRING);
    }

    /// <summary>
    /// Verifies that the provided data has
    /// been gzipped
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static ICollectionMore<byte> GZipped(
        this ICollectionBe<byte> be,
        string customMessage
    )
    {
        return be.GZipped(() => customMessage);
    }

    /// <summary>
    /// Verifies that the provided data has
    /// been gzipped
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static ICollectionMore<byte> GZipped(
        this ICollectionBe<byte> be,
        Func<string> customMessageGenerator
    )
    {
        return be.AddMatcher(
            actual =>
            {
                if (actual is null)
                {
                    return new EnforcedMatcherResult(
                        false,
                        () => "Cannot verify whether a null byte array has been gzipped"
                    );
                }

                var header = actual.Take(16).ToArray();
                var passed = header.IsGZipped();
                return new MatcherResult(
                    passed,
                    () => $"Expected data {passed.AsNot()}to be gzipped",
                    customMessageGenerator
                );
            }
        );
    }
}