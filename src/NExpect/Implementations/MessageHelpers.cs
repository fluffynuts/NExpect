using System;
using System.Collections.Generic;
using System.Linq;
using Imported.PeanutButter.Utils;
using NExpect.Exceptions;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations;

/// <summary>
/// Provides some helpers for formatting and creating failure messages
/// </summary>
public static class MessageHelpers
{
    /// <summary>
    /// Provides an easy access to a null string value
    /// - use to disambiguate calls between the version which
    /// takes a static string and the version which takes a Func&lt;string&gt;
    /// </summary>
    public const string NULL_STRING = null;

    /// <summary>
    /// Provides easy access to a null custom message generator
    /// </summary>
    public const Func<string> NULL_GENERATOR = null;

    /// <summary>
    /// Not to be confused with NULL, this is the string
    /// put in place of nulls within stringified messages
    /// </summary>
    internal const string NULL_REPLACER = "(null)";

    /// <summary>
    /// Provides a final message, given a standard message and
    /// a custom message - if the custom message is null, the standard
    /// message alone is returned
    /// </summary>
    /// <param name="standardMessage">Message which should always be shown at the point of failure</param>
    /// <param name="customMessage">Optional custom message to show if not null</param>
    /// <returns>Final message derived from the two parameters</returns>
    public static string FinalMessageFor(
        string standardMessage,
        string customMessage
    )
    {
        return string.IsNullOrWhiteSpace(customMessage)
            ? standardMessage
            : $"{customMessage}\n\n{standardMessage}";
    }

    /// <summary>
    /// Provides a final message, given a standard message generator
    /// and a custom message generator. If the custom message generator
    /// return null or whitespace, the standard message alone is returned
    /// from the result func.
    /// </summary>
    /// <param name="standardMessage"></param>
    /// <param name="customMessageFunc"></param>
    /// <returns></returns>
    public static Func<string> FinalMessageFor(
        Func<string> standardMessage,
        Func<string> customMessageFunc
    )
    {
        return () =>
        {
            var customMessage = TryGetCustomMessage(customMessageFunc);
            return string.IsNullOrWhiteSpace(customMessage)
                ? standardMessage()
                : $"{customMessage}\n\n{standardMessage()}";
        };
    }

    private static string TryGetCustomMessage(Func<string> customMessageFunc)
    {
        try
        {
            return customMessageFunc?.Invoke();
        }
        catch (Exception ex)
        {
            return $"Unable to evaluate custom message expression: {ex.Message}";
        }
    }

