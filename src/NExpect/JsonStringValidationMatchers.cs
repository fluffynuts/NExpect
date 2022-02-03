using System;
using System.IO;
using NExpect.Interfaces;
using static NExpect.Implementations.MessageHelpers;
using System.Runtime.Serialization.Json;
using System.Text;
using NExpect.MatcherLogic;

namespace NExpect;

/// <summary>
/// Provides matchers to assert whether or not a string is valid JSON
/// </summary>
public static class JsonStringValidationMatchers
{
    /// <summary>
    /// Verifies whether or not the provided string can be parsed as JSON
    /// </summary>
    /// <param name="be"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static IMore<string> Json(
        this IBe<string> be
    )
    {
        return be.Json(NULL_STRING);
    }

    /// <summary>
    /// Verifies whether or not the provided string can be parsed as JSON
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static IMore<string> Json(
        this IBe<string> be,
        string customMessage
    )
    {
        return be.Json(() => customMessage);
    }

    /// <summary>
    /// Verifies whether or not the provided string can be parsed as JSON
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static IMore<string> Json(
        this IBe<string> be,
        Func<string> customMessageGenerator
    )
    {
        return be.AddMatcher(actual =>
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(actual));
            var serializer = new DataContractJsonSerializer(typeof(object));
            var passed = true;
            try
            {
                serializer.ReadObject(stream);
            }
            catch
            {
                passed = false;
            }

            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () => $"Expected '{actual}' {passed.AsNot()}to be JSON",
                    customMessageGenerator
                )
            );
        });
    }
}