    /// <summary>
    /// Creates a final message, given standard message parts and
    /// a custom message. When the parts, concatenated with a space,
    /// are longer than the expected max line length, they are split across lines.
    /// </summary>
    /// <param name="standardMessageParts"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static string FinalMessageFor(
        string[] standardMessageParts,
        string customMessage
    )
    {
        return FinalMessageFor(MakeMessage(standardMessageParts), customMessage);
    }

    /// <summary>
    /// Creates a final message, given standard message parts and
    /// a custom message generator. When the parts, concatenated with a space,
    /// are longer than the expected max line length, they are split across lines.
    /// </summary>
    /// <param name="standardMessageParts"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static Func<string> FinalMessageFor(
        string[] standardMessageParts,
        Func<string> customMessageGenerator)

    {
        return FinalMessageFor(
            () => MakeMessage(standardMessageParts),
            customMessageGenerator);
    }

    /// <summary>
    /// Creates a final message, given standard message parts generator and
    /// a custom message generator. When the parts, concatenated with a space,
    /// are longer than the expected max line length, they are split across lines.
    /// </summary>
    /// <param name="standardMessagePartsGenerator"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static Func<string> FinalMessageFor(
        Func<string[]> standardMessagePartsGenerator,
        Func<string> customMessageGenerator
    )
    {
        return FinalMessageFor(
            MakeMessage(standardMessagePartsGenerator),
            customMessageGenerator
        );
    }

    private static string MakeMessage(params string[] templateParts)
    {
        var firstPass = templateParts.JoinWith(" ");
        return firstPass.Length > NExpectEnvironment.MaxLineLength
            ? templateParts.JoinWith("\n")
            : firstPass;
    }

    private static Func<string> MakeMessage(Func<string[]> templatePartsGenerator)
    {
        return () =>
        {
            var templateParts = templatePartsGenerator();
            var firstPass = templateParts.JoinWith(" ");
            return firstPass.Length > NExpectEnvironment.MaxLineLength
                ? templateParts.JoinWith("\n")
                : firstPass;
        };
    }

    /// <summary>
    /// Provides the "not" for a matcher message, based on the passed flag.
    /// When your matcher passes, you get "not ", so you can do, eg $"Expected foo {not}to be bar"
    /// When your matcher fails, you get an empty string, which also works for the example given.
    /// </summary>
    /// <param name="passed"></param>
    /// <returns></returns>
    public static string AsNot(this bool passed)
    {
        return passed
            ? "not "
            : "";
    }

    internal static Func<string> MessageForContainsResult(
        bool passed,
        string src,
        string search,
        Func<string> customMessageGenerator
    )
    {
        return FinalMessageFor(
            () => $"Expected {Quote(src)} {passed.AsNot()}to contain {Quote(search)}",
            customMessageGenerator
        );
    }

    internal static Func<string> MessageForMatchResult(
        bool passed,
        string src,
        Func<string> customMessageGenerator
    )
    {
        return FinalMessageFor(
            () => $"Expected {Quote(src)} {passed.AsNot()}to be matched",
            customMessageGenerator
        );
    }

    internal static Func<string> MessageForNotMatchResult(
        bool passed,
        string src,
        Func<string> customMessageGenerator
    )
    {
        return FinalMessageFor(
            () => $"Expected {Quote(src)} {passed.AsNot()}to be matched",
            customMessageGenerator
        );
    }

    internal static Func<string> MessageForNotContainsResult(
        bool passed,
        string src,
        string search,
        Func<string> customMessageGenerator
    )
    {
        return FinalMessageFor(
            () => $"Expected {Quote(src)} {passed.AsNot()}to contain {Quote(search)}",
            customMessageGenerator
        );
    }

    /// <summary>
    /// Quotes a string or object, as required. Only non-null strings
    /// get quotes.
    /// </summary>
    /// <param name="o">Object to quote</param>
    /// <typeparam name="T">Type of object</typeparam>
    /// <returns>String representation of object</returns>
    public static string Quote<T>(T o)
    {
        return Stringifier.Stringify(o, NULL_REPLACER);
    }

    /// <summary>
    /// Returns a collection as a comma-separated list
    /// </summary>
    /// <param name="collection">Collection to operate on</param>
    /// <typeparam name="T">Item type of collection</typeparam>
    /// <returns>Comma-separated list representing the collection</returns>
    public static string Stringify<T>(this IEnumerable<T> collection)
    {
        return collection is null
            ? NULL_REPLACER
            : $"[ {string.Join(", ", collection.Select(Quote))} ]";
    }

    /// <summary>
    /// Returns string with up to 10 elements from a collection with ellipsis if required
    /// </summary>
    /// <param name="collection">Collection to inspect</param>
    /// <typeparam name="T">Item type of collection</typeparam>
    /// <returns>Something like `[ "a", "b", "c" ]`</returns>
    public static string LimitedPrint<T>(this IEnumerable<T> collection)
    {
        if (collection is null)
        {
            return NULL_REPLACER;
        }

        var asArray = collection.ToArray();
        var ellipsis = asArray.Length > 10
            ? " ..."
            : "";
        return $"[ {asArray.Take(10).Select(o => o.Stringify()).JoinWith(", ")}{ellipsis} ]";
    }

    /// <summary>
    /// Performs default stringification of an object
    /// </summary>
    /// <param name="item">Object to stringify</param>
    /// <typeparam name="T">Type of object to stringify</typeparam>
    /// <returns></returns>
    public static string Stringify<T>(this T item)
    {
        return Stringifier.Stringify(item, NULL_STRING);
    }
